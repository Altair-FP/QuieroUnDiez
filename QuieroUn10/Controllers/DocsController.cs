using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuieroUn10.Data;
using QuieroUn10.Dtos;
using QuieroUn10.Models;

namespace QuieroUn10.Controllers
{
    public class DocsController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public DocsController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: Docs
        public async Task<IActionResult> Index()
        {
            var quieroUnDiezDBContex = _context.Doc.Include(d => d.StudentHasSubject);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: Docs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doc = await _context.Doc
                .Include(d => d.StudentHasSubject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doc == null)
            {
                return NotFound();
            }

            return View(doc);
        }

        // GET: Docs/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Docs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Archivo")] DocDto docDto)
        {
            if (ModelState.IsValid)
            {
                var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
                var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
                var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();
                using (var memoryStream = new MemoryStream())
                {
                    await docDto.Archivo.CopyToAsync(memoryStream);
                    if (memoryStream.Length < 2097152)
                    {
                        Doc doc = new Doc();
                        doc.DocByte = memoryStream.ToArray();
                        doc.DocContentType = docDto.Archivo.ContentType;
                        doc.DocSourceFileName = Path.GetFileName(docDto.Archivo.FileName);
                        doc.StudentHasSubjectId = _context.StudentHasSubject.Where(s => s.StudentId == student.ID).FirstOrDefault().ID;
                        

                        _context.Add(doc);

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                }
                  
                return RedirectToAction(nameof(Index));
            }
            
            return View(docDto);
        }

        public async Task<IActionResult> DownloadFile(int id)
        {

            var myDoc = await _context.Doc.FirstOrDefaultAsync(m => m.ID == id);
            if (myDoc == null)
            {
                return NotFound();
            }

            if (myDoc.DocByte == null)
            {
                return View();
            }
            else
            {
                byte[] byteArr = myDoc.DocByte;
                string mimeType = "application/pdf";
                return new FileContentResult(byteArr, mimeType)
                {
                    FileDownloadName = $"{myDoc.DocSourceFileName}"
                };
            }
        }

        // GET: Docs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doc = await _context.Doc.FindAsync(id);
            if (doc == null)
            {
                return NotFound();
            }
            ViewData["StudentHasSubjectId"] = new SelectList(_context.Set<StudentHasSubject>(), "ID", "ID", doc.StudentHasSubjectId);
            return View(doc);
        }

        // POST: Docs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DocByte,DocSourceFileName,DocContentType,StudentHasSubjectId")] Doc doc)
        {
            if (id != doc.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocExists(doc.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentHasSubjectId"] = new SelectList(_context.Set<StudentHasSubject>(), "ID", "ID", doc.StudentHasSubjectId);
            return View(doc);
        }

        // GET: Docs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doc = await _context.Doc
                .Include(d => d.StudentHasSubject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doc == null)
            {
                return NotFound();
            }

            return View(doc);
        }

        // POST: Docs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doc = await _context.Doc.FindAsync(id);
            _context.Doc.Remove(doc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocExists(int id)
        {
            return _context.Doc.Any(e => e.ID == id);
        }
    }
}
