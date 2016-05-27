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
    public class SpecsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Specs
        public ActionResult Index()
        {
            return View(db.Speci.ToList());
        }

        // GET: Specs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spec spec = db.Speci.Find(id);
            if (spec == null)
            {
                return HttpNotFound();
            }
            return View(spec);
        }

        // GET: Specs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Specs/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Specialità")] Spec spec)
        {
            if (ModelState.IsValid)
            {
                db.Speci.Add(spec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spec);
        }

        // GET: Specs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spec spec = db.Speci.Find(id);
            if (spec == null)
            {
                return HttpNotFound();
            }
            return View(spec);
        }

        // POST: Specs/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Specialità")] Spec spec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spec);
        }

        // GET: Specs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spec spec = db.Speci.Find(id);
            if (spec == null)
            {
                return HttpNotFound();
            }
            return View(spec);
        }

        // POST: Specs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spec spec = db.Speci.Find(id);
            db.Speci.Remove(spec);
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
