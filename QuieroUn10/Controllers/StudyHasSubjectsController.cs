using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuieroUn10.Data;
using QuieroUn10.Filter;
using QuieroUn10.Models;

namespace QuieroUn10.Controllers
{
    [ServiceFilter(typeof(Security))]
    [ServiceFilter(typeof(SecurityAdmin))]
    public class StudyHasSubjectsController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public StudyHasSubjectsController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: StudyHasSubjects
        public async Task<IActionResult> Index(string? errorMessage, string? successMessage)
        {
            var quieroUnDiezDBContex = _context.StudyHasSubject.Include(s => s.Study).Include(s => s.Subject);
            ViewBag.errorMessage = errorMessage;
            ViewBag.successMessage = successMessage;
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: StudyHasSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            var studyHasSubject = await _context.StudyHasSubject
                .Include(s => s.Study)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studyHasSubject == null)
            {
                return  RedirectToAction("NotFound","Methods");
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
                return  RedirectToAction("NotFound","Methods");
            }

            var studyHasSubject = await _context.StudyHasSubject.FindAsync(id);
            if (studyHasSubject == null)
            {
                return  RedirectToAction("NotFound","Methods");
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
                return  RedirectToAction("NotFound","Methods");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Study study = _context.Studies.Where(s => s.ID == studyHasSubject.StudyId).FirstOrDefault();
                    if(study != null)
                    {
                        Subject subject = _context.Subject.Where(c => c.ID == studyHasSubject.SubjectId).FirstOrDefault();
                        if(subject != null)
                        {
                            _context.Update(studyHasSubject);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return RedirectToAction(nameof(Index), new { errorMessage = "Error, no se ha podido realizar esa acción." });
                        }

                    }
                    else
                    {
                        return RedirectToAction(nameof(Index), new { errorMessage = "Error, no se ha podido realizar esa acción." });
                    }

                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyHasSubjectExists(studyHasSubject.ID))
                    {
                        return  RedirectToAction("NotFound","Methods");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { successMessage = "Se ha editado correctamente"});
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
                return  RedirectToAction("NotFound","Methods");
            }
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();

            if (usuario.Role.Name.Equals("ADMIN"))
            {
                var studyHasSubject = await _context.StudyHasSubject
                .Include(s => s.Study)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ID == id);
                if (studyHasSubject == null)
                {
                    return  RedirectToAction("NotFound","Methods");
                }

                return View(studyHasSubject);
            }
            else
            {
                return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "No tiene permiso para eliminar un estudio y sus asignaturas" });
            }

        }

        // POST: StudyHasSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();

            if (usuario.Role.Name.Equals("ADMIN"))
            {
                var studyHasSubject = await _context.StudyHasSubject.FindAsync(id);
                _context.StudyHasSubject.Remove(studyHasSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "No tiene permiso para eliminar un estudio y sus asignaturas" });
            }


        }

        private bool StudyHasSubjectExists(int id)
        {
            return _context.StudyHasSubject.Any(e => e.ID == id);
        }
    }
}
