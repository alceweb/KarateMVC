using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KarateMVC.Models;

namespace KarateMVC.Controllers
{
    public class SquadresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Squadres
        public ActionResult Index()
        {
            var squadre = db.Squadres.OrderByDescending(a=>a.Anno).ToList();
            var medaglie = db.Medaglieres.OrderByDescending(a => a.classifica).ToList();
            ViewBag.Medaglie = medaglie;
            return View(squadre);
        }

        // GET: Squadres
        public ActionResult IndexUt()
        {
            var dettaglio = db.SquadraDetts.OrderBy(n => n.Nome.Nome).ToList();
            var squadre = db.Squadres.OrderByDescending(a => a.Anno).ToList();
            var palmares = db.Medaglieres.ToList();
            ViewData["Palmares"] = palmares;
            ViewBag.Palmares = palmares;
            ViewBag.Dettaglio = dettaglio;
            return View(squadre);
        }

        // GET: Squadres/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Squadre squadre = db.Squadres.Find(id);
            var allievi = db.SquadraDetts.Where(a=>a.Squadra_id==id);
            ViewBag.Allievi = allievi;
            if (squadre == null)
            {
                return HttpNotFound();
            }
            return View(squadre);
        }

        [Authorize(Roles = "Admin")]
        // GET: Squadres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Squadres/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Squadra_id,Anno,NomeSquadra")] Squadre squadre)
        {
            if (ModelState.IsValid)
            {
                db.Squadres.Add(squadre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(squadre);
        }

        // GET: Squadres/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Squadre squadre = db.Squadres.Find(id);
            if (squadre == null)
            {
                return HttpNotFound();
            }
            return View(squadre);
        }

        // POST: Squadres/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Squadra_id,Anno,NomeSquadra")] Squadre squadre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(squadre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(squadre);
        }

        // GET: Squadres/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Squadre squadre = db.Squadres.Find(id);
            if (squadre == null)
            {
                return HttpNotFound();
            }
            return View(squadre);
        }

        // POST: Squadres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Squadre squadre = db.Squadres.Find(id);
            db.Squadres.Remove(squadre);
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
