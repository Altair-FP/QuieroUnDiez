using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(int id)
        {
            var subjects = _context.Subject.Where(s => s.ID == id).FirstOrDefault();
            ViewBag.subjectName = subjects.Name;
            return View();
        }
    }
}
