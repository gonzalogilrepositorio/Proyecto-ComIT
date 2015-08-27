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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Properties
        public ActionResult Index()
        {
            var properties = db.Properties.Include(p => p.Category).Include(p => p.Locality).Include(p => p.PropertyType).Include(p => p.User);
            return View(properties.ToList());
        }

        // GET: Properties/Details/5
        public ActionResult Details(int id)
        {
            Property property = db.Properties.Find(id);
            
            if (property == null)
            {
                return HttpNotFound();
            }

            var images = (from p in db.Images
                        where p.PropertyId == id
                        orderby p.Id ascending
                        select p).ToList();

            ViewBag.imageList = images;
            return View(property);
        }

        // GET: Properties/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Nombre");
            ViewBag.LocalityId = new SelectList(db.Localities, "Id", "Nombre");
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "Nombre");
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre");
            return View();
        }

        
        // POST: Properties1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public ActionResult Create(Property property)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(x => x.Email.ToLower() == User.Identity.Name.ToLower());

                if (user != null)
                {
                    user.Properties.Add(property);

                    //property.User = user;
                    //db.Properties.Add(property);
                }
                
                db.SaveChanges();
                
                return RedirectToAction("/../Images/Create", new { idProp = property.Id });
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Nombre", property.CategoryId);
            ViewBag.LocalityId = new SelectList(db.Localities, "Id", "Nombre", property.LocalityId);
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "Nombre", property.PropertyTypeId);
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", property.UserId);
            //return View("/../Views/Home/index");
            return RedirectToAction("/../Home/Index");
        }

        // GET: Properties/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            var username = User.Identity.Name;//obtengo usuario actual
            var prop = db.Properties.FirstOrDefault(x => x.Id == id && x.User.UserName.ToLower() == username.ToLower());//busco si usuario que intenta modificar la propiedad sea el mismo que la creo

            if (prop == null)
            {
                return Redirect("/?error=NotProperty");
            }

            //Property property = db.Properties.Find(id);
            //if (property == null)
            //{
            //    return HttpNotFound();
            //}
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Nombre", prop.CategoryId);
            ViewBag.LocalityId = new SelectList(db.Localities, "Id", "Nombre", prop.LocalityId);
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "Nombre", prop.PropertyTypeId);
            return View(prop);
        }

        // POST: Properties/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Nombre", property.UserId);
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
