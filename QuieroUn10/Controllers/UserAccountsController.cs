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
using QuieroUn10.Utilities;

namespace QuieroUn10.Controllers
{
    [ServiceFilter(typeof(Security))]
    [ServiceFilter(typeof(SecurityAdmin))]
    public class UserAccountsController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public UserAccountsController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        // GET: UserAccounts
        public async Task<IActionResult> Index(string? errorMessage, string? successMessage)
        {
            var quieroUnDiezDBContex = _context.UserAccount.Include(u => u.Role);
            ViewBag.errorMessage = errorMessage;
            ViewBag.successMessage = successMessage;
            return View(await quieroUnDiezDBContex.ToListAsync());
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            var userAccount = await _context.UserAccount
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userAccount == null)
            {
                return  RedirectToAction("NotFound","Methods");
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
                return  RedirectToAction("NotFound","Methods");
            }

            var userAccount = await _context.UserAccount.FindAsync(id);
            if (userAccount == null)
            {
                return  RedirectToAction("NotFound","Methods");
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
                return  RedirectToAction("NotFound","Methods");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userAccount.Password = Utility.Encriptar(userAccount.Password);
                    _context.Update(userAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountExists(userAccount.ID))
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
            ViewData["RoleId"] = new SelectList(_context.Set<Role>(), "ID", "Name", userAccount.RoleId);
            return View(userAccount);
        }

        // GET: UserAccounts/Delete/5
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
                var userAccount = await _context.UserAccount
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
                if (userAccount.Role.Name.Equals("STUDENT"))
                {
                    var studenHasSubject = _context.StudentHasSubject.Include(s => s.Student).Where(s => s.Student.UserAccountId == userAccount.ID).ToList();
                    if (studenHasSubject.Count != 0)
                    {
                        ViewBag.errorMessage = "No se puede eliminar, este estudiante tiene asignaturas asignadas.";
                    }
                    else
                    {
                        ViewBag.successMessage = "Se puede eliminar, no tiene ninguna asignatura asignada.";
                    }
                }
                if (userAccount == null)
                {
                    return  RedirectToAction("NotFound","Methods");
                }

                return View(userAccount);
            }
            else
            {
                return RedirectToAction("Index","StudentHasSubjects", new { errorMessage = "No tiene permiso para eliminar un usuario"});
            }

                
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idC = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var usuario = _context.UserAccount.Include(r => r.Role).Where(r => r.ID == idC).FirstOrDefault();
            if (usuario.Role.Name.Equals("ADMIN"))
            {

                var userAccount = await _context.UserAccount.FindAsync(id);
                if (userAccount.Role.Name.Equals("ADMIN"))
                {
                    var admin = _context.Admin.Where(s => s.UserAccountId == userAccount.ID).FirstOrDefault();

                    _context.Admin.Remove(admin);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var student = _context.Student.Where(s => s.UserAccountId == userAccount.ID).FirstOrDefault();

                    _context.Student.Remove(student);
                    await _context.SaveChangesAsync();
                }


                _context.UserAccount.Remove(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { successMessage = "Se ha eliminado correctamente al usuario" });
            }
            else
            {
                return RedirectToAction("Index","StudentHasSubjects", new { errorMessage = "No tienes permiso para eliminar a un usuario"});
            }

        }
        public async Task<IActionResult> DesHab(int? id)
        {
            string habDes = "";
            var idUserAccount = Convert.ToInt32(HttpContext.Session.GetString("user"));
            var ifAdmin = _context.Admin.Include(r => r.UserAccount).Where(e => e.UserAccountId == idUserAccount).ToList();
            if (ifAdmin.Count == 0)
            {
                return RedirectToAction("Index", "Home", new { errorMessage = "Debes ser un admin para deshabilitar" });
            }
            else
            {
                
                //Deshabilitamos
                var usuario = _context.UserAccount.Where(r => r.ID == id).FirstOrDefault();
                
                if(usuario.ID == 1)
                {
                    return RedirectToAction("Index", "UserAccounts", new { errorMessage = "La cuenta del administrador principal no se puede deshabilitar" });

                }
                else
                {
                    if (usuario.Active)
                    {
                        usuario.Active = false;
                        habDes = "deshabilitada";
                    }
                    else
                    {
                        usuario.Active = true;
                        habDes = "habilitada";
                    }
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();

                    //Enviar el email con el token
                    string emailTo = usuario.Email;
                    string reference = "https://quieroundiez.alejandrocruz.es/";


                    string subject = "Cuenta " + habDes;
                    string body = "Desde Quiero Un Diez le informamos que su cuenta de usuario ha sido " + habDes + ". Para mas informacion" +
                        " acceda a nuestra pagina web. ->" + "<a href='" + reference + "'> Quiero Un Diez</a>";

                    Utility.SendEmail(emailTo, subject, body);
                }
                
            }
            return RedirectToAction("Index", "UserAccounts", new { successMessage = "La cuenta del usuario ha sido " + habDes + " correctamente" });
        }

        private bool UserAccountExists(int id)
        {
            return _context.UserAccount.Any(e => e.ID == id);
        }
    }
}
