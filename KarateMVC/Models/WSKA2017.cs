using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KarateMVC.Models
{
    public class WSKA2017
    {
        [Key]
        public int Wska2017_id { get; set; }
        [Display(Name ="Numero biglietti")]
        public int Numero { get; set; }
        [Display(Name ="Data prenotazione")]
        public DateTime DataP { get; set; }
        [Display(Name ="Nome")]
        public string Nome { get; set; }
        [Display(Name = "Cognome")]
        public string Cognome { get; set; }
        [Display(Name ="Opzione giorni")]
        public string Giorni { get; set; }
        public int Costo { get; set; }
        public int Totale { get; set; }
        public bool Pagato { get; set; }
        public string UId { get; set; }
    }
}