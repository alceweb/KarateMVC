using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace KarateMVC.Models
{
    // È possibile aggiungere dati di profilo dell'utente specificando altre proprietà della classe ApplicationUser. Per ulteriori informazioni, visitare http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        [Display(Name = "Data di nascita")]
        [DataType(DataType.Date)]
        public DateTime DataNascita { get; set; }
        public string AnnoInizio { get; set; }
        public string Grado { get; set; }
        public string Frase { get; set; }
        public string Kata { get; set; }
        public bool Maestro { get; set; }
        public bool Istruttore { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenere presente che il valore di authenticationType deve corrispondere a quello definito in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Aggiungere qui i reclami utente personalizzati
            return userIdentity;
        }

    }

    public class Eventi
    {
        [Key]
        public int Evento_Id { get; set; }
        [Display(Name="Tipo evento")]
        public int? Gara_Id { get; set; }
        public virtual Gare NomeGara{ get; set; }
        [Display(Name = "Nome evento")]
        public string Titolo { get; set; }
        [Display(Name = "Descrizione")]
        [DataType(DataType.Text)]
        public string Descrizione { get; set; }
        [Display(Name = "Data evento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        [Display(Name = "Inizio splashpage")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataP { get; set; }
        [Display(Name = "Fine splashpage")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataF { get; set; }
        [Display(Name = "Pubblica")]
        public bool Pubblica { get; set; }
        [Display(Name = "Approfondimento")]
        public string Approfondimento { get; set; }
        [Display(Name = "Galleria")]
        public bool Galleria { get; set; }
    }

    public class Gare
    {
        [Key]
        public int Gara_Id { get; set; }
        public string NomeGara { get; set; }
        [Display(Name= "FIKTA")]
        public bool Fikta { get; set; }

        public virtual ICollection<Eventi> Eventis { get; set; }

    }

    public class Documenti
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Titolo")]
        public string Titolo { get; set; }
        [Display(Name = "Descrizione")]
        public string descrizione { get; set; }
    }

    public class Spec
    {
        [Key]
        public int Id { get; set; }
        public string Specialità { get; set; }
    }

    public class Squadre
    {
        [Key]
        public int Squadra_id { get; set; }
        [Display(Name ="Anno")]
        public int Anno { get; set; }
        [Display(Name ="Nome squadra")]
        public string NomeSquadra { get; set; }

        public virtual ICollection<SquadraDett> Squadres { get; set; }

    }

    public class SquadraDett
    {
        [Key]
        public int SquadraDett_id { get; set; }
        public int Squadra_id { get; set; }
        public virtual Squadre NomeSquadra { get; set; }
        public string Id { get; set; }
        public virtual ApplicationUser Nome { get; set; }

    }

    public class Medagliere
    {
        [Key]
        public int Medagliere_Id { get; set; }
        public int Evento_Id { get; set; }
        public virtual Eventi Titolo { get; set; }
        public string Spec { get; set; }
        public string classifica { get; set; }
        public string Uid { get; set; }
        

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<KarateMVC.Models.Eventi> Eventis { get; set; }
        public DbSet<KarateMVC.Models.Documenti> Documentis { get; set; }
        public DbSet<KarateMVC.Models.Spec> Speci { get; set; }
        public DbSet<KarateMVC.Models.Gare> Gares { get; set; }
        public DbSet<KarateMVC.Models.Medagliere> Medaglieres { get; set; }
        public DbSet<KarateMVC.Models.Squadre> Squadres { get; set; }
        public DbSet<KarateMVC.Models.SquadraDett> SquadraDetts { get; set; }
    }
}