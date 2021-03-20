using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuieroUn10.Data;
using QuieroUn10.ENUM;
using QuieroUn10.Models;
using Task = QuieroUn10.Models.Task;

namespace QuieroUn10.Controllers
{
    public class TasksController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public TasksController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var quieroUnDiezDBContex = _context.Task.Include(t => t.StudentHasSubject);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.StudentHasSubject).ThenInclude
                (t=>t.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["StudentHasSubjectId"] = new SelectList(_context.StudentHasSubject, "ID", "ID");
            List<TaskType> taskType = new List<TaskType>();
            taskType.Add(TaskType.EXAM);
            taskType.Add(TaskType.EXERCISE);
            taskType.Add(TaskType.PRACTICE);
            ViewData["Type"] = taskType.ToList();
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Start,End,AllDay,Type,StudentHasSubjectId")] Task task)
        {
            if (ModelState.IsValid)
            {
                //creamos un calendarTask
                CalendarTask calendarTask = new CalendarTask();
                //Buscar el studentHasSubject
                StudentHasSubject studentHasSubject = _context.StudentHasSubject.Where(s => s.ID == task.StudentHasSubjectId).FirstOrDefault();
                calendarTask.StudentId = studentHasSubject.StudentId;
                calendarTask.Day = task.Start;
                task.CreateDate = DateTime.Now;
                if (task.Type.Equals(TaskType.PRACTICE))
                {
                    task.ClassName = "info";
                }else if (task.Type.Equals(TaskType.EXAM))
                {
                    task.ClassName = "important";
                }
                else
                {
                    task.ClassName = "success";
                }
                _context.Add(calendarTask);
                await _context.SaveChangesAsync();
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentHasSubjectId"] = new SelectList(_context.StudentHasSubject, "ID", "ID", task.StudentHasSubjectId);
            List<TaskType> taskType = new List<TaskType>();
            taskType.Add(TaskType.EXAM);
            taskType.Add(TaskType.EXERCISE);
            taskType.Add(TaskType.PRACTICE);
            ViewData["Type"] = taskType.ToList();
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["StudentHasSubjectId"] = new SelectList(_context.StudentHasSubject, "ID", "ID", task.StudentHasSubjectId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreateDate,TaskDate,Type,StudentHasSubjectId")] Task task)
        {
            if (id != task.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.ID))
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
            ViewData["StudentHasSubjectId"] = new SelectList(_context.StudentHasSubject, "ID", "ID", task.StudentHasSubjectId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.StudentHasSubject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Task.FindAsync(id);
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.ID == id);
        }
    }
}
