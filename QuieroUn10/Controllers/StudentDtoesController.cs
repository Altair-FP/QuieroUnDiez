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

    public class StudentDtoesController : Controller
    {
        private readonly QuieroUnDiezDBContex _context;

        public StudentDtoesController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }


        [ServiceFilter(typeof(SecurityStudent))]
        // GET: StudentDtoes/Details/5
        public async Task<IActionResult> Details(string errorMessage)
        {
            ViewBag.errorMessage = errorMessage;
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));

            if (id == 0)
            {
                return NotFound();
            }
            var student = _context.Student.Include(r => r.UserAccount).Where(r => r.UserAccountId == id).FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }
            StudentDto studentDto = new StudentDto();
            studentDto.Birthdate = student.Birthdate;
            // studentDto.ConfirmPassword = student.UserAccount.Password;
            studentDto.Email = student.UserAccount.Email;
            studentDto.Name = student.Name;
            //studentDto.Password = student.UserAccount.Password;
            studentDto.Phone = student.Phone;
            studentDto.Surname = student.Surname;
            studentDto.Username = student.UserAccount.Username;


            return View(studentDto);
        }


        // GET: StudentDtoes/Create
        public IActionResult Create(string errorMessage)
        {
            ViewBag.errorMessage = errorMessage;
            return View();
        }


        // POST: StudentDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Username,Password,ConfirmPassword,Email,Name,Surname,Phone,Birthdate")] StudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
                if (studentDto.Password != studentDto.ConfirmPassword)
                {
                    return RedirectToAction(nameof(Create), new { errorMessage = "La contraseña no es correcta" });
                }
                var userExists = _context.UserAccount.Where(u => u.Username.Equals(studentDto.Username)).ToList();

                if (userExists.Count == 1)
                {
                    return RedirectToAction(nameof(Create), new { errorMessage = "Ese usuario ya existe" });
                }
                var emailExists = _context.UserAccount.Where(c => c.Email.Equals(studentDto.Email)).ToList();

                if (emailExists.Count == 1)
                {
                    return RedirectToAction(nameof(Create), new { errorMessage = "Un usuario ya utiliza ese email" });
                }
                var role = _context.Role.Where(r => r.Name.Equals("STUDENT")).FirstOrDefault();


                //Creamos un UserAccount

                UserAccount userAccount = new UserAccount();
                userAccount.Username = studentDto.Username;
                userAccount.Email = studentDto.Email;
                //Aquí hay que encriptar la contraseña
                userAccount.Active = false;
                var contraseñaNuevaEncriptada = Utility.Encriptar(studentDto.Password);
                userAccount.Password = contraseñaNuevaEncriptada;
                userAccount.RoleId = role.ID;
                userAccount.Active = true;

                _context.UserAccount.Add(userAccount);
                await _context.SaveChangesAsync();

                //Creamos el customer

                Student student = new Student();
                student.Birthdate = studentDto.Birthdate;
                student.Name = studentDto.Name;
                student.Phone = studentDto.Phone;
                student.Surname = studentDto.Surname;
                student.UserAccountId = userAccount.ID;
                


                _context.Add(student);
                await _context.SaveChangesAsync();

                Utility.SendEmail(userAccount.Email, "Bienvenido a Quiero Un Diez", "Gracias por formar parte de nuestra familia. Esperamos que le guste nuestro servicio y cualquier duda, solo tiene que enviarnos un email. Gracias.");


                return RedirectToAction("Index", "Login");
            }
            return View(studentDto);

        }

        [ServiceFilter(typeof(SecurityStudent))]
        // GET: StudentDtoes/Edit/5
        public async Task<IActionResult> Edit()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetString("user"));
            if (id == 0)
            {
                return NotFound();
            }
            var student = _context.Student.Include(r => r.UserAccount).Where(r => r.UserAccountId == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }

            StudentDto studentDto = new StudentDto();
            studentDto.Birthdate = student.Birthdate;
            studentDto.ConfirmPassword = student.UserAccount.Password;
            studentDto.Email = student.UserAccount.Email;
            studentDto.Name = student.Name;
            studentDto.Password = student.UserAccount.Password;
            studentDto.Phone = student.Phone;
            studentDto.Surname = student.Surname;
            studentDto.Username = student.UserAccount.Username;



            return View(studentDto);
        }



        // POST: StudentDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Username,Password,ConfirmPassword,Email,Name,Surname,Phone,Birthdate")] StudentDto studentDto)
        {
            if (id != studentDto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userAccountId = Convert.ToInt32(HttpContext.Session.GetString("user"));
                var nombreUser = _context.Student.Include(e => e.UserAccount).Where(r => r.UserAccount.Username == studentDto.Username).FirstOrDefault();
                var userAccount = _context.UserAccount.Where(u => u.ID == userAccountId).FirstOrDefault();
                if (nombreUser == null || userAccount == null)
                {
                    return NotFound();
                }
                else
                {
                    try
                    {
                        if (studentDto.Username.Equals(nombreUser.UserAccount.Username))
                        {
                            if (studentDto.Password.Equals(studentDto.ConfirmPassword))
                            {
                                nombreUser.Birthdate = studentDto.Birthdate;
                                nombreUser.Name = studentDto.Name;
                                nombreUser.Phone = studentDto.Phone;
                                nombreUser.Surname = studentDto.Surname;
                                userAccount.Password = Utility.Encriptar(studentDto.Password);
                                userAccount.Username = studentDto.Username;
                                userAccount.Email = studentDto.Email;

                                _context.Update(nombreUser);
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                return RedirectToAction("Details", "StudentDtoes", new { errorMessage = "Las contraseñas deben coincidir" });
                            }
                        }
                        else
                        {
                            return RedirectToAction(nameof(Index), new { errorMessage = "No se puede cambiar el nombre de usuario" });
                        }

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StudentDtoExists(studentDto.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Details", "StudentDtoes");
                }
            }
            return View(studentDto);

        }

        private bool StudentDtoExists(int id)
        {
            return _context.Student.Any(e => e.ID == id);
        }
    }
}
