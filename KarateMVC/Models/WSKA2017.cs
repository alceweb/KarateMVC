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
        [Display(Name ="Data prenotazione")]
        public DateTime DataP { get; set; }
        [Display(Name ="Opzione giorni")]
        public int Giorni { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public float? Costo { get; set; }
        public bool Pagato { get; set; }
    }
}