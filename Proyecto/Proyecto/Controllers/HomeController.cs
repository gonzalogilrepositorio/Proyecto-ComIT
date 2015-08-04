﻿using PagedList;
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
        public ActionResult Index(int page = 1, bool async = false)
        {
            ViewBag.Categorias = ListadoCategorias();
            ViewBag.Localidades = ListadoLocalidades();
            ViewBag.Tipos = ListadoTipoPropiedades();

            if (async)
            {
                return PartialView("_List", ListadoPropiedades(page));
            }
            else
                return View(ListadoPropiedades(page));
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
            try
            {
                using (var db = new AspNetContexto())
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
        public IPagedList<Property> ListadoPropiedades(int page)
        {
            using (var db = new AspNetContexto())
            {
                var data = (from p in db.Properties
                            orderby p.FechaPublicacion descending
                            select p);
                return data.ToPagedList(page, 3);
            }
        }
    }
}