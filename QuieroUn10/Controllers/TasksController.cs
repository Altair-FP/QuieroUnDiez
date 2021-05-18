using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuieroUn10.Data;
using QuieroUn10.Dtos;
using QuieroUn10.ENUM;
using QuieroUn10.Models;
using Rotativa.AspNetCore;
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

        // GET: Tasks Aquí solo puede acceder el Admin
        public async Task<IActionResult> Index(int? id, int eli)
        {
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
            StudentHasSubject studentHasSubject = _context.StudentHasSubject.Include(s => s.Student).Where(s => s.Student.UserAccountId == usuario.ID).FirstOrDefault();
            ViewBag.eli = id;
            ViewBag.eli2 = eli;
            var quieroUnDiezDBContex = _context.Task.Include(t => t.StudentHasSubject).ThenInclude(s=>s.Student).Where(t=>t.StudentHasSubject.Student.UserAccountId == usuario.ID && t.StudentHasSubject.SubjectId == eli);
           return View(await quieroUnDiezDBContex.ToListAsync());

        }
        public async Task<IActionResult> ContactPDF(int? id, int eli)
        {
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
            // return View(await _context.Customers.ToListAsync());
            return new ViewAsPdf("Index", await _context.Task.Include(t => t.StudentHasSubject).ThenInclude(s => s.Student).Where(t => t.StudentHasSubject.Student.UserAccountId == usuario.ID && t.StudentHasSubject.SubjectId == eli).ToListAsync())
            {
                // ...
            };
        }
        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id, int? eli)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.StudentHasSubject).ThenInclude
                (t=>t.Student).Include(t=>t.StudentHasSubject.Subject)
                .FirstOrDefaultAsync(m => m.ID == id);
            ViewBag.eli = eli;
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create(int id)
        {
            ViewData["StudentHasSubjectId"] = new SelectList(_context.StudentHasSubject, "ID", "ID");
            List<TaskType> taskType = new List<TaskType>();
            taskType.Add(TaskType.Examen);
            taskType.Add(TaskType.Ejercicio);
            taskType.Add(TaskType.Practica);
            ViewData["Type"] = taskType.ToList();
            ViewBag.id = id;
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[Bind("Title,Description,Start,End,Type")] TaskDto taskDto)
        {
            if (ModelState.IsValid)
            {
                var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
                var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
                Task tasks = new Models.Task();
                //creamos un calendarTask
                CalendarTask calendarTask = new CalendarTask();
                //Buscar el studentHasSubject
                StudentHasSubject studentHasSubject = _context.StudentHasSubject.Include(s => s.Student).Where(s => s.Student.UserAccountId == usuario.ID).FirstOrDefault();
                calendarTask.StudentId = studentHasSubject.StudentId;
                calendarTask.DayStart = taskDto.Start;
                calendarTask.DayEnd = taskDto.End;


                tasks.AllDay = true;
                tasks.CreateDate = DateTime.Now;
                if (taskDto.Type.Equals(TaskType.Practica))
                {
                    tasks.ClassName = "practica";
                }else if (taskDto.Type.Equals(TaskType.Examen))
                {
                    tasks.ClassName = "examen";
                }
                else
                {
                    tasks.ClassName = "ejercicio";
                }
                tasks.Description = taskDto.Description;
                tasks.End = taskDto.End;
                tasks.Start = taskDto.Start;
                tasks.StudentHasSubjectId = id;
                tasks.Title = taskDto.Title;
                tasks.Type = taskDto.Type;


                _context.Add(calendarTask);
                await _context.SaveChangesAsync();
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                if(tasks.End == null)
                {
                    Utilities.Utility.SendEmail(usuario.Email, "Nueva tarea programada", "Se ha registrado una nueva tarea con el nombre de: " + tasks.Title + "para el dia " + tasks.Start + ". La tarea es de tipo: " + tasks.Type);

                }
                else
                {
                    Utilities.Utility.SendEmail(usuario.Email, "Nueva tarea programada", "Se ha registrado una nueva tarea con el nombre de: " + tasks.Title + "para el dia " + tasks.Start + ". La tarea es de tipo: " + tasks.Type);

                }

                return RedirectToAction("Details", "StudentHasSubjects", new { id = id });
            }
            List<TaskType> taskType = new List<TaskType>();
            taskType.Add(TaskType.Examen);
            taskType.Add(TaskType.Ejercicio);
            taskType.Add(TaskType.Practica);
            ViewData["Type"] = taskType.ToList();
            return View(taskDto);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id, int? eli)
        {
            if (id == null)
            {
                return NotFound();
            }
            TaskDto taskDto = new TaskDto();
            var task = await _context.Task.FindAsync(id);
            taskDto.Description = task.Description;
            taskDto.End = task.End;
            taskDto.Start = task.Start;
            taskDto.Title = task.Title;
            taskDto.Type = task.Type;
            ViewBag.id = id;
            ViewBag.eli = eli;
            if (task == null)
            {
                return NotFound();
            }
            List<TaskType> taskType = new List<TaskType>();
            taskType.Add(TaskType.Examen);
            taskType.Add(TaskType.Ejercicio);
            taskType.Add(TaskType.Practica);
            ViewData["Type"] = taskType.ToList();
            ViewData["StudentHasSubjectId"] = new SelectList(_context.StudentHasSubject, "ID", "ID", task.StudentHasSubjectId);
            return View(taskDto);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int eli, [Bind("Title,Description,Start,End,Type")] TaskDto taskDto)
        {
            Task tasks = _context.Task.Where(t => t.ID == id).FirstOrDefault();
            if (tasks == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    tasks.Title = taskDto.Title;
                    tasks.Description = taskDto.Description;
                    tasks.Start = taskDto.Start;
                    tasks.End = taskDto.End;
                    tasks.Type = taskDto.Type;

                    if (taskDto.Type.Equals(TaskType.Practica))
                    {
                        tasks.ClassName = "practica";
                    }
                    else if (taskDto.Type.Equals(TaskType.Examen))
                    {
                        tasks.ClassName = "examen";
                    }
                    else
                    {
                        tasks.ClassName = "ejercicio";
                    }

                    _context.Update(tasks);
                    await _context.SaveChangesAsync();

                    //Editamos tambien el calendar task

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(tasks.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "StudentHasSubjects", new { id = eli });
            }
            ViewData["StudentHasSubjectId"] = new SelectList(_context.StudentHasSubject, "ID", "ID", tasks.StudentHasSubjectId);
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id, int? eli)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.StudentHasSubject)
                .FirstOrDefaultAsync(m => m.ID == id);
            ViewBag.eli = eli;
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int eli)
        {
            var task = await _context.Task.FindAsync(id);
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "StudentHasSubjects", new { id = eli, successMessage = "Se ha eliminado la tarea correctamente." });
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.ID == id);
        }
    }
}
