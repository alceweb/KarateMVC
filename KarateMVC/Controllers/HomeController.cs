using KarateMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.IO;

namespace KarateMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var eventi = db.Eventis.Where(p => p.Pubblica == true).OrderByDescending(d=>d.Data);
            return View(eventi.ToList());
        }

        public ActionResult Evento(int? id)
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

        public ActionResult Corsi()
        {
            ViewBag.Message = "Corsi";
            return View();
        }

            public async Task<ActionResult> Palestra()
        {
            ViewBag.Message = "Palestra";
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Palestra/"));
            ViewBag.Immagini = immagini.ToList();
            // Get the list of Users in this Role
            var agonisti = new List<ApplicationUser>();
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, "Agonista"))
                {
                    agonisti.Add(user);
                }
            }
            ViewBag.Agonisti = agonisti;

            return View(await UserManager.Users.ToListAsync());
        }

        public ActionResult Eventi()
        {
            ViewBag.CountEventi = db.Eventis.Count();
            ViewBag.Message = "Vita della palestra";
            var eventi = db.Eventis.Where(g => g.Galleria == true).OrderByDescending(d=>d.Data);
            return View(eventi);
        }

        public ActionResult Appuntamenti()
        {
            ViewBag.Message = "Appuntamenti";
            var eventi = db.Eventis.Where(d=>d.Data > DateTime.Now && d.Gara_Id != 19).OrderBy(d=>d.Data);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            return View(eventi);
        }
        public ActionResult Download()
        {
            ViewBag.Message = "Download";
            var documenti = db.Documentis.OrderByDescending(i => i.Id);
            return View(documenti);
        }

        public ActionResult Agonisti()
        {
            ViewBag.Message = "Area agonisti";
            return View();
        }

        public ActionResult Credits()
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

        [HttpPost]

        public ActionResult test(int? id)
        {
            System.IO.Directory.CreateDirectory(Server.MapPath("~/Content/ciao"));
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult NewsLetter()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewsLetter(NewsLetterFormModel model)
        {
            if (ModelState.IsValid)
            {
                var message = new MailMessage();
                message.From = new MailAddress("webservice@shotokenshukai.eu");
                message.To.Add(new MailAddress(model.Destinatario));  
                message.Subject = "News letter Shotokenshukai";
                message.Body = string.Format(model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    ViewBag.Message = "Newsletter inviata correttamente";
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult Palmares(string id)
        {
            var utente = id.ToString();
            ViewBag.Utente = id;
            return View();
        }
        [Authorize(Roles ="Special")]
        public ActionResult Special()
        {
            return View();
        }
    }
}