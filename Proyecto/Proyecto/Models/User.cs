using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Proyecto.Models
{
    public class User: IdentityUser
    {
        public User()
        {
            Properties = new List<Property>();
        }

        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Adreess { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Phone2 { get; set; }

        public List<Property> Properties { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar reclamaciones de usuario personalizado aquí
            return userIdentity;
        }
    }
}