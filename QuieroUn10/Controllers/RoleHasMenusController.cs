using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuieroUn10.Data;
using QuieroUn10.Models;

namespace QuieroUn10.Controllers
{
    public class RoleHasMenusController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public RoleHasMenusController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: RoleHasMenus
        public async Task<IActionResult> Index()
        {
            var quieroUnDiezDBContex = _context.RoleHasMenu.Include(r => r.Menu).Include(r => r.Role);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: RoleHasMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleHasMenu = await _context.RoleHasMenu
                .Include(r => r.Menu)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roleHasMenu == null)
            {
                return NotFound();
            }

            return View(roleHasMenu);
        }

        // GET: RoleHasMenus/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Set<Menu>(), "ID", "Action");
            ViewData["RoleId"] = new SelectList(_context.Role, "ID", "Name");
            return View();
        }

        // POST: RoleHasMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RoleId,MenuId")] RoleHasMenu roleHasMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleHasMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Set<Menu>(), "ID", "Action", roleHasMenu.MenuId);
            ViewData["RoleId"] = new SelectList(_context.Role, "ID", "Name", roleHasMenu.RoleId);
            return View(roleHasMenu);
        }

        // GET: RoleHasMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleHasMenu = await _context.RoleHasMenu.FindAsync(id);
            if (roleHasMenu == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Set<Menu>(), "ID", "Action", roleHasMenu.MenuId);
            ViewData["RoleId"] = new SelectList(_context.Role, "ID", "Name", roleHasMenu.RoleId);
            return View(roleHasMenu);
        }

        // POST: RoleHasMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RoleId,MenuId")] RoleHasMenu roleHasMenu)
        {
            if (id != roleHasMenu.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleHasMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleHasMenuExists(roleHasMenu.ID))
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
            ViewData["MenuId"] = new SelectList(_context.Set<Menu>(), "ID", "Action", roleHasMenu.MenuId);
            ViewData["RoleId"] = new SelectList(_context.Role, "ID", "Name", roleHasMenu.RoleId);
            return View(roleHasMenu);
        }

        // GET: RoleHasMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleHasMenu = await _context.RoleHasMenu
                .Include(r => r.Menu)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roleHasMenu == null)
            {
                return NotFound();
            }

            return View(roleHasMenu);
        }

        // POST: RoleHasMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleHasMenu = await _context.RoleHasMenu.FindAsync(id);
            _context.RoleHasMenu.Remove(roleHasMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleHasMenuExists(int id)
        {
            return _context.RoleHasMenu.Any(e => e.ID == id);
        }
    }
}
