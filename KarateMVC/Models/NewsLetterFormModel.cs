using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KarateMVC.Models
{
    public class NewsLetterFormModel
    {
        [Required, Display(Name = "Destinazione")]
        public string Destinatario { get; set; }
        [Required, Display(Name ="Testo newsletter")]
        [AllowHtml]
        public string Message { get; set; }
    }
}