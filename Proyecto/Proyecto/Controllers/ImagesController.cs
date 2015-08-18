using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using System.IO;

namespace Proyecto.Controllers
{
    public class ImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Images
        public ActionResult Index(int idPropiedad)
        {
            var images = db.Images.Include(i => i.Property).Where(i => i.PropertyId==idPropiedad);
            return View(images.ToList());
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Titulo");
            return View();
        }

        // POST: Images/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase[] uploadfile1, int idPropiedad)
        {
            string myGuid;
            var imagen = new Image();

            if (uploadfile1 != null)
            {
                foreach (var file in uploadfile1.Where(x => x != null))
                {
                    string extension = Path.GetExtension(file.FileName);
                    myGuid = Guid.NewGuid().ToString();
                    var rutaCompleta = Path.Combine(Server.MapPath("~/Images"), myGuid + extension);

                    file.SaveAs(rutaCompleta);

                    imagen.ImagenUrl = myGuid + extension;//se guarda SOLO el nombre de la imagen, ya se sabe que todas las img estan en la carpeta 'Images'
                    imagen.PropertyId = idPropiedad;
                    
                    db.Images.Add(imagen);
                    db.SaveChanges();
                }

                
            }
            return View();
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image imagen = db.Images.Find(id);
            if (imagen == null)
            {
                return HttpNotFound();
            }
            return View(imagen);
        }

        // POST: Images/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImagenUrl,PropertyId")] Image imagen, HttpPostedFileBase file)
        {
            string myGuid;
            if (ModelState.IsValid)
            {
                string extension = Path.GetExtension(file.FileName);
                myGuid = Guid.NewGuid().ToString();
                var rutaCompleta = Path.Combine(Server.MapPath("~/Images"), myGuid + extension);

                System.IO.File.Delete(Server.MapPath("~/Images/" + imagen.ImagenUrl));
                file.SaveAs(rutaCompleta);
                imagen.ImagenUrl = rutaCompleta;

                db.Entry(imagen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Titulo");
            return View();
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            //lo borro fisicamente
            var file = Path.Combine(HttpContext.Server.MapPath("/Images"), image.ImagenUrl);
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);

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
