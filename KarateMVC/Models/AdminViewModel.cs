using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace KarateMVC.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Cognome")]
        public string Cognome { get; set; }
        [Display(Name = "Data di nascita")]
        public DateTime DataNascita { get; set; }
        [Display(Name = "Data inizio attività")]
        public DateTime DataInizio { get; set; }
        [Display(Name = "Grado")]
        public string Grado { get; set; }
        [Display(Name = "La tua frase")]
        public string Frase { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
    public class EditUsViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Cognome")]
        public string Cognome { get; set; }
        [Display(Name ="Data di nascita")]
        public DateTime DataNascita { get; set; }
        [Display(Name ="Data inizio attività")]
        public DateTime DataInizio { get; set; }
        [Display(Name ="Grade")]
        public string Grado { get; set; }
        [Display(Name ="La tua frase")]
        public string Frase { get; set; }


        public IEnumerable<SelectListItem> RolesList { get; set; }
    }

}