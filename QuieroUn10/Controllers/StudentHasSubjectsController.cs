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
using QuieroUn10.Models;
using Task = QuieroUn10.Models.Task;

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
        public async Task<IActionResult> Index(string errorMessage, string successMessage)
        {
            List<String> imagenes = new List<string>();
            imagenes.Add("https://i.pinimg.com/736x/11/20/26/1120265a38c5926950d9b680954d6561.jpg");
            imagenes.Add("https://img.freepik.com/foto-gratis/textura-papel-arrugado-azul_23-2148383718.jpg?size=626&ext=jpg");
            imagenes.Add("https://image.freepik.com/foto-gratis/textura-papel-arrugado-negro_1194-6941.jpg");
            imagenes.Add("https://i.pinimg.com/736x/f7/fd/33/f7fd33a89cfe128d4d7159a6011bd95d.jpg");
            imagenes.Add("https://i.pinimg.com/736x/f8/4c/a9/f84ca916c0a746ef3bb3779ff0d62533.jpg");
            ViewBag.imagenes = imagenes;
            ViewBag.errorMessage = errorMessage;
            ViewBag.successMessage = successMessage;
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
            var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();
           
            var quieroUnDiezDBContex = new List<StudentHasSubject>();
            if (usuario.Role.Name.Equals("ADMIN"))
            {
                 quieroUnDiezDBContex = _context.StudentHasSubject.Include(s => s.Student).Include(s => s.Subject).ToList();
            }
            else if (usuario.Role.Name.Equals("STUDENT"))
            {
                   
                quieroUnDiezDBContex = _context.StudentHasSubject.Include(s => s.Student).Include(s => s.Subject).Include(s=>s.Tasks).Include(s=>s.Docs).Where(s=>s.StudentId == student.ID).ToList();
            }
            
            return View(quieroUnDiezDBContex);
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
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Acronym");
            return View();
        }

        // POST: StudentHasSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InscriptionDate,StudentId,SubjectId")] StudentHasSubjectsDto studentHasSubjectDto)
        {
            if (ModelState.IsValid)
            {
                //Comprobar si está ya en la lista
                var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
                var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
                var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();

                var listaAsignaturas = _context.StudentHasSubject.Where(i => i.StudentId == student.ID).ToList();
                var listaAsignaturasPropias = new List<Subject>();

                foreach (var asig in listaAsignaturas)
                {
                    listaAsignaturasPropias.Add(_context.Subject.Where(s => s.ID == asig.SubjectId).FirstOrDefault());
                }
                Subject subject = _context.Subject.Where(c => c.ID == studentHasSubjectDto.SubjectId).FirstOrDefault();
                if (listaAsignaturasPropias.Contains(subject))
                {
                    return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "La asignatura ya está añadida." });
                }
                else
                {
                    StudentHasSubject studentHasSubject = new StudentHasSubject();
                    studentHasSubject.InscriptionDate = DateTime.Now;
                    studentHasSubject.StudentId = student.ID;
                    studentHasSubject.SubjectId = studentHasSubjectDto.SubjectId;
                    studentHasSubject.Docs = new List<Doc>();
                    studentHasSubject.Tasks = new List<Task>();
                    _context.Add(studentHasSubject);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction("Index", "StudentHasSubjects", new { successMessage = "La asignatura se ha añadido correctamente." });
            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Acronym", studentHasSubjectDto.SubjectId);
            return View(studentHasSubjectDto);
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
