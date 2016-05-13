using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace KarateMVC.Models
{
    // È possibile aggiungere dati di profilo dell'utente specificando altre proprietà della classe ApplicationUser. Per ulteriori informazioni, visitare http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }

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
        public int Id { get; set; }
        [Display(Name = "Titolo")]
        public string Titolo { get; set; }
        [Display(Name = "Descrizione")]
        [DataType(DataType.Text)]
        public string Descrizione { get; set; }
        [Display(Name = "Data evento")]
        [DataType(DataType.Date)]
        public DateTime  Data { get; set; }
        [Display(Name = "Pubblica")]
        public bool Pubblica { get; set; }
        [Display(Name = "Approfondimento")]
        public string Approfondimento { get; set; }
        [Display(Name = "Galleria")]
        public bool Galleria { get; set; }
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

    }
}