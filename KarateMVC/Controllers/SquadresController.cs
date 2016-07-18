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
    [Authorize(Roles = "Admin")]
    public class SquadresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Squadres
        public ActionResult Index()
        {
            var query = db.SquadraDetts.Include(u => u.Nome);
            ViewBag.SquadreDett = query.ToList();
            var squadre = db.Squadres.ToList().OrderBy(a=>a.Anno);
            var allievi = db.SquadraDetts;
            ViewBag.SquadreCount = db.Squadres.Count();
            return View(squadre);
        }

        // GET: Squadres/Details/5
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

        // GET: Squadres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Squadres/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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
