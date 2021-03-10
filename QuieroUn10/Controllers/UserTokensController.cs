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
    public class UserTokensController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public UserTokensController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: UserTokens
        public async Task<IActionResult> Index()
        {
            var quieroUnDiezDBContex = _context.UserToken.Include(u => u.UserAccount);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: UserTokens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToken = await _context.UserToken
                .Include(u => u.UserAccount)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userToken == null)
            {
                return NotFound();
            }

            return View(userToken);
        }

        // GET: UserTokens/Create
        public IActionResult Create()
        {
            ViewData["UserAccountId"] = new SelectList(_context.UserAccount, "ID", "Password");
            return View();
        }

        // POST: UserTokens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Token,GeneratedDate,Life,UserAccountId")] UserToken userToken)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userToken);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserAccountId"] = new SelectList(_context.UserAccount, "ID", "Password", userToken.UserAccountId);
            return View(userToken);
        }

        // GET: UserTokens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToken = await _context.UserToken.FindAsync(id);
            if (userToken == null)
            {
                return NotFound();
            }
            ViewData["UserAccountId"] = new SelectList(_context.UserAccount, "ID", "Password", userToken.UserAccountId);
            return View(userToken);
        }

        // POST: UserTokens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Token,GeneratedDate,Life,UserAccountId")] UserToken userToken)
        {
            if (id != userToken.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userToken);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTokenExists(userToken.ID))
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
            ViewData["UserAccountId"] = new SelectList(_context.UserAccount, "ID", "Password", userToken.UserAccountId);
            return View(userToken);
        }

        // GET: UserTokens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToken = await _context.UserToken
                .Include(u => u.UserAccount)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userToken == null)
            {
                return NotFound();
            }

            return View(userToken);
        }

        // POST: UserTokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userToken = await _context.UserToken.FindAsync(id);
            _context.UserToken.Remove(userToken);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTokenExists(int id)
        {
            return _context.UserToken.Any(e => e.ID == id);
        }
    }
}
