using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuieroUn10.Data;
using QuieroUn10.Dtos;
using QuieroUn10.Filter;
using QuieroUn10.Models;
using Rotativa.AspNetCore;

namespace QuieroUn10.Controllers
{
    [ServiceFilter(typeof(Security))]
    
    public class SubjectsController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public SubjectsController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }
        [ServiceFilter(typeof(SecurityAdmin))]
        // GET: Subjects
        public async Task<IActionResult> Index(string? errorMessage, string? successMessage, FormSubjectDto formSubjectDto)
        {
            ViewBag.errorMessage = errorMessage;
            ViewBag.successMessage = successMessage;

            var predicate = PredicateBuilder.New<Subject>(true);
            if (!String.IsNullOrEmpty(formSubjectDto.Name))
            {
                predicate = predicate.And(i => i.Name.ToUpper().Contains(formSubjectDto.Name.ToUpper()) || i.Acronym.ToUpper().Contains(formSubjectDto.Name.ToUpper()));
            }


            if (!formSubjectDto.Student_CreateT && !formSubjectDto.Student_CreateF)
            {
                predicate = predicate.And(i => i.Student_Create == true || i.Student_Create == false);
            }
            if (formSubjectDto.Student_CreateT && !formSubjectDto.Student_CreateF)
            {
                predicate = predicate.And(i => i.Student_Create == true);
            }
            if (!formSubjectDto.Student_CreateT && formSubjectDto.Student_CreateF)
            {
                predicate = predicate.And(i => i.Student_Create == false);
            }
            if (formSubjectDto.Student_CreateT && formSubjectDto.Student_CreateF)
            {
                predicate = predicate.And(i => i.Student_Create == true && i.Student_Create == false);
            }

            if (!formSubjectDto.Formal_SubjectT && !formSubjectDto.Formal_SubjectF)
            {
                predicate = predicate.And(i => i.Formal_Subject == true || i.Formal_Subject == false);
            }
            if (formSubjectDto.Formal_SubjectT && !formSubjectDto.Formal_SubjectF)
            {
                predicate = predicate.And(i => i.Formal_Subject == true);
            }
            if (!formSubjectDto.Formal_SubjectT && formSubjectDto.Formal_SubjectF)
            {
                predicate = predicate.And(i =>  i.Formal_Subject == false);
            }
            if (formSubjectDto.Formal_SubjectT && formSubjectDto.Formal_SubjectF)
            {
                predicate = predicate.And(i => i.Formal_Subject == true && i.Formal_Subject == false);
            }



            if (formSubjectDto.Course != null && formSubjectDto.Course > 0 )
            {
                predicate = predicate.And(i => i.Course == formSubjectDto.Course.ToString());
            }

            ViewBag.subjects = _context.Subject.Where(predicate).ToList();
            return View("Index");
        }
        [ServiceFilter(typeof(SecurityAdmin))]
        public async Task<IActionResult> ContactPDF(FormSubjectDto formSubjectDto)
        {
            var predicate = PredicateBuilder.New<Subject>(true);
            if (!String.IsNullOrEmpty(formSubjectDto.Name))
            {
                predicate = predicate.And(i => i.Name.ToUpper().Contains(formSubjectDto.Name.ToUpper()) || i.Acronym.ToUpper().Contains(formSubjectDto.Name.ToUpper()));
            }


            if (!formSubjectDto.Student_CreateT && !formSubjectDto.Student_CreateF)
            {
                predicate = predicate.And(i => i.Student_Create == true || i.Student_Create == false);
            }
            if (formSubjectDto.Student_CreateT && !formSubjectDto.Student_CreateF)
            {
                predicate = predicate.And(i => i.Student_Create == true);
            }
            if (!formSubjectDto.Student_CreateT && formSubjectDto.Student_CreateF)
            {
                predicate = predicate.And(i => i.Student_Create == false);
            }
            if (formSubjectDto.Student_CreateT && formSubjectDto.Student_CreateF)
            {
                predicate = predicate.And(i => i.Student_Create == true && i.Student_Create == false);
            }

            if (!formSubjectDto.Formal_SubjectT && !formSubjectDto.Formal_SubjectF)
            {
                predicate = predicate.And(i => i.Formal_Subject == true || i.Formal_Subject == false);
            }
            if (formSubjectDto.Formal_SubjectT && !formSubjectDto.Formal_SubjectF)
            {
                predicate = predicate.And(i => i.Formal_Subject == true);
            }
            if (!formSubjectDto.Formal_SubjectT && formSubjectDto.Formal_SubjectF)
            {
                predicate = predicate.And(i => i.Formal_Subject == false);
            }
            if (formSubjectDto.Formal_SubjectT && formSubjectDto.Formal_SubjectF)
            {
                predicate = predicate.And(i => i.Formal_Subject == true && i.Formal_Subject == false);
            }



            if (formSubjectDto.Course != null && formSubjectDto.Course > 0)
            {
                predicate = predicate.And(i => i.Course == formSubjectDto.Course.ToString());
            }

            return new ViewAsPdf("Index1", _context.Subject.Where(predicate).ToList());
            {
                // ...
            };
        }
        [ServiceFilter(typeof(SecurityAdmin))]
        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .FirstOrDefaultAsync(m => m.ID == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [ServiceFilter(typeof(SecurityStudentAdmin))]
        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }
        [ServiceFilter(typeof(SecurityAdmin))]
        public async Task<IActionResult> HabDesH(int id)
        {
            var subject =  _context.Subject
               .FirstOrDefault(m => m.ID == id);
            if (subject == null)
            {
                return NotFound();
            }

           
            //Coger al usuario de esa asignatura y coger alguna asignatura de ese estudiante


            //Una asignatura del estudiante y de ahí cojemos el studio
            var studenHasSubject = _context.StudentHasSubject.Where(s => s.SubjectId == id).FirstOrDefault();
            //Coger la asignatura de ese estudiante y con esa asignatura coger el studio de esa asignatura
            var asignaturaStudiante = _context.StudentHasSubject.Where(s => s.StudentId == studenHasSubject.StudentId).FirstOrDefault();
            StudyHasSubject studyHasSubjects = _context.StudyHasSubject.Where(s => s.SubjectId == asignaturaStudiante.SubjectId).FirstOrDefault();

            if (studyHasSubjects != null)
            {

                //Creamos un studyHasSubject con cualquier asignatura del estudiante
                StudyHasSubject studyHasSubject = new StudyHasSubject();
                studyHasSubject.SubjectId = id;
                studyHasSubject.StudyId = studyHasSubjects.StudyId;
                _context.Add(studyHasSubject);
                await _context.SaveChangesAsync();

                subject.Formal_Subject = true;
                _context.Update(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", new {errorMessage = "No se ha habilitar esa asignatura, tiene que validarla manualmente. Debe asignarle un estudio manualmente."});
            }
            
           

            
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Course,Acronym,Active,Student_Create")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
                var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
                if (usuario.Role.Name.Equals("ADMIN"))
                {
                    subject.Formal_Subject = true;
                    subject.Student_Create = false;
                    
                }
                else
                {
                    subject.Formal_Subject = false;
                    subject.Student_Create = true;
                }

               

                _context.Add(subject);
                await _context.SaveChangesAsync();
                if (usuario.Role.Name.Equals("STUDENT"))
                {
                    Student student = _context.Student.Include(s => s.UserAccount).Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();
                    //Creamos un  studenHasSubject
                    StudentHasSubject studentHasSubject = new StudentHasSubject();
                    studentHasSubject.Docs = new List<Doc>();
                    studentHasSubject.InscriptionDate = DateTime.Now;
                    studentHasSubject.StudentId = student.ID;
                    studentHasSubject.SubjectId = subject.ID;
                    studentHasSubject.Tasks = new List<Models.Task>();
                    _context.Add(studentHasSubject);
                    await _context.SaveChangesAsync();                  
                }

                

                return RedirectToAction("Index", "StudentHasSubjects");
            }
            return View(subject);
        }
        [ServiceFilter(typeof(SecurityAdmin))]
        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Course,Acronym,Active,Formal_Subject,Student_Create")] Subject subject)
        {
            if (id != subject.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.ID))
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
            return View(subject);
        }
        [ServiceFilter(typeof(SecurityAdmin))]
        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
            if (usuario.Role.Name.Equals("ADMIN"))
            {
                var subject = await _context.Subject
                .FirstOrDefaultAsync(m => m.ID == id);
                var studentHasSubject = _context.StudentHasSubject.Include(s => s.Student).ThenInclude(c => c.UserAccount).Where(s => s.SubjectId == id).ToList();
                ViewBag.listaAsignaturas = studentHasSubject;
                if (studentHasSubject.Count != 0)
                {
                    if (studentHasSubject.Count == 1)
                    {
                        ViewBag.errorMessage = "No se puede eliminar, esta asignatura está asignada a " + studentHasSubject.Count + " estudiante.";
                    }
                    else
                    {
                        ViewBag.errorMessage = "No se puede eliminar, esta asignatura está asignada a " + studentHasSubject.Count + " estudiantes.";
                    }
                }
                if (subject == null)
                {
                    return NotFound();
                }

                return View(subject);
            }
            else
            {
                return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "No tiene permiso para eliminar una asignatura" });
            }

        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subject.FindAsync(id);

            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();

            if (usuario.Role.Name.Equals("ADMIN"))
            {
                _context.Subject.Remove(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { successMessage = "Se han eliminado todos los documentos y tareas asociadas a la asignatura eliminada." });
            }
            else
            {
                return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "No tiene permiso para eliminar una asignatura" });
            }



        }

        private bool SubjectExists(int id)
        {
            return _context.Subject.Any(e => e.ID == id);
        }
    }
}
