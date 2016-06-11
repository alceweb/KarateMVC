using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KarateMVC.Models;
using Microsoft.AspNet.Identity;

namespace KarateMVC.Controllers
{
    public class MedaglieresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Medaglieres
        [Authorize]
        public ActionResult Index()
        {
            var utente = User.Identity.GetUserId();
            var medaglieres = db.Medaglieres.Include(m => m.Titolo).Where(m=>m.Uid == utente).OrderByDescending(m => m.Titolo.Data);
            ViewBag.MedCount = medaglieres.Count();
            ViewBag.Oro = medaglieres.Where(c => c.classifica == "1").Count();
            ViewBag.Argento = medaglieres.Where(c => c.classifica == "2").Count();
            ViewBag.Bronzo = medaglieres.Where(c => c.classifica == "3").Count();
            return View(medaglieres.ToList());
        }

        // GET: Medaglieres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medagliere medagliere = db.Medaglieres.Find(id);
            if (medagliere == null)
            {
                return HttpNotFound();
            }
            return View(medagliere);
        }

        // GET: Medaglieres/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Evento_Id = new SelectList(db.Eventis.Where(g => g.NomeGara.Gara_Id != 19 && g.NomeGara.Gara_Id > 2).OrderByDescending(d => d.Data), "Evento_Id", "Titolo");
            return View();
        }

        // POST: Medaglieres/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Medagliere_Id,Evento_Id,Spec,classifica,Uid")] Medagliere medagliere)
        {
            if (ModelState.IsValid)
            {
                medagliere.Uid = User.Identity.GetUserId();
                db.Medaglieres.Add(medagliere);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Evento_Id = new SelectList(db.Eventis, "Evento_Id", "Titolo", medagliere.Evento_Id);
            return View(medagliere);
        }

        [Authorize]
        public ActionResult CreateUt(string id)
        {

            ViewBag.Bbb = "uuu!";
            ViewBag.Evento_Id = new SelectList(db.Eventis.Where(g => g.NomeGara.Gara_Id != 19 && g.NomeGara.Gara_Id > 2).OrderByDescending(d => d.Data), "Evento_Id", "Titolo");
            var utente = User.Identity.GetUserId();
            ViewBag.Utente = id;
            var medaglieres = db.Medaglieres.Include(m => m.Titolo).Where(m => m.Uid == id).OrderByDescending(d => d.Titolo.Data);
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateUt(string id, [Bind(Include ="Medagliere_Id,Evento_Id,Spec,Classifica,Uid")] Medagliere medagliere)
        {
            if (ModelState.IsValid)
            {
                medagliere.Uid = id;
                db.Medaglieres.Add(medagliere);
                db.SaveChanges();
                return RedirectToAction("Index", "UsersAdmin");
            }
            ViewBag.Evento_Id = new SelectList(db.Eventis.Where(g => g.NomeGara.Gara_Id != 19 && g.NomeGara.Gara_Id > 2).OrderByDescending(d => d.Data), "Evento_Id", "Titolo");
            return View(medagliere);
        }
        // GET: Medaglieres/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medagliere medagliere = db.Medaglieres.Find(id);
            if (medagliere == null)
            {
                return HttpNotFound();
            }
            ViewBag.Evento_Id = new SelectList(db.Eventis.Where(g=>g.NomeGara.Gara_Id != 19 && g.NomeGara.Gara_Id > 2).OrderByDescending(d=>d.Data), "Evento_Id", "Titolo", medagliere.Evento_Id);
            return View(medagliere);
        }

        // POST: Medaglieres/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Medagliere_Id,Evento_Id,Spec,classifica,Uid")] Medagliere medagliere)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medagliere).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Evento_Id = new SelectList(db.Eventis, "Evento_Id", "Titolo", medagliere.Evento_Id);
            return View(medagliere);
        }

        // GET: Medaglieres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medagliere medagliere = db.Medaglieres.Find(id);
            if (medagliere == null)
            {
                return HttpNotFound();
            }
            return View(medagliere);
        }

        // POST: Medaglieres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medagliere medagliere = db.Medaglieres.Find(id);
            db.Medaglieres.Remove(medagliere);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Palmares(string id)
        {
            var uid = id;
            ViewBag.Uid = uid;
            var utente = db.Users.Where(u=>u.Id == id );
            ViewBag.Utente = utente;
            var medaglieres = db.Medaglieres.Include(m => m.Titolo).Where(m => m.Uid == id).OrderByDescending(d=>d.Titolo.Data);
            ViewBag.MedCount = medaglieres.Count();
            ViewBag.Oro = medaglieres.Where(c => c.classifica == "1").Count();
            ViewBag.Argento = medaglieres.Where(c => c.classifica == "2").Count();
            ViewBag.Bronzo = medaglieres.Where(c => c.classifica == "3").Count();
            return View(medaglieres.ToList());
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
