using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class PropertiesController : Controller
    {
        private AspNetContexto db = new AspNetContexto();

        // GET: Properties
        public ActionResult Index()
        {
            var properties = db.Properties.Include(p => p.Category).Include(p => p.Locality).Include(p => p.PropertyType).Include(p => p.User);
            return View(properties.ToList());
        }

        // GET: Properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Properties/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Nombre");
            ViewBag.LocalityId = new SelectList(db.Localities, "Id", "Nombre");
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");
            return View();
        }

        // POST: Properties/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descripcion,Direccion,Latitud,Longitud,Ambientes,Baños,Cochera,GasNatural,Amoblado,AireAcond,Niños,Mascotas,MtsCuadrados,Precio,Estado,TiempoRestante,FechaPublicacion,LocalityId,PropertyTypeId,CategoryId,UserId")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Nombre", property.CategoryId);
            ViewBag.LocalityId = new SelectList(db.Localities, "Id", "Nombre", property.LocalityId);
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "Nombre", property.PropertyTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", property.UserId);
            return View(property);
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Nombre", property.CategoryId);
            ViewBag.LocalityId = new SelectList(db.Localities, "Id", "Nombre", property.LocalityId);
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "Nombre", property.PropertyTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", property.UserId);
            return View(property);
        }

        // POST: Properties/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descripcion,Direccion,Latitud,Longitud,Ambientes,Baños,Cochera,GasNatural,Amoblado,AireAcond,Niños,Mascotas,MtsCuadrados,Precio,Estado,TiempoRestante,FechaPublicacion,LocalityId,PropertyTypeId,CategoryId,UserId")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Nombre", property.CategoryId);
            ViewBag.LocalityId = new SelectList(db.Localities, "Id", "Nombre", property.LocalityId);
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "Nombre", property.PropertyTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", property.UserId);
            return View(property);
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
