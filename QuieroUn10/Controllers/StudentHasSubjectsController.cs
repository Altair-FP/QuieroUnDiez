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
using QuieroUn10.ENUM;
using QuieroUn10.Filter;
using QuieroUn10.Models;
using QuieroUn10.Sort;
using Task = QuieroUn10.Models.Task;

namespace QuieroUn10.Controllers
{
    [ServiceFilter(typeof(Security))]
    [ServiceFilter(typeof(SecurityStudentAdmin))]
    public class StudentHasSubjectsController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public StudentHasSubjectsController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: StudentHasSubjects
        [ServiceFilter(typeof(SecurityStudent))]
        public async Task<IActionResult> Index(string errorMessage, string successMessage)
          {
              List<String> imagenes = new List<string>();
              imagenes.Add("border-primary bg2-primary");
              imagenes.Add("border-secondary bg2-secondary");
              imagenes.Add("border-success bg2-success");
              imagenes.Add("border-danger bg2-danger");
              imagenes.Add("border-warning bg2-warning");
              imagenes.Add("border-dark bg2-dark");
              var quieroUnDiezDBContex = new List<StudentHasSubject>();

              ViewBag.imagenes = imagenes;
              ViewBag.errorMessage = errorMessage;
              ViewBag.successMessage = successMessage;
              var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
              var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
              ViewBag.role = usuario.Role.Name;

              //Solo te tienen que salir el número de las tasks futuras.
              var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();
              ViewBag.TasksProximas = _context.StudentHasSubject.Include(s => s.Tasks).Where(s => s.StudentId == student.ID ).Select(s=>s.Tasks.Where(t => t.Start.Date >= DateTime.Now.Date)).ToList();

              quieroUnDiezDBContex = _context.StudentHasSubject.Include(s => s.Student).Include(s => s.Subject).Include(s => s.Tasks).Include(s => s.Docs).Where(s => s.StudentId == student.ID).ToList();

              return View(quieroUnDiezDBContex);
          }


        [ServiceFilter(typeof(SecurityAdmin))]
        public async Task<IActionResult> IndexAdmin(string errorMessage, string successMessage, FormStudenHasSubjectsDto form)
        {
            ViewBag.errorMessage = errorMessage;
            ViewBag.successMessage = successMessage;
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
            ViewBag.role = usuario.Role.Name;
            var quieroUnDiezDBContex = new List<StudentHasSubject>();
            if (usuario.Role.Name.Equals("ADMIN"))
            {
                var lista = _context.StudentHasSubject.Include(s => s.Student).Include(s => s.Subject).Include(s => s.Docs).ToList();
                Dictionary<string, string> dictionaryOrderIng = new Dictionary<string, string>();
                dictionaryOrderIng.Add("Order inscription date", "Order inscription date");
                dictionaryOrderIng.Add("Order by descending date", "Order by descending date");
                dictionaryOrderIng.Add("Order by ascending date", "Order by ascending date");
                ViewBag.selectLista = new SelectList(dictionaryOrderIng, "Key", "Value");
                var predicate = PredicateBuilder.New<StudentHasSubject>(true);
                if (!String.IsNullOrEmpty(form.StudentName))
                {
                    predicate = predicate.And(i => i.Student.Name.ToUpper().Contains(form.StudentName.ToUpper()) || i.Student.Surname.ToUpper().Contains(form.StudentName.ToUpper()));
                }
                if (form.CreationDateI == new DateTime(0001, 01, 01) && form.CreationDateF == new DateTime(0001, 01, 01))
                {
                    predicate = predicate.And(i => i.InscriptionDate.Date <= DateTime.Now.Date);
                }
                if (form.CreationDateI != new DateTime(0001, 01, 01) && form.CreationDateF != new DateTime(0001, 01, 01))
                {
                    predicate = predicate.And(i => i.InscriptionDate.Date >= form.CreationDateI.Date && i.InscriptionDate.Date <= form.CreationDateF.Date);
                }
                if (form.CreationDateI == new DateTime(0001, 01, 01) && form.CreationDateF != new DateTime(0001, 01, 01))
                {
                    predicate = predicate.And(i => i.InscriptionDate.Date <= form.CreationDateF.Date);
                }

                if (form.CreationDateI != new DateTime(0001, 01, 01) && form.CreationDateF == new DateTime(0001, 01, 01))
                {
                    predicate = predicate.And(i => i.InscriptionDate.Date >= form.CreationDateI.Date);
                }
                if (!String.IsNullOrEmpty(form.SubjectName))
                {
                    predicate = predicate.And(i => i.Subject.Name.ToUpper().Contains(form.SubjectName.ToUpper()));
                }


                switch (form.Ordering)
                {
                    case SubjectsOrder.SUBJECTBYDATEASC:
                        //orderItem = _context.Items.Include(a => a.ItemHasCategory).OrderBy(t => t.Name);
                        lista = lista.OrderBy(t => t.InscriptionDate.Date).ThenBy(t => t.InscriptionDate.TimeOfDay).ToList();
                        break;

                    case SubjectsOrder.SUBJECTBYDATEDESC:
                        // orderItem = _context.Items.Include(a => a.ItemHasCategory).OrderByDescending(t => t.Name);
                        lista = lista.OrderByDescending(t => t.InscriptionDate.Date).ThenByDescending(t => t.InscriptionDate.TimeOfDay).ToList();
                        break;
                    default:
                        lista = lista.OrderByDescending(t => t.InscriptionDate.Date).ThenByDescending(t => t.InscriptionDate.TimeOfDay).ToList();
                        break;

                }

                ViewBag.lista = lista.Where(predicate).ToList();


            }
           

            return View("IndexAdmin");
        }
        // GET: StudentHasSubjects/Details/5
        public async Task<IActionResult> Details(int? id, string? successMessage, string? errorMessage)
        {
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
            var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();
            if (id == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            var studentHasSubject = await _context.StudentHasSubject
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ID == id);
            ViewBag.successMessage = successMessage;
            ViewBag.errorMessage = errorMessage;

            if (studentHasSubject == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }
            else
            {
                if(studentHasSubject.StudentId == student.ID)
                {
                    var listaDocumentos = _context.Doc.Where(d => d.StudentHasSubjectId == id).ToList();
                    ViewBag.listaDocumentos = listaDocumentos;

                    ViewBag.listaExamenes = _context.Task.Where(t => t.StudentHasSubjectId == id && t.Type.Equals(TaskType.Examen) && t.Start.Date >= DateTime.Now.Date).ToList();
                    ViewBag.listaPracticas = _context.Task.Where(t => t.StudentHasSubjectId == id && t.Type.Equals(TaskType.Practica) && t.Start.Date >= DateTime.Now.Date).ToList();
                    ViewBag.listaEjercicios = _context.Task.Where(t => t.StudentHasSubjectId == id && t.Type.Equals(TaskType.Ejercicio) && t.Start.Date >= DateTime.Now.Date).ToList();
                }
                else
                {
                    return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "Esa asignatura usted no la tiene asignada."});
                }

            }

            return View(studentHasSubject);
        }

        // GET: StudentHasSubjects/Create
        public IActionResult Create()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
            var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();

            //Aquí tengo una asignatura
            var unaAsignatura = _context.StudentHasSubject.Where(s => s.StudentId == student.ID).FirstOrDefault();

            //Buscamos de esa asignatura el study
            var estudioAsignatura = _context.StudyHasSubject.Include(s=>s.Study).Where(s => s.SubjectId == unaAsignatura.ID).FirstOrDefault().Study;

            var StudyHasSubject = _context.StudyHasSubject.Where(s => s.StudyId == estudioAsignatura.ID).ToList();
       

            ViewBag.subjects = _context.StudyHasSubject.Include(s=>s.Subject).Where(s => s.StudyId == estudioAsignatura.ID).ToList();
 
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
                if(subject != null)
                {
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
                }
                else
                {
                    return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "La asignatura no se ha encontrado." });
                }
               


                return RedirectToAction("Index", "StudentHasSubjects", new { successMessage = "La asignatura se ha añadido correctamente." });
            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "ID", "Acronym", studentHasSubjectDto.SubjectId);
            return View(studentHasSubjectDto);
        }

        // GET: StudentHasSubjects/Edit/5
       /* public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            var studentHasSubject = await _context.StudentHasSubject.FindAsync(id);
            if (studentHasSubject == null)
            {
                return  RedirectToAction("NotFound","Methods");
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
                return  RedirectToAction("NotFound","Methods");
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
                        return  RedirectToAction("NotFound","Methods");
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
        }*/

        // GET: StudentHasSubjects/Delete/5
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
                var studentHasSubject = await _context.StudentHasSubject
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.ID == id);
                if (studentHasSubject == null)
                {
                    return  RedirectToAction("NotFound","Methods");
                }

                return View(studentHasSubject);
            }
            else
            {
                //Ver si la asignatura es tuya
                var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();
                var studentHasSubject = await _context.StudentHasSubject
               .Include(s => s.Student)
               .Include(s => s.Subject)
               .FirstOrDefaultAsync(m => m.ID == id);
                if (studentHasSubject == null)
                {
                    return  RedirectToAction("NotFound","Methods");
                }
                if(studentHasSubject.StudentId == student.ID)
                {
                    ViewBag.errorMessage = "Se eliminaran todos los documentos y tareas asociadas a esta asignatura.";
                    return View(studentHasSubject);
                }
                else
                {
                    return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "No tienes permiso para eliminar a una asignatura de otro estudiante." });
                }

            }

        }

        // POST: StudentHasSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string? errorMessage)
        {
            ViewBag.errorMessage = errorMessage;
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
            if (usuario.Role.Name.Equals("ADMIN"))
            {

                var studentHasSubject = await _context.StudentHasSubject.FindAsync(id);
                var subject = _context.Subject.Where(s => s.ID == studentHasSubject.SubjectId).FirstOrDefault();

                _context.StudentHasSubject.Remove(studentHasSubject);
                await _context.SaveChangesAsync();

                if (!subject.Formal_Subject)
                {
                    //Eliminamos tambien la asignatura
                    _context.Subject.Remove(subject);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index), new { successMessage = "Se ha eliminado correctamente al estudiante"});
            }
            else
            {
                //Ver si la asignatura es tuya
                var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();
                var studentHasSubject = await _context.StudentHasSubject.FindAsync(id);
                if (studentHasSubject.StudentId == student.ID)
                {
                    var subject = _context.Subject.Where(s => s.ID == studentHasSubject.SubjectId).FirstOrDefault();

                    _context.StudentHasSubject.Remove(studentHasSubject);
                    await _context.SaveChangesAsync();

                    if (!subject.Formal_Subject)
                    {
                        //Eliminamos tambien la asignatura
                        _context.Subject.Remove(subject);
                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index), new { successMessage = "Se ha eliminado correctamente al estudiante" });
                }
                else
                {
                    return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "No tienes permiso para eliminar a una asignatura de otro estudiante." });

                }
            }

        }

        private bool StudentHasSubjectExists(int id)
        {
            return _context.StudentHasSubject.Any(e => e.ID == id);
        }
    }
}
