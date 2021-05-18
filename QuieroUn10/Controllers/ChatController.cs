using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuieroUn10.Data;
using QuieroUn10.Models;

namespace QuieroUn10.Controllers
{
    public class ChatController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public ChatController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }
        public IActionResult Index(int id, int eli)
        {
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();

            ViewBag.nombre = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault().Name;
            ViewBag.id = id;

            var subjects = _context.Subject.Where(s => s.ID == id).FirstOrDefault();

            //Veo si la asginatura es mia
           var studenHasSubject = _context.StudentHasSubject.Include(s => s.Student).Where(s => s.Student.UserAccountId == usuario.ID && s.SubjectId == id && s.ID == eli).FirstOrDefault();
            if(studenHasSubject == null)
            {
                return RedirectToAction("Details", "StudentHasSubjects", new {  id = eli });
            }

            if(subjects == null)
            {
                return RedirectToAction("Details", "StudentHasSubjects", new { errorMessage = "No existe esa asignatura", id = eli} );
            }
            else{
                ViewBag.subjectName = subjects.Name;
            }
            
            return View();
        }
    }
}
