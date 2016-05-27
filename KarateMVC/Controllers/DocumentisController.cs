using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KarateMVC.Models;
using System.IO;

namespace KarateMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DocumentisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documentis
        public ActionResult Index()
        {
            ViewBag.PdfCount = db.Documentis.Count();
            var documenti = db.Documentis.OrderByDescending(i => i.Id);
            return View(documenti);
        }

        // GET: Documentis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documenti documenti = db.Documentis.Find(id);
            if (documenti == null)
            {
                return HttpNotFound();
            }
            return View(documenti);
        }

        // GET: Documentis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documentis/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, [Bind(Include = "Id,Titolo,descrizione")] Documenti documenti)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0 && Path.GetExtension(file.FileName)== ".pdf")
                    try
                    {
                        db.Documentis.Add(documenti);
                        db.SaveChanges();
                        string estensione = Path.GetExtension(file.FileName);
                        string path = Path.Combine(Server.MapPath("/Content/Download"),
                                                   Path.GetFileName(documenti.Id.ToString() + estensione));
                        file.SaveAs(path);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "Non hai selezionato nessun file. Puoi inserire solo file PDF";
                }
            }

            return View(documenti);
        }

        // GET: Documentis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documenti documenti = db.Documentis.Find(id);
            if (documenti == null)
            {
                return HttpNotFound();
            }
            return View(documenti);
        }

        // POST: Documentis/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titolo,descrizione")] Documenti documenti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documenti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documenti);
        }

        // GET: Documentis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documenti documenti = db.Documentis.Find(id);
            if (documenti == null)
            {
                return HttpNotFound();
            }
            return View(documenti);
        }

        // POST: Documentis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Documenti documenti = db.Documentis.Find(id);
            db.Documentis.Remove(documenti);
            db.SaveChanges();
            string filepath = Request.MapPath("~/Content/Download/" + id + ".pdf");
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
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
