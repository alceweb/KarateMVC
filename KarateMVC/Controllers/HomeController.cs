using KarateMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ActionResult Index()
        {
            var avviso = db.Eventis.Where(e => e.Gara_Id == 20 && e.DataP <= DateTime.Today && e.DataF >= DateTime.Today);
            var eventi = db.Eventis.Where(p => p.Pubblica == true).OrderByDescending(d=>d.Data);
            ViewBag.AvvisoCount = avviso.Count();
            ViewBag.Avviso = avviso;
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
            var squadre = RoleManager.Roles.Where(r=>r.Name.Contains("Squadra"));
            var roles= new List<ApplicationUser>();
            ViewBag.Roles = roles;
            ViewBag.Squadre = squadre;
            var maestro = new List<ApplicationUser>();
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, "Maestro"))
                {
                    maestro.Add(user);
                }
            }
            ViewBag.Maestro = maestro;
            var istruttori = new List<ApplicationUser>();
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, "Istruttore"))
                {
                    istruttori.Add(user);
                }
            }
            ViewBag.Istruttori = istruttori;
            var agonisti = new List<ApplicationUser>();
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, "Agonista"))
                {
                    agonisti.Add(user);
                }
            }
            ViewBag.Agonisti = agonisti;
            var ex = new List<ApplicationUser>();
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, "Ex agonista"))
                {
                    ex.Add(user);
                }
            }
            ViewBag.Ex = ex;
            return View(await UserManager.Users.ToListAsync());
        }

        public ActionResult Squadre()
        {
            ViewBag.Roles = RoleManager.Roles.ToList().Where(n=>n.Name.Contains("Squadra")).OrderByDescending(n=>n.Name);
            ViewBag.RolesCount = RoleManager.Roles.Count();
            return View();

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
            var oggi = DateTime.Today;
            var eventi = db.Eventis.Where(d=>d.Data >= oggi && d.Gara_Id != 19).OrderBy(d=>d.Data);
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

        public ActionResult Palmares()
        {
            var medaglieres = db.Medaglieres.Include(m => m.Titolo).OrderByDescending(d => d.Titolo.NomeGara.Fikta == true);
            ViewBag.Oro = medaglieres.Where(c => c.classifica == "1").Count();
            ViewBag.Argento = medaglieres.Where(c => c.classifica == "2").Count();
            ViewBag.Bronzo = medaglieres.Where(c => c.classifica == "3").Count();
            return View(medaglieres.ToList());
        }
        [Authorize(Roles ="Special")]
        public ActionResult Special()
        {
            return View();
        }

        public async Task<ActionResult> Scheda(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(Directory.Exists(Server.MapPath("~/Content/Immagini/FotoPersonali/" + id + "/"))){
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/FotoPersonali/" + id + "/"));
            ViewBag.Immagini = immagini.ToList();

            }
            var user = await UserManager.FindByIdAsync(id);
            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        public async Task<ActionResult> Album(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Directory.Exists(Server.MapPath("~/Content/Immagini/FotoPersonali/" + id + "/")))
            {
                var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/FotoPersonali/" + id + "/"));
                ViewBag.Immagini = immagini.ToList();
            }
            var user = await UserManager.FindByIdAsync(id);
            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Album(IEnumerable<HttpPostedFile> files, string id)
        {
            if (!Directory.Exists(Server.MapPath("~/Content/Immagini/FotoPersonali/" + id + "/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Content/Immagini/FotoPersonali/" + id + "/"));
            }
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Immagini/FotoPersonali/" + id + "/"), fileName);
                    WebImage img = new WebImage(file.InputStream);
                    var larghezza = img.Width;
                    var altezza = img.Height;
                    var rapportoO = larghezza/altezza;
                    var rapportoV = altezza/larghezza;
                    if (altezza > 1900 | larghezza > 1900)
                    {
                        if(rapportoO >= 1)
                        {
                            ViewBag.Message = "Attendi la fine del download...";
                            img.Resize(1900, 1900/rapportoO);
                            img.Save(path);
                            ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                        }
                        else
                        {
                            img.Resize(800/rapportoV, 800);
                            img.Save(path);
                            ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                        }
                    }
                    else
                    {
                        img.Save(path);
                        ViewBag.Message = "Download immagine avvenuto con successo Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                    }
                    //var fileName = Path.GetFileName(file.FileName);
                    //var path = Path.Combine(Server.MapPath("~/Content/Immagini/FotoPersonali/" + id + "/"), fileName);
                    //file.SaveAs(path);
                    //ViewBag.Message = "File scaricato con successo";
                }
                else
                {
                    ViewBag.Message = "Devi scegliere un file";
                }
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/FotoPersonali/" + id + "/"));
            ViewBag.Immagini = immagini.ToList();
            var user = UserManager.FindById(id);
            ViewBag.RoleNames = UserManager.GetRoles(user.Id);
            return View(user);
        }

        public ActionResult DelImgAlbum(string id, string filename)
        {
            ViewBag.Uid = id;
            ViewBag.Message = filename;
            return View();
        }

        [HttpPost, ActionName("DelImgAlbum")]
        public ActionResult DelImgAlbumConfirmed(string id, string filename)
        {
            var path = Server.MapPath("~/Content/Immagini/FotoPersonali/" + id +"/" + filename);
            System.IO.File.Delete(path);
            return RedirectToAction("Album", new {id = id });
        }

    }
}