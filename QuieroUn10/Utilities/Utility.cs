using Microsoft.AspNetCore.Http;
using QuieroUn10.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QuieroUn10.Utilities
{
    public class Utility
    {
        public static List<Menu> Menus { get; set; } = new List<Menu>();

        public static int TTL = 60;


        public static void SendEmail(string emailTo, string subject, string body)
        {
            MailMessage email = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            email.To.Add(new MailAddress(emailTo));
            email.From = new MailAddress("altairGym2020@gmail.com");
            email.Subject = subject;
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = body;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;


            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("altairGym2020@gmail.com", "alaplaya");

            smtp.Send(email);
            email.Dispose();


        }

        public static byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }


    }
}
