using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuieroUn10.Data;
using QuieroUn10.Models;
using Task = QuieroUn10.Models.Task;

namespace QuieroUn10.Controllers
{
    public class CalendarTasksController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public CalendarTasksController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }
        //https://github.com/esausilva/fullcalendar-aspnet-core/blob/96133f88afd1373c2e34f9f734c7523bf2153117/fullcalendar-core/Scripts/calendar.js
        // GET: CalendarTasks
        public async Task<IActionResult> Index()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
            var quieroUnDiezDBContex = new List<CalendarTask>();
            if (usuario.Role.Name.Equals("ADMIN"))
            {
                quieroUnDiezDBContex = _context.CalendarTask.Include(c => c.Student).ToList();
            }else if (usuario.Role.Name.Equals("STUDENT"))
            {
                var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();
                quieroUnDiezDBContex = _context.CalendarTask.Include(c => c.Student).Where(s=>s.StudentId == student.ID).ToList();
            }

            ViewBag.tasks = _context.Task.ToList();
            return View(quieroUnDiezDBContex);
        }
        [HttpGet]
        public IActionResult GetEvents()
        {
            List<Task> events = _context.Task.ToList();

            return Json(events);
        }
        // GET: CalendarTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarTask = await _context.CalendarTask
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (calendarTask == null)
            {
                return NotFound();
            }

            return View(calendarTask);
        }

        // GET: CalendarTasks/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Student, "ID", "Name");
            return View();
        }

        // POST: CalendarTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Day,StudentId")] CalendarTask calendarTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendarTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "ID", "Name", calendarTask.StudentId);
            return View(calendarTask);
        }

        // GET: CalendarTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarTask = await _context.CalendarTask.FindAsync(id);
            if (calendarTask == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "ID", "Name", calendarTask.StudentId);
            return View(calendarTask);
        }

        // POST: CalendarTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Day,StudentId")] CalendarTask calendarTask)
        {
            if (id != calendarTask.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendarTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarTaskExists(calendarTask.ID))
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
            ViewData["StudentId"] = new SelectList(_context.Student, "ID", "Name", calendarTask.StudentId);
            return View(calendarTask);
        }

        // GET: CalendarTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarTask = await _context.CalendarTask
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (calendarTask == null)
            {
                return NotFound();
            }

            return View(calendarTask);
        }

        // POST: CalendarTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calendarTask = await _context.CalendarTask.FindAsync(id);
            _context.CalendarTask.Remove(calendarTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarTaskExists(int id)
        {
            return _context.CalendarTask.Any(e => e.ID == id);
        }
    }
}
