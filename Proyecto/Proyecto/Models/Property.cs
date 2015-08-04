using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public int Ambientes { get; set; }
        public int Baños { get; set; }
        public bool Cochera { get; set; }
        public bool GasNatural { get; set; }
        public bool Amoblado { get; set; }
        public bool AireAcond { get; set; }
        public bool Niños { get; set; }
        public bool Mascotas { get; set; }
        public double MtsCuadrados { get; set; }
        public double Precio { get; set; }
        public bool Estado { get; set; } //indica si esta disponible o no la propiedad
        public int TiempoRestante { get; set; } //indica el tiempo restante para que caduque la publicacion
        public DateTime FechaPublicacion { get; set; }

        public virtual Locality Locality { get; set; }
        public int LocalityId { get; set; } //indica la ciudad o pueblo donde se encuentra
        public virtual PropertyType PropertyType { get; set; }
        public int PropertyTypeId { get; set; } //indica si es un local, dpto, casa...
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; } //indica si es venta, alquiler temporario o alquiler mensual
        public virtual User User { get; set; }
        public int UserId { get; set; } //indica el usuario que publico la propiedad
    }
}