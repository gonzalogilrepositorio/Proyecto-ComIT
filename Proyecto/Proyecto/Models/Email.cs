using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Proyecto.Models
{
    public class Email
    {
        [Required, Display(Name = "Tu Nombre")]
        public string FromName { get; set; }
        [Required, Display(Name = "Tu Telefono")]
        public string FromPhone { get; set; }
        [Required, Display(Name = "Tu email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        public string Message { get; set; }
    }
}