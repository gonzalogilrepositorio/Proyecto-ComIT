using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImagenUrl { get; set; }

        public Property Propiedad { get; set; }
        public int IdProperty { get; set; }
    }
}