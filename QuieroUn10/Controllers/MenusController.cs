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
    public class MenusController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public MenusController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menu.ToListAsync());
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menu == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Controller,Action,Label")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Controller,Action,Label")] Menu menu)
        {
            if (id != menu.ID)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.ID))
                    {
                        return  RedirectToAction("NotFound","Methods");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
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
                var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.ID == id);
                if (menu == null)
                {
                    return  RedirectToAction("NotFound","Methods");
                }

                return View(menu);
            }
            else
            {
                return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "No tiene permiso para eliminar el menu" });
            }

        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
            if (usuario.Role.Name.Equals("ADMIN"))
            {
                _context.Menu.Remove(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "StudentHasSubjects", new { errorMessage = "No tiene permiso para eliminar el menu" });
            }

        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.ID == id);
        }
    }
}
