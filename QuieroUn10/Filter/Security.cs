using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuieroUn10.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Filter
{
    public class Security : IActionFilter
    {
        private readonly QuieroUnDiezDBContex _context;

        public Security(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = Convert.ToInt32(context.HttpContext.Session.GetString("user"));
            var user = _context.UserAccount.Where(u => u.ID == userId).FirstOrDefault();
            if (user == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
        }

    }
}
