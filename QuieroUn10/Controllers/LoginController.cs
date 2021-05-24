using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuieroUn10.Data;
using QuieroUn10.Dtos;
using QuieroUn10.Models;
using QuieroUn10.Utilities;


namespace QuieroUn10.Controllers
{
    public class LoginController : Controller
    {

        private readonly QuieroUnDiezDBContex _context;

        public LoginController(QuieroUnDiezDBContex context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? errorMessage,string? successMessage, string username)
        {

            ViewBag.errorMessage = errorMessage;
            ViewBag.successMessage = successMessage;
            ViewBag.username = username;

            return View();
        }


        public IActionResult Login(String username, String password)
        {

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return RedirectToAction("Index", "Login", new { errorMessage = "Los dos campos deben rellenarse" });

            }
            else
            {
                //Si existe userAccount con el username y password
                //que nos llega desde el formulario:
                var contraseñaComprobar = Utility.Encriptar(password);
                var user = _context.UserAccount.Where(u => u.Username.Equals(username) && u.Password.Equals(contraseñaComprobar));
                var usuario = user.FirstOrDefault();
                if (user.Count() == 0)
                {
                    return RedirectToAction("Index", "Login", new { errorMessage = "No existe ese usuario" });
                }
                else if (!usuario.Active)
                {
                    return RedirectToAction("Index", "Login", new { errorMessage = "El usuario no tiene habilitado el acceso" });
                }
                else
                {
                    HttpContext.Session.SetString("user", user.First().ID.ToString());
                    //HttpContext.Session.GetString("user");
                    Utility.Menus = _context.RoleHasMenu.Include(r => r.Menu)
                        .Where(m => m.RoleId == user.First().RoleId)
                        .Select(r => r.Menu)//Selecciono solo los valores del menu
                        .ToList();
                    return RedirectToAction("Index", "Home");
                }
            }
            //Aquí hay que meter un error
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index");
        }

        public IActionResult RememberPassword(string errorMessage)
        {
            ViewBag.errorMessage = errorMessage;
            return View();
        }

        public IActionResult ResetPassword(string errorMessage, string email, string token)
        {
            PasswordDto passwordDto = new PasswordDto();
            passwordDto.Email = email;
            passwordDto.Token = token;
            ViewBag.errorMessage = errorMessage;
            return View(passwordDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([Bind("Email,Token,Password,ConfirmPassword")] PasswordDto passwordDto)
        {
            if (!passwordDto.Password.Equals(passwordDto.ConfirmPassword))
            {
                return RedirectToAction("RememberPassword", new { errorMessage = "Error en contraseñas" });
            }
            else
            {
                //Hay que comprobar el tiempo del token
                UserToken userToken = new UserToken();
                userToken = _context.UserToken.Where(r => r.Token.Equals(passwordDto.Token)).FirstOrDefault();
                var fechaActual = DateTime.Now;
                var fechaDeCreacion = userToken.GeneratedDate;
                if (fechaDeCreacion.AddMinutes(userToken.Life).CompareTo(fechaActual) < 0)
                {
                    return RedirectToAction("ResetPassword", "Login", new { errorMessage = "El tiempo ha expirado" });
                }
                else
                {
                    UserAccount userAccount = new UserAccount();
                    var UserAccountID = GetUserAccountID(passwordDto.Email);
                    string emailTo = GetEmailByUserID(UserAccountID);
                    var nombreUser = await _context.UserAccount.Where(r => r.ID == UserAccountID).FirstOrDefaultAsync();


                    nombreUser.Password = Utility.Encriptar(passwordDto.Password);
                    _context.Update(nombreUser);
                    await _context.SaveChangesAsync();
                }

            }
            return RedirectToAction("Index", "Login");
        }



        public IActionResult SendEmail(string EmailOrUsername)
        {
            var UserAccountID = GetUserAccountID(EmailOrUsername);
            if (UserAccountID == -1)
            {
                return RedirectToAction("RememberPassword", new { errorMessage = "Email or username not valid" });
            }

            //Limpiamos los tokens del usuario

            DeleteTokensByUserID(UserAccountID);

            //Generamos el nuevo token

            UserToken token = new UserToken();
            token.GeneratedDate = DateTime.Now;
            token.Life = Utility.TTL;
            token.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace('+', 'a');
            token.UserAccountId = UserAccountID;
            _context.UserToken.Add(token);
            _context.SaveChanges();


            //Enviar el email con el token
            string emailTo = GetEmailByUserID(UserAccountID);
            string reference = "https://localhost:44333/Login/ResetPassword?email=" + emailTo + "&token=" + token.Token;

            if (emailTo.Equals(""))
            {
                return RedirectToAction("RememberPassword", new { errorMessage = "Email not valid" });
            }

            string subject = "Remember password";
            string body = "Click on the following link to reset the password: " + "<a href='" + reference + "'> Reset Password </a>";

            Utility.SendEmail(emailTo, subject, body);
            return RedirectToAction("Index",new { successMessage = "Se ha enviado un email para restaurar la contraseña" });

        }


        public int GetUserAccountID(string EmailOrUsername)
        {

            int UserAccountID = -1;
            var user = _context.UserAccount.Where(u => u.Username.Equals(EmailOrUsername))
                .FirstOrDefault();
            //En user tenemos el username

            if (user == null)
            {
                var userAccount = _context.UserAccount
                  .Where(c => c.Email.Equals(EmailOrUsername))
                  .FirstOrDefault();
                if (userAccount != null)
                {
                    UserAccountID = userAccount.ID;

                }
            }
            else
            {
                UserAccountID = user.ID;
            }

            return UserAccountID;
        }


        public string GetEmailByUserID(int UserID)
        {
            string email = "";

            var userAccount = _context.UserAccount.Where(c => c.ID == UserID)
                .FirstOrDefault();


            if (userAccount != null)
            {
                email = userAccount.Email;
            }
            return email;

        }



        public void DeleteTokensByUserID(int UserID)
        {

            var tokens = _context.UserToken
                .Where(u => u.UserAccountId == UserID)
                .ToList();


            foreach (UserToken token in tokens)
            {
                _context.UserToken.Remove(token);
            }

            _context.SaveChanges();

        }



    }
}
