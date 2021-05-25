using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuieroUn10.Filter;

namespace QuieroUn10.Controllers
{
    [ServiceFilter(typeof(Security))]
    [ServiceFilter(typeof(SecurityStudent))]
    public class MethodsController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
