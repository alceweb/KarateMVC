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
            var eventi = db.Eventis.Where(p => p.Pubblica == true);
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

            public ActionResult Palestra()
        {
            ViewBag.Message = "Palestra";
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Palestra/"));
            ViewBag.Immagini = immagini.ToList();
            return View();
        }

        public ActionResult Eventi()
        {
            ViewBag.Message = "Vita della palestra";
            var eventi = db.Eventis.Where(g => g.Galleria == true);
            return View(eventi);
        }

        public ActionResult Appuntamenti()
        {
            ViewBag.Message = "Appuntamenti";
            var eventi = db.Eventis.Where(d=>d.Data > DateTime.Now);
            if (eventi == null)
            {
                return HttpNotFound();
            }
            return View(eventi);
        }

        public ActionResult Download()
        {
            ViewBag.Message = "Download";
            return View();
        }

        public ActionResult Agonisti()
        {
            ViewBag.Message = "Area agonisti";
            return View();
        }
    }
}