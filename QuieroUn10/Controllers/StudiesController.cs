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
    public class StudiesController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public StudiesController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: Studies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Studies.ToListAsync());
        }

        // GET: Studies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Studies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (study == null)
            {
                return NotFound();
            }

            return View(study);
        }

        // GET: Studies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Acronym")] Study study)
        {
            if (ModelState.IsValid)
            {
                _context.Add(study);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(study);
        }

        // GET: Studies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Studies.FindAsync(id);
            if (study == null)
            {
                return NotFound();
            }
            return View(study);
        }

        // POST: Studies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Acronym")] Study study)
        {
            if (id != study.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(study);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyExists(study.ID))
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
            return View(study);
        }

        // GET: Studies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Studies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (study == null)
            {
                return NotFound();
            }

            return View(study);
        }

        // POST: Studies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var study = await _context.Studies.FindAsync(id);
            _context.Studies.Remove(study);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyExists(int id)
        {
            return _context.Studies.Any(e => e.ID == id);
        }
    }
}
