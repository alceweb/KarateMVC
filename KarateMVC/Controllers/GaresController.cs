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
    public class GaresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gares
        public ActionResult Index()
        {
            return View(db.Gares.ToList());
        }

        // GET: Gares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gare gare = db.Gares.Find(id);
            if (gare == null)
            {
                return HttpNotFound();
            }
            return View(gare);
        }

        // GET: Gares/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gares/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Gara_Id,NomeGara")] Gare gare)
        {
            if (ModelState.IsValid)
            {
                db.Gares.Add(gare);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gare);
        }

        // GET: Gares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gare gare = db.Gares.Find(id);
            if (gare == null)
            {
                return HttpNotFound();
            }
            return View(gare);
        }

        // POST: Gares/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Gara_Id,NomeGara")] Gare gare)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gare).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gare);
        }

        // GET: Gares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gare gare = db.Gares.Find(id);
            if (gare == null)
            {
                return HttpNotFound();
            }
            return View(gare);
        }

        // POST: Gares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gare gare = db.Gares.Find(id);
            db.Gares.Remove(gare);
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
