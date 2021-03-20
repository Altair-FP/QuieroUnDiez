using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuieroUn10.Data;
using QuieroUn10.Dtos;
using QuieroUn10.Models;
using Task = QuieroUn10.Models.Task;

namespace QuieroUn10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuieroUnDiezDBContex _context;
        public HomeController(ILogger<HomeController> logger, QuieroUnDiezDBContex context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
            var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();

            if (!student.Activate)
            {
                ViewData["estudios"] = new SelectList(_context.Studies, "ID", "Name");
                ViewData["asignaturas"] = _context.StudyHasSubject.Include(i => i.Subject);
                ViewData["todas"] = false;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "StudentHasSubjects");
            }  
        }
        public IActionResult RecogerDatos([Bind("ID,StudiesId,SubjectId")] InicioDto inicioDto)
        {
            ViewData["estudios"] = new SelectList(_context.Studies, "ID", "Name");
            ViewData["asignaturas"] = _context.StudyHasSubject.Include(i=>i.Subject).Where(i => i.StudyId == inicioDto.StudiesId);

            ViewData["todas"] = false;
            return View("Index");
        }
        public  IActionResult GuardarDatos([Bind("ID,StudiesId,SubjectId")] InicioDto inicioDto)
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == id).FirstOrDefault();
            var student = _context.Student.Where(s => s.UserAccountId == usuario.ID).FirstOrDefault();
            //Guardamos su curso y sus asignaturas
            student.Activate = true;
            _context.Update(usuario);
            _context.SaveChanges();

           
            foreach (var subject in inicioDto.SubjectId)
            {
                StudentHasSubject studentHasSubject = new StudentHasSubject();
                studentHasSubject.SubjectId = subject;
                studentHasSubject.StudentId = student.ID;
                studentHasSubject.InscriptionDate = DateTime.Now;
                studentHasSubject.Docs = new List<Doc>();
                studentHasSubject.Tasks = new List<Task>();
                _context.Add(studentHasSubject);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "StudentHasSubjects");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
