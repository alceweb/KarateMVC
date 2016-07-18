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
    public class SquadraDettsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SquadraDetts
        public ActionResult Index()
        {
            var squadraDetts = db.SquadraDetts.Include(s => s.Nome).Include(s => s.NomeSquadra);
            return View(squadraDetts.ToList());
        }

        public ActionResult Index1(int? id)
        {
            var squadre = db.SquadraDetts.Include(s => s.Nome).Include(s => s.NomeSquadra).Where(s=>s.Squadra_id == id);
            ViewBag.Squadre=squadre;
            return View();
        }

        // GET: SquadraDetts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquadraDett squadraDett = db.SquadraDetts.Find(id);
            if (squadraDett == null)
            {
                return HttpNotFound();
            }
            return View(squadraDett);
        }

        // GET: SquadraDetts/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "Nome");
            var squadre = db.Squadres.OrderBy(a => a.Anno).Select(s=> new SelectListItem { Value = s.Squadra_id.ToString(), Text = s.Anno + " - " + s.NomeSquadra });
            ViewBag.Squadra_id = new SelectList(squadre, "Value", "Text" );
            return View();
        }

        // POST: SquadraDetts/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SquadraDett_id,Squadra_id,Id")] SquadraDett squadraDett)
        {
            if (ModelState.IsValid)
            {
                db.SquadraDetts.Add(squadraDett);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Nome", squadraDett.Id);
            ViewBag.Squadra_id = new SelectList(db.Squadres, "Squadra_id", "NomeSquadra");
            return View(squadraDett);
        }

        // GET: SquadraDetts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquadraDett squadraDett = db.SquadraDetts.Find(id);
            if (squadraDett == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Nome", squadraDett.Id);
            ViewBag.Squadra_id = new SelectList(db.Squadres, "Squadra_id", "NomeSquadra", squadraDett.Squadra_id);
            return View(squadraDett);
        }

        // POST: SquadraDetts/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SquadraDett_id,Squadra_id,Id")] SquadraDett squadraDett)
        {
            if (ModelState.IsValid)
            {
                db.Entry(squadraDett).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Nome", squadraDett.Id);
            ViewBag.Squadra_id = new SelectList(db.Squadres, "Squadra_id", "NomeSquadra", squadraDett.Squadra_id);
            return View(squadraDett);
        }

        public ActionResult EditS(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var squadre = db.SquadraDetts.Where(s => s.Squadra_id == id).ToList();
            ViewBag.Squadre = squadre;
            return View();
        }
        // GET: SquadraDetts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquadraDett squadraDett = db.SquadraDetts.Find(id);
            if (squadraDett == null)
            {
                return HttpNotFound();
            }
            return View(squadraDett);
        }

        // POST: SquadraDetts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SquadraDett squadraDett = db.SquadraDetts.Find(id);
            db.SquadraDetts.Remove(squadraDett);
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
