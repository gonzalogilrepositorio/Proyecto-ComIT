using PagedList;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;//se agrego para enviar email
using System.Net.Mail;//se agrego para enviar email
using System.Threading.Tasks;//se agrego para enviar email, en el metodo contact

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int page = 1, bool async = false)
        {
            var db = new ApplicationDbContext();

            var user = db.Users.FirstOrDefault();

            ViewBag.Categorias = ListadoCategorias();
            ViewBag.Localidades = ListadoLocalidades();
            ViewBag.Tipos = ListadoTipoPropiedades();

            if (async)
            {
                return PartialView("_List", ListadoPropiedades(page));
            }
            else
            {
                var data = ListadoPropiedades(page);
                return View(data);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(Email model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Petición de Contacto de: {0} ({1})</p></br></br></br><p>Mensaje:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("alquiladosanluis@gmail.com"));  // a quien se lo envia tomarlo desde la propiedad enviar q copia el propiertario y al interesado
                message.From = new MailAddress("alquiladosanluis@gmail.com");  // quien lo envia
                message.Subject = "Peticion de Contacto: "+model.FromName;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "alquiladosanluis@gmail.com",  // replace with valid value
                        Password = "gonzaema123"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

        //metodo creado para retornar la lista de categorias que tengo en la bd
        public List<Category> ListadoCategorias()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.Categories.ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        //metodo creado para retornar la lista de localidades que tengo en la bd
        public List<Locality> ListadoLocalidades()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Localities.ToList();
            }
        }

        //metodo creado para retornar la lista de tipo de propiedades que tengo en la bd
        public List<PropertyType> ListadoTipoPropiedades()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.PropertyTypes.ToList();
            }
        }

        //metodo creado para retornar la lista de propiedades/publicaciones que tengo en la bd
        public IPagedList<Property> ListadoPropiedades(int page)
        {
            using (var db = new ApplicationDbContext())
            {
                //var data = (from p in db.Properties
                //            where p.IsValid == true
                //            orderby p.FechaPublicacion descending
                //            select p).ToList();

                return db.Properties.Include("Images").Where(x => x.Images.Count() > 0).OrderByDescending(x => x.FechaPublicacion).ToPagedList(page, 3);
            }
        }
    }
}