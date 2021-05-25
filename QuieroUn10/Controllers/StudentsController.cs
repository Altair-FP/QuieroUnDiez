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
    public class StudentsController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public StudentsController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string? errorMessage, string? successMessage)
        {
            ViewBag.errorMessage = errorMessage;
            ViewBag.successMessage = successMessage;
            var quieroUnDiezDBContex = _context.Student.Include(s => s.UserAccount);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            var student = await _context.Student
                .Include(s => s.UserAccount)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            return View(student);
        }


        // GET: Students/Delete/5
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
                var student = await _context.Student
               .Include(s => s.UserAccount)
               .FirstOrDefaultAsync(m => m.ID == id);
                var studenHasSubject = _context.StudentHasSubject.Include(s => s.Subject).Where(s => s.StudentId == student.ID).ToList();
                ViewBag.listaAsignaturas = studenHasSubject;
                if (studenHasSubject.Count != 0)
                {
                    //Tiene asociada asignaturas.
                    ViewBag.errorMessage = "No se puede eliminar, este estudiante esta inscrito a " + studenHasSubject.Count + " asignaturas.";

                }
                if (student == null)
                {
                    return  RedirectToAction("NotFound","Methods");
                }

                return View(student);
            }
            else
            {
                return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "No tiene permiso para eliminar a un estudiante" });
            }

        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
            if (usuario.Role.Name.Equals("ADMIN"))
            {
                var student = await _context.Student.FindAsync(id);
                var user = _context.UserAccount.Where(u => u.ID == student.UserAccountId).FirstOrDefault();
                _context.Student.Remove(student);
                await _context.SaveChangesAsync();
                _context.UserAccount.Remove(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { successMessage = "Se he eliminado correctamente el estudiante con todos sus documentos y tareas" });
            }
            else
            {
                return RedirectToAction("Index", "StudenHasSubjects", new { errorMessage = "No tiene permiso para eliminar un estudiante" });

            }
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.ID == id);
        }
    }
}
