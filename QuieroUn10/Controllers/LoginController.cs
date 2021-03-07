using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuieroUn10.Data;

namespace QuieroUn10.Controllers
{
    public class LoginController : Controller
    {

        private readonly QuieroUnDiezDBContex _context;

        public LoginController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
           
           
            return View();
        }




    }
}
