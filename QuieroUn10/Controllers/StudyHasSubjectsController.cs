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
    public class StudyHasSubjectsController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public StudyHasSubjectsController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: StudyHasSubjects
        public async Task<IActionResult> Index()
        {
            var quieroUnDiezDBContex = _context.StudyHasSubject.Include(s => s.Study).Include(s => s.Subject);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: StudyHasSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyHasSubject = await _context.StudyHasSubject
                .Include(s => s.Study)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studyHasSubject == null)
            {
                return NotFound();
            }

            return View(studyHasSubject);
        }

        // GET: StudyHasSubjects/Create
        public IActionResult Create()
        {
            ViewData["StudyId"] = new SelectList(_context.Studies, "ID", "Acronym");
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Name");
            return View();
        }

        // POST: StudyHasSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StudyId,SubjectId")] StudyHasSubject studyHasSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studyHasSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudyId"] = new SelectList(_context.Studies, "ID", "Acronym", studyHasSubject.StudyId);
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Name", studyHasSubject.SubjectId);
            return View(studyHasSubject);
        }

        // GET: StudyHasSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyHasSubject = await _context.StudyHasSubject.FindAsync(id);
            if (studyHasSubject == null)
            {
                return NotFound();
            }
            ViewData["StudyId"] = new SelectList(_context.Studies, "ID", "Acronym", studyHasSubject.StudyId);
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Name", studyHasSubject.SubjectId);
            return View(studyHasSubject);
        }

        // POST: StudyHasSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StudyId,SubjectId")] StudyHasSubject studyHasSubject)
        {
            if (id != studyHasSubject.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyHasSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyHasSubjectExists(studyHasSubject.ID))
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
            ViewData["StudyId"] = new SelectList(_context.Studies, "ID", "Acronym", studyHasSubject.StudyId);
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Name", studyHasSubject.SubjectId);
            return View(studyHasSubject);
        }

        // GET: StudyHasSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyHasSubject = await _context.StudyHasSubject
                .Include(s => s.Study)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studyHasSubject == null)
            {
                return NotFound();
            }

            return View(studyHasSubject);
        }

        // POST: StudyHasSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studyHasSubject = await _context.StudyHasSubject.FindAsync(id);
            _context.StudyHasSubject.Remove(studyHasSubject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyHasSubjectExists(int id)
        {
            return _context.StudyHasSubject.Any(e => e.ID == id);
        }
    }
}
