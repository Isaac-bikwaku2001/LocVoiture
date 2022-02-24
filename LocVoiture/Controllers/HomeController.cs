using LocVoiture.Models;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace LocVoiture.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var voitures = db.Voitures.Include(v => v.Categorie).Include(v => v.Modele);
            return View(voitures.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string email, string subject, string message)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("smtp.gmail.com");
            msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["Email"].ToString()));
            msg.Subject = subject;
            msg.Body = message;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            NetworkCredential credentials = new NetworkCredential("smtp.gmail.com", "");
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);

            return RedirectToAction("Index");
        }
    }
}