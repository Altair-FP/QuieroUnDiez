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
    public class AdminsController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public AdminsController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index(string? errorMessage, string? successMessage)
        {
            ViewBag.errorMessage = errorMessage;
            ViewBag.successMessage = successMessage;
            var quieroUnDiezDBContex = _context.Admin.Include(a => a.UserAccount);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .Include(a => a.UserAccount)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }



        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            ViewData["UserAccountId"] = new SelectList(_context.UserAccount, "ID", "Password", admin.UserAccountId);
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Surname,Phone")] Admin admin)
        {
            if (id != admin.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
                    var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
                    Admin adminEdit = _context.Admin.Where(s => s.UserAccountId == id).FirstOrDefault();
                    adminEdit.Name = admin.Name;
                    adminEdit.Surname = admin.Surname;
                    adminEdit.Phone = admin.Phone;

                    _context.Update(adminEdit);
                    await _context.SaveChangesAsync();
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserAccountId"] = new SelectList(_context.UserAccount, "ID", "Password", admin.UserAccountId);
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .Include(a => a.UserAccount)
                .FirstOrDefaultAsync(m => m.ID == id);
            if(admin.ID == 1)
            {
                return RedirectToAction("Index", "Admins", new { errorMessage = "No tiene permiso para eliminar al administrador principal" });
            }
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admin.FindAsync(id);
            if(admin.ID == 1)
            {
                return RedirectToAction("Index", "Admins", new { errorMessage = "No tiene permiso para eliminar al administrador principal" });
            }
            else
            {
                _context.Admin.Remove(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
        }

        private bool AdminExists(int id)
        {
            return _context.Admin.Any(e => e.ID == id);
        }
    }
}
