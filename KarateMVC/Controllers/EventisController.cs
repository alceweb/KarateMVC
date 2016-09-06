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
            ViewBag.Gara_Id = new SelectList(db.Gares.OrderBy(t => t.NomeGara), "Gara_Id", "NomeGara");
            return View();
        }
        
        // POST: Eventis/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Evento_Id,Gara_Id,Titolo,Data,DataP,DataF,Pubblica,Approfondimento,Galleria", Exclude ="Descrizione")] Eventi eventi)
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
            ViewBag.Gara_Id = new SelectList(db.Gares.OrderBy(t=>t.NomeGara), "Gara_Id", "NomeGara", eventi.Gara_Id);
            return View(eventi);
        }

        // POST: Eventis/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Evento_Id,Gara_Id,Titolo,Data,DataP,DataF,Pubblica,Approfondimento,Galleria", Exclude ="Descrizione")] Eventi eventi)
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
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Immagini/Eventi/" + id + "/"), id + ".jpg");
                    WebImage img = new WebImage(file.InputStream);
                    var larghezza = img.Width;
                    var altezza = img.Height;
                    var rapportoO = larghezza / altezza;
                    var rapportoV = altezza / larghezza;
                    if (altezza > 1900 | larghezza > 1900)
                    {
                        if (rapportoO >= 1)
                        {
                            ViewBag.Message = "Attendi la fine del download...";
                            img.Resize(1900, 1900 / rapportoO);
                            img.Save(path);
                            ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                        }
                        else
                        {
                            img.Resize(800 / rapportoV, 800);
                            img.Save(path);
                            ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                        }
                    }


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
                if (file != null)
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Immagini/Eventi/" + id + "/"), fileName);
                        WebImage img = new WebImage(file.InputStream);
                        var larghezza = img.Width;
                        var altezza = img.Height;
                        var rapportoO = larghezza / altezza;
                        var rapportoV = altezza / larghezza;
                        if (altezza > 1900 | larghezza > 1900)
                        {
                            if (rapportoO >= 1)
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Resize(1900, 1900 / rapportoO);
                                img.Save(path);
                                ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                            }
                            else
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Resize(800 / rapportoV, 800);
                                img.Save(path);
                                ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                            }
                        }
                        else
                        {
                            if (rapportoO >= 1)
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Save(path);
                                ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                            }
                            else
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Save(path);
                                ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                            }
                        }
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

        public ActionResult AvvisiProgrammati()
        {
            ViewBag.EventiCount = db.Eventis.Where(e => e.Gara_Id == 20 && e.DataP < DateTime.Now && e.DataF > DateTime.Now).Count();
            var eventi = db.Eventis.Where(e => e.Gara_Id == 20 && e.DataF > DateTime.Now).OrderByDescending(d => d.Data);
            return View(eventi);
        }
        public ActionResult AvvisiObsoletii()
        {
            ViewBag.EventiCount = db.Eventis.Where(e => e.Gara_Id == 20 && e.DataF < DateTime.Now).Count();
            var eventi = db.Eventis.Where(e => e.Gara_Id == 20 && e.DataF < DateTime.Now).OrderByDescending(d => d.Data);
            return View(eventi);
        }

    }
}
