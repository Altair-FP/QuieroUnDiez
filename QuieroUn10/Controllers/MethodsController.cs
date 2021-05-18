using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuieroUn10.Controllers
{
    public class MethodsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
