using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class AspNetContexto : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Locality> Localities { get; set; }

        public System.Data.Entity.DbSet<Proyecto.Models.Image> Images { get; set; }
    }
}