using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Security.Core.Model;
using PagedList;
using Security.Core.Repository;
using PagedList;

namespace Security.Controllers
{
    public class AccionesController : Controller
    {        
        private readonly IRepo<Acciones> repo;

        public AccionesController(IRepo<Acciones> Repo)
        {
            this.repo = Repo;
        }      

        // GET: Acciones
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Nombre = String.IsNullOrEmpty(sortOrder) ? "Nombre_desc" : "";            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Acciones> accions = repo.GetAll().OrderBy(x => x.Nombre);

            var modelo = from s in accions
                               select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Nombre":
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
                case "Nombre_desc":
                    modelo = modelo.OrderByDescending(s => s.Nombre);
                    break;            
                default:
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }

        // GET: Acciones/Details/5
        public ActionResult Details(int id = 0)
        {
            Acciones accions = repo.Get(id);
            if (accions == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", accions);
        }

        // GET: Acciones/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: Acciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Accion,Nombre,Estatus")] Acciones acciones)
        {
            if (ModelState.IsValid)
            {
                repo.Add(acciones);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Acciones");
                return Json(new { success = true, url = url });
            }

            return View(acciones);
        }

        // GET: Acciones/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Acciones acciones = repo.Get(id);
            if (acciones == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", acciones);
        }

        // POST: Acciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Accion,Nombre,Estatus")] Acciones acciones)
        {
            if (ModelState.IsValid)
            {                
                repo.Update(acciones);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Acciones");
                return Json(new { success = true, url = url });   
            }
            return PartialView("_Edit", acciones);
        }

        // GET: Acciones/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Acciones acciones = repo.Get(id);
            if (acciones == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", acciones);
        }

        // POST: Acciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Acciones acciones = repo.Get(id);
                repo.Delete(acciones);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Acciones", new { id = acciones.Id_Accion });
                return Json(new { success = true, url = url });
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Este registro no se puede eliminar por estar referenciado con otro registro.";
                ViewBag.True = 1;
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
