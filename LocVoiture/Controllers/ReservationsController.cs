using LocVoiture.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LocVoiture.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(u => u.User).Include(v => v.Voiture);
            return View(reservations);
        }

        [Authorize]
        public ActionResult Reserver()
        {
            ViewBag.VoitureID = new SelectList(db.Voitures, "VoitureID", "NumImmatriculation");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserver(Reservation reservation)
        {
            reservation.UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservation);
        }
    }
}