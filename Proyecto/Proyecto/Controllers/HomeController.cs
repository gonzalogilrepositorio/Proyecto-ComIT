using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Categorias = ListadoCategorias();
            ViewBag.Localidades = ListadoLocalidades();
            ViewBag.Tipos = ListadoTipoPropiedades();
            ViewBag.Propiedades = ListadoPropiedades();
            return View();
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

        //metodo creado para retornar la lista de categorias que tengo en la bd
        public List<Category> ListadoCategorias()
        {
            using (var db = new AspNetContexto())
            {
                return db.Categories.ToList();
            }
        }

        //metodo creado para retornar la lista de localidades que tengo en la bd
        public List<Locality> ListadoLocalidades()
        {
            using (var db = new AspNetContexto())
            {
                return db.Localities.ToList();
            }
        }

        //metodo creado para retornar la lista de tipo de propiedades que tengo en la bd
        public List<PropertyType> ListadoTipoPropiedades()
        {
            using (var db = new AspNetContexto())
            {
                return db.PropertyTypes.ToList();
            }
        }

        //metodo creado para retornar la lista de propiedades/publicaciones que tengo en la bd
        public List<Property> ListadoPropiedades()
        {
            using (var db = new AspNetContexto())
            {
                return db.Properties.ToList();
            }
        }
    }
}