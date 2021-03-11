using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuieroUn10.Data;
using QuieroUn10.Models;

namespace QuieroUn10.Controllers
{
    public class StudentHasSubjectsController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public StudentHasSubjectsController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: StudentHasSubjects
        public async Task<IActionResult> Index()
        {
            var quieroUnDiezDBContex = _context.StudentHasSubject.Include(s => s.Student).Include(s => s.Subject);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: StudentHasSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentHasSubject = await _context.StudentHasSubject
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentHasSubject == null)
            {
                return NotFound();
            }

            return View(studentHasSubject);
        }

        // GET: StudentHasSubjects/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Student, "ID", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Acronym");
            return View();
        }

        // POST: StudentHasSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InscriptionDate,StudentId,SubjectId")] StudentHasSubject studentHasSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentHasSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "ID", "Name", studentHasSubject.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Acronym", studentHasSubject.SubjectId);
            return View(studentHasSubject);
        }

        // GET: StudentHasSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentHasSubject = await _context.StudentHasSubject.FindAsync(id);
            if (studentHasSubject == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "ID", "Name", studentHasSubject.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Acronym", studentHasSubject.SubjectId);
            return View(studentHasSubject);
        }

        // POST: StudentHasSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,InscriptionDate,StudentId,SubjectId")] StudentHasSubject studentHasSubject)
        {
            if (id != studentHasSubject.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentHasSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentHasSubjectExists(studentHasSubject.ID))
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
            ViewData["StudentId"] = new SelectList(_context.Student, "ID", "Name", studentHasSubject.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Acronym", studentHasSubject.SubjectId);
            return View(studentHasSubject);
        }

        // GET: StudentHasSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentHasSubject = await _context.StudentHasSubject
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentHasSubject == null)
            {
                return NotFound();
            }

            return View(studentHasSubject);
        }

        // POST: StudentHasSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentHasSubject = await _context.StudentHasSubject.FindAsync(id);
            _context.StudentHasSubject.Remove(studentHasSubject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentHasSubjectExists(int id)
        {
            return _context.StudentHasSubject.Any(e => e.ID == id);
        }
    }
}
