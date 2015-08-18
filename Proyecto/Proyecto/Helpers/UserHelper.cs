using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Helpers
{
    public static class UserHelper
    {
        public static User GetUser(this System.Security.Principal.IIdentity identity){

            if (identity.IsAuthenticated) {
                using (var db = new ApplicationDbContext())
                {
                    return db.Users.FirstOrDefault(x => x.UserName == identity.Name);
                }
            }
            return null;
        }
    }
}