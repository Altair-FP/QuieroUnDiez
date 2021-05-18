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
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
            StudentHasSubject studentHasSubject = _context.StudentHasSubject.Include(s => s.Student).Where(s => s.Student.UserAccountId == usuario.ID).FirstOrDefault();

            var quieroUnDiezDBContex = _context.Doc.Include(d => d.StudentHasSubject).Where(d=>d.StudentHasSubjectId == studentHasSubject.ID);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }


        // GET: Docs/Create
        public IActionResult Create(int id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Docs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[Bind("Archivo")] DocDto docDto)
        {
            if (ModelState.IsValid)
            {
                var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
                var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
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
                        doc.StudentHasSubjectId = id;
                        

                        _context.Add(doc);

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                        return RedirectToAction("Details", "StudentHasSubjects", new { id = id, errorMessage = "No se ha podido subir el archivo, pesa más de 2 MB" });
                    }
                }
                  
                return RedirectToAction("Details", "StudentHasSubjects",new { id = id });
            }
            
            return View(docDto);
        }

        public async Task<IActionResult> DownloadFile(int id)
        {
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
            var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();

            var myDoc = await _context.Doc.FirstOrDefaultAsync(m => m.ID == id && m.StudentHasSubject.StudentId == student.ID);

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

        public async Task<IActionResult> ViewFile(int id)
        {
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
            var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();

            var myDoc = await _context.Doc.FirstOrDefaultAsync(m => m.ID == id && m.StudentHasSubject.StudentId == student.ID);
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
                Stream stream = new MemoryStream(myDoc.DocByte);
                
                string contentType = myDoc.DocContentType;
                string fileName = myDoc.DocSourceFileName;

                var file = File(stream, contentType);
                
                return file;

            }
        }


        // GET: Docs/Delete/5
        public async Task<IActionResult> Delete(int? id, int? eli)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doc = await _context.Doc
                .Include(d => d.StudentHasSubject)
                .FirstOrDefaultAsync(m => m.ID == id);
            ViewBag.eli = eli;
            if (doc == null)
            {
                return NotFound();
            }

            return View(doc);
        }

        // POST: Docs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int eli)
        {
            var doc = await _context.Doc.FindAsync(id);
            _context.Doc.Remove(doc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "StudentHasSubjects", new { id = eli });
        }

        private bool DocExists(int id)
        {
            return _context.Doc.Any(e => e.ID == id);
        }
    }
}
