using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuieroUn10.Data;
using QuieroUn10.Dtos;
using QuieroUn10.Filter;
using QuieroUn10.Models;
using QuieroUn10.Utilities;

namespace QuieroUn10.Controllers
{
    [ServiceFilter(typeof(Security))]
    [ServiceFilter(typeof(SecurityAdmin))]
    public class AdminDtoesController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public AdminDtoesController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var quieroUnDiezDBContex = _context.Admin.Include(s => s.UserAccount);
            return View(await quieroUnDiezDBContex.ToListAsync());
        }


        // GET: AdminDtoes/Details/5
        public async Task<IActionResult> Details(string errorMessage)
        {
            ViewBag.errorMessage = errorMessage;
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
            if (id == 0)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            var admin = _context.Admin.Include(r => r.UserAccount).Where(r => r.UserAccountId == id).FirstOrDefault();
            if (admin == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            AdminDto adminDto = new AdminDto();
            adminDto.Email = admin.UserAccount.Email;
            adminDto.Name = admin.Name;
            adminDto.Phone = admin.Phone;
            adminDto.Surname = admin.Surname;
            adminDto.Username = admin.UserAccount.Username;

            return View(adminDto);
        }



        // GET: AdminDtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Username,Password,ConfirmPassword,Email,Name,Surname,Phone")] AdminDto adminDto)
        {
            if (ModelState.IsValid)
            {
                var role = _context.Role.Where(r => r.Name.Equals("ADMIN")).First();
                if (adminDto.Password != adminDto.ConfirmPassword)
                {
                    return RedirectToAction("Create", "AdminDtoes", new { errorMessage = "La contraseña no es correcta" });
                }
                var userExists = _context.Admin.Include(u => u.UserAccount)
                .Where(u => u.UserAccount.Username == adminDto.Username || u.UserAccount.Email == adminDto.Email).ToList();

                if (userExists.Count > 0)
                {
                    return RedirectToAction(nameof(Create), new { errorMessage = "Ese usuario ya existe" });
                }
                //Crear UserAccount
                UserAccount userAccount = new UserAccount();
                userAccount.Username = adminDto.Username;
                var contraseñaNuevaEncriptada = Utility.Encriptar(adminDto.Password);

                userAccount.Password = contraseñaNuevaEncriptada;
                userAccount.Email = adminDto.Email;
                userAccount.RoleId = role.ID;
                userAccount.Active = true;

                _context.UserAccount.Add(userAccount);
                await _context.SaveChangesAsync();

                Admin admin = new Admin();
                admin.Name = adminDto.Name;
                admin.Phone = adminDto.Phone;
                admin.Surname = adminDto.Surname;
                admin.UserAccountId = userAccount.ID;



                _context.Add(admin);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Login");
            }
            return View(adminDto);
        }

        // GET: AdminDtoes/Edit/5
        public async Task<IActionResult> Edit()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));

            if (id == 0)
            {
                return  RedirectToAction("NotFound","Methods");
            }
            var admin = _context.Admin.Include(r => r.UserAccount).Where(r => r.UserAccountId == id).FirstOrDefault();

            if (admin == null)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            AdminDto adminDto = new AdminDto();
            adminDto.ConfirmPassword = admin.UserAccount.Password;
            adminDto.Email = admin.UserAccount.Email;
            adminDto.Name = admin.Name;
            adminDto.Password = admin.UserAccount.Password;
            adminDto.Phone = admin.Phone;
            adminDto.Surname = admin.Surname;
            adminDto.Username = admin.UserAccount.Username;


            return View(adminDto);
        }


        // POST: AdminDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,Password,ConfirmPassword,Email,Name,Surname,Phone")] AdminDto adminDto)
        {
            if (id != adminDto.ID)
            {
                return  RedirectToAction("NotFound","Methods");
            }

            if (ModelState.IsValid)
            {
                var userAccountId = Convert.ToInt32(HttpContext.Session.GetString("user"));
                var nombreUser = _context.Admin.Include(e => e.UserAccount).Where(r => r.UserAccount.Username == adminDto.Username).FirstOrDefault();
                var userAccount = _context.UserAccount.Where(u => u.ID == userAccountId).FirstOrDefault();
                if (nombreUser == null || userAccount == null)
                {
                    return  RedirectToAction("NotFound","Methods");
                }
                else
                {
                    try
                    {
                        if (adminDto.Username.Equals(nombreUser.UserAccount.Username))
                        {
                            if (adminDto.Password.Equals(adminDto.ConfirmPassword))
                            {
                                nombreUser.Name = adminDto.Name;
                                nombreUser.Phone = adminDto.Phone;
                                nombreUser.Surname = adminDto.Surname;
                                nombreUser.UserAccount = userAccount;
                                userAccount.Password = Utility.Encriptar(adminDto.Password);
                                userAccount.Username = adminDto.Username;
                                userAccount.Email = adminDto.Email;

                                _context.Update(nombreUser);
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                return RedirectToAction("Details", "AdminDtoes", new { errorMessage = "Las contraseñas deben coincidir" });
                            }
                        }
                        else
                        {
                            return RedirectToAction(nameof(Index), new { errorMessage = "No se puede cambiar el nombre de usuario" });
                        }

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AdminDtoExists(adminDto.ID))
                        {
                            return  RedirectToAction("NotFound","Methods");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Details", "AdminDtoes");
                }
            }
            return View(adminDto);

        }
        private bool AdminDtoExists(int id)
        {
            return _context.Admin.Any(e => e.ID == id);
        }
    }
}
