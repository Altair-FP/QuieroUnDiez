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
    public class UserAccountsController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public UserAccountsController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: UserAccounts
        public async Task<IActionResult> Index()
        {
            var quieroUnDiezDBContex = _context.UserAccount.Include(u => u.Role);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // GET: UserAccounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Set<Role>(), "ID", "Name");
            return View();
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Username,Password,Email,Active,RoleId")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Set<Role>(), "ID", "Name", userAccount.RoleId);
            return View(userAccount);
        }

        // GET: UserAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Set<Role>(), "ID", "Name", userAccount.RoleId);
            return View(userAccount);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,Password,Email,Active,RoleId")] UserAccount userAccount)
        {
            if (id != userAccount.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountExists(userAccount.ID))
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
            ViewData["RoleId"] = new SelectList(_context.Set<Role>(), "ID", "Name", userAccount.RoleId);
            return View(userAccount);
        }

        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccount = await _context.UserAccount.FindAsync(id);
            _context.UserAccount.Remove(userAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int id)
        {
            return _context.UserAccount.Any(e => e.ID == id);
        }
    }
}
