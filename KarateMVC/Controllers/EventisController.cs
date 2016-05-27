using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KarateMVC.Models;
using System.Web.Helpers;
using System.IO;

namespace KarateMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EventisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Eventis
        public ActionResult Index()
        {
            ViewBag.EventiCount = db.Eventis.Count();
            var eventi = db.Eventis.OrderByDescending(d => d.Data);
            return View(eventi);
        }

        // GET: Eventis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventi eventi = db.Eventis.Find(id);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            return View(eventi);
        }

        // GET: Eventis/Create
        public ActionResult Create()
        {
            ViewBag.Gara_Id = new SelectList(db.Gares, "Gara_Id", "NomeGara");
            return View();
        }
        
        // POST: Eventis/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Gara_Id,Titolo,Data,Pubblica,Approfondimento,Galleria", Exclude ="Descrizione")] Eventi eventi)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            eventi.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                db.Eventis.Add(eventi);
                db.SaveChanges();
                    Directory.CreateDirectory(Server.MapPath("~/Content/Immagini/Eventi/" + eventi.Evento_Id));
                return RedirectToAction("Index");
            }
            ViewBag.Gara_Id = new SelectList(db.Gares, "Gara_Id", "NomeGara", eventi.Gara_Id);
            return View();
        }

        // GET: Eventis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventi eventi = db.Eventis.Find(id);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gara_Id = new SelectList(db.Gares, "Gara_Id", "NomeGara", eventi.Gara_Id);
            return View(eventi);
        }

        // POST: Eventis/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Evento_Id,Gara_Id,Titolo,Data,Pubblica,Approfondimento,Galleria", Exclude ="Descrizione")] Eventi eventi)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            eventi.Descrizione = collection["Descrizione"];

            if (ModelState.IsValid)
            {
                db.Entry(eventi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Gara_Id = new SelectList(db.Gares, "Gara_Id", "NomeGara", eventi.Gara_Id);
            return View(eventi);
        }

        public ActionResult EditImgP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Eventi/" + id + "/"));
            ViewBag.Immagini = immagini.ToList();
            Eventi eventi = db.Eventis.Find(id);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            return View(eventi);
        }

        [HttpPost]
        public ActionResult EditImgP(HttpPostedFileBase file, int? id)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("/Content/Immagini/Eventi/" + id + "/" + id + ".jpg").ToLower());
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "Devi scegliere un file";
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Eventi/" + id + "/"));
            ViewBag.Immagini = immagini.ToList();
            Eventi eventi = db.Eventis.Find(id);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            return View(eventi);

        }

        public ActionResult EditImg(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Eventi/" + id + "/"));
            ViewBag.Immagini = immagini.ToList();
            Eventi eventi = db.Eventis.Find(id);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            return View(eventi);
        }

        [HttpPost]
        public ActionResult EditImg(IEnumerable<HttpPostedFileBase> files, int? id)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                    try
                    {
                        file.SaveAs(Server.MapPath("/Content/Immagini/Eventi/" + id +"/" + file.FileName).ToLower());
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "Devi scegliere un file";
                }
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Eventi/" + id + "/"));
            ViewBag.Immagini = immagini.ToList();
            Eventi eventi = db.Eventis.Find(id);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            return View(eventi);

        }

        // GET: Eventis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventi eventi = db.Eventis.Find(id);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            return View(eventi);
        }

        // POST: Eventis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string[] files = Directory.GetFiles(Server.MapPath("~/Content/Immagini/Eventi/" + id));
            foreach (var file in files)
            {
                System.IO.File.Delete(Server.MapPath(file));
            }
            
            Directory.Delete(Server.MapPath("~/Content/Immagini/Eventi/" + id));
            Eventi eventi = db.Eventis.Find(id);
            db.Eventis.Remove(eventi);
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
