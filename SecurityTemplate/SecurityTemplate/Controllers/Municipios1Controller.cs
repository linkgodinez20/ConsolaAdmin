using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Security.Core.Model;

namespace Security.Controllers
{
    public class Municipios1Controller : Controller
    {
        private SecurityEntities db = new SecurityEntities();

        // GET: Municipios1
        public ActionResult Index()
        {
            var municipios = db.Municipios.Include(m => m.Entidades);
            return View(municipios.ToList());
        }

        // GET: Municipios1/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipios municipios = db.Municipios.Find(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            return View(municipios);
        }

        // GET: Municipios1/Create
        public ActionResult Create()
        {
            ViewBag.Id_Pais = new SelectList(db.Entidades, "Id_Pais", "Nombre");
            return View();
        }

        // POST: Municipios1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Pais,Id_Entidad,Id_Municipio,Nombre,Abreviatura")] Municipios municipios)
        {
            if (ModelState.IsValid)
            {
                db.Municipios.Add(municipios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Pais = new SelectList(db.Entidades, "Id_Pais", "Nombre", municipios.Id_Pais);
            return View(municipios);
        }

        // GET: Municipios1/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipios municipios = db.Municipios.Find(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Pais = new SelectList(db.Entidades, "Id_Pais", "Nombre", municipios.Id_Pais);
            return View(municipios);
        }

        // POST: Municipios1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Pais,Id_Entidad,Id_Municipio,Nombre,Abreviatura")] Municipios municipios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Pais = new SelectList(db.Entidades, "Id_Pais", "Nombre", municipios.Id_Pais);
            return View(municipios);
        }

        // GET: Municipios1/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipios municipios = db.Municipios.Find(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            return View(municipios);
        }

        // POST: Municipios1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Municipios municipios = db.Municipios.Find(id);
            db.Municipios.Remove(municipios);
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
