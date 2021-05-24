using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuieroUn10.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Filter
{
    public class SecurityStudent : IActionFilter
    {
        private readonly QuieroUnDiezDBContex _context;

        public SecurityStudent(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = Convert.ToInt32(context.HttpContext.Session.GetString("user"));
            var user = _context.UserAccount
                .Where(u => u.ID == userId)
                .Include(u => u.Role)
                .FirstOrDefault();

            var role = _context.Role
                 .Where(r => r.ID == user.RoleId)
                 .FirstOrDefault();

            if (role == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            else if (!role.Name.ToUpper().Equals("STUDENT"))
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}
