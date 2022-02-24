using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LocVoiture.Models;

namespace LocVoiture.Controllers
{
    public class VoituresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Voitures
        public ActionResult Index()
        {
            var voitures = db.Voitures.Include(v => v.Categorie).Include(v => v.Modele);
            return View(voitures.ToList());
        }

        // GET: Voitures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // GET: Voitures/Create
        public ActionResult Create()
        {
            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "Libelle");
            ViewBag.ModeleID = new SelectList(db.Modeles, "ModeleID", "Marque");
            return View();
        }

        // POST: Voitures/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Voiture voiture)
        {
            voiture.ImageFile.SaveAs(@"C:\Users\Isaac\Desktop\devC#\PROJETS\LocVoiture\LocVoiture\Images\voitures\" + voiture.ImageFile.FileName);
            voiture.Image = voiture.ImageFile.FileName;

            db.Voitures.Add(voiture);
            db.SaveChanges();
            ModelState.Clear();

            return RedirectToAction("Index");
        }

        // GET: Voitures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "Libelle", voiture.CategorieID);
            ViewBag.ModeleID = new SelectList(db.Modeles, "ModeleID", "Marque", voiture.ModeleID);
            return View(voiture);
        }

        // POST: Voitures/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoitureID,NumImmatriculation,CategorieID,ModeleID,DateMiseEnCirculation,TypeCarburant,TypeTransmission,Kilometrage,Place,Bagage,PrixLocation,Image")] Voiture voiture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voiture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategorieID = new SelectList(db.Categories, "CategorieID", "Libelle", voiture.CategorieID);
            ViewBag.ModeleID = new SelectList(db.Modeles, "ModeleID", "Marque", voiture.ModeleID);
            return View(voiture);
        }

        // GET: Voitures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voiture voiture = db.Voitures.Find(id);
            db.Voitures.Remove(voiture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
