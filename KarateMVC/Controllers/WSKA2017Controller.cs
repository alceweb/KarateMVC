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
using System.Net.Mail;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace KarateMVC.Controllers
{
    [Authorize]
    public class WSKA2017Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: WSKA2017
        public ActionResult Index()
        {
            var uid = User.Identity.GetUserId();
            ViewBag.Uid = uid;
            ViewBag.Count = db.WSKA2017s.Where(w => w.UId == uid).Count();
            return View(db.WSKA2017s.ToList());
        }

        // GET: WSKA2017/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WSKA2017 wSKA2017 = db.WSKA2017s.Find(id);
            if (wSKA2017 == null)
            {
                return HttpNotFound();
            }
            return View(wSKA2017);
        }

        // GET: WSKA2017/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WSKA2017/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Wska2017_id,Numero,DataP,Giorni,Costo,Totale,Pagato,UId,Nome,Cognome")] WSKA2017 wSKA2017)
        {
         if (ModelState.IsValid)
            {
                 var tot = wSKA2017.Numero * wSKA2017.Costo;
                ApplicationUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
                wSKA2017.Nome = user.Nome;
                wSKA2017.Cognome = user.Cognome;
                wSKA2017.DataP = DateTime.Now;
                wSKA2017.UId = User.Identity.GetUserId();
                wSKA2017.Totale = tot;
               db.WSKA2017s.Add(wSKA2017);
                db.SaveChanges();
                var message = new MailMessage();
                message.From = new MailAddress("webservice@shotokenshukai.eu");
                message.To.Add(new MailAddress("cesare@cr-consult.eu"));
                message.Subject = "Prenotazione biglietti WSKA 2017";
                message.Body = "Il giorno <strong>" + DateTime.Now +
                    "</strong><br/><strong>" +
                    wSKA2017.Nome + " " + wSKA2017.Cognome + " [<em>" + User.Identity.GetUserName() + "</em>] " +
                    "</strong><br/>ha inserito una nuova prenotazione biglietti per WSKA 2017.<hr/><strong>Data:</strong> " +
                    wSKA2017.DataP +
                     "<br/><strong>Numero biglietti:</strong> " +
                     wSKA2017.Numero +
                     "<br/><strong>Opzione giorni:</strong> " +
                     wSKA2017.Giorni +
                     "<br/><strong>Costo:</strong> € " +
                     wSKA2017.Costo +
                     "<br/><strong>Totale:</strong>  € " +
                     wSKA2017.Totale;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }

                return RedirectToAction("Index");
            }

            return View(wSKA2017);
        }

        // GET: WSKA2017/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WSKA2017 wSKA2017 = db.WSKA2017s.Find(id);
            if (wSKA2017 == null)
            {
                return HttpNotFound();
            }
            return View(wSKA2017);
        }

        // POST: WSKA2017/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Wska2017_id,Numero,DataP,Giorni,Costo,Totale,Pagato,UId,Nome,Cognome")] WSKA2017 wSKA2017)
        {
            if (ModelState.IsValid)
            {
                var tot = wSKA2017.Numero * wSKA2017.Costo;
                ApplicationUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
                wSKA2017.Nome = user.Nome;
                wSKA2017.Cognome = user.Cognome;
                wSKA2017.Totale = tot;
                wSKA2017.DataP = DateTime.Now;
                db.Entry(wSKA2017).State = EntityState.Modified;
                db.SaveChanges();
                var message = new MailMessage();
                message.From = new MailAddress("webservice@shotokenshukai.eu");
                message.To.Add(new MailAddress("cesare@cr-consult.eu"));
                message.Subject = "Modifica prenotazione biglietti WSKA 2017";
                message.Body = "Il giorno <strong>" + DateTime.Now +
                    "</strong><br/><strong>" +
                    wSKA2017.Nome + " " + wSKA2017.Cognome + " [<em>" + User.Identity.GetUserName() + "</em>] " +
                    "</strong><br/>ha modificato la prenotazione biglietti per WSKA 2017.<hr/><strong>Data:</strong> " +
                    wSKA2017.DataP +
                     "<br/><strong>Numero biglietti:</strong> " +
                     wSKA2017.Numero +
                     "<br/><strong>Opzione giorni:</strong> " +
                     wSKA2017.Giorni +
                     "<br/><strong>Costo:</strong> € " +
                     wSKA2017.Costo +
                     "<br/><strong>Totale:</strong> € " +
                     wSKA2017.Totale;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }


                return RedirectToAction("Index");
            }
            return View(wSKA2017);
        }

        // GET: WSKA2017/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WSKA2017 wSKA2017 = db.WSKA2017s.Find(id);
            if (wSKA2017 == null)
            {
                return HttpNotFound();
            }
            return View(wSKA2017);
        }

        // POST: WSKA2017/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WSKA2017 wSKA2017 = db.WSKA2017s.Find(id);
            db.WSKA2017s.Remove(wSKA2017);
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
