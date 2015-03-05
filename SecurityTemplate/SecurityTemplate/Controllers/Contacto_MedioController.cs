using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Security.Core.Model;
using Security.Core.Repository;
using PagedList;

namespace Security.Controllers
{
    public class Contacto_MedioController : Controller
    {
        private readonly IRepo<Contacto_medio> repo;       

        public Contacto_MedioController( IRepo<Contacto_medio> Repo)
        {
            this.repo = Repo;
        }

        // GET: /Contacto_Medio/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Contacto = String.IsNullOrEmpty(sortOrder) ? "Contacto_desc" : "";            

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Contacto_medio> contact_med = repo.GetAll().OrderBy(x => x.Nombre);

            var modelo = from s in contact_med select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Nombre.Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "Contacto":
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
                case "Contacto_desc":
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

        // GET: /Contacto_Medio/Details/5
        public ActionResult Details(byte id = 0)
        {
            Contacto_medio contacto_medio = repo.Get(id);
            if (contacto_medio == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", contacto_medio);
        }

        // GET: /Contacto_Medio/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: /Contacto_Medio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_ContactoMedio,Nombre")] Contacto_medio contacto_medio)
        {
            if (ModelState.IsValid)
            {
                repo.Add(contacto_medio);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Contacto_Medio");
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create", contacto_medio);
        }

        // GET: /Contacto_Medio/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Contacto_medio contacto_medio = repo.Get(id);
            if (contacto_medio == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", contacto_medio);
        }

        // POST: /Contacto_Medio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_ContactoMedio,Nombre")] Contacto_medio contacto_medio)
        {
            if (ModelState.IsValid)
            {
                repo.Update(contacto_medio);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Contacto_Medio");
                return Json(new { success = true, url = url });   
            }
            return PartialView("_Edit", contacto_medio);
        }

        // GET: /Contacto_Medio/Delete/5
        public ActionResult Delete(byte id)
        {
            Contacto_medio contacto_medio = repo.Get(id);
            if (contacto_medio == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", contacto_medio);
        }

        // POST: /Contacto_Medio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {           
            try
            {
                Contacto_medio contacto_medio = repo.Get(id);
                repo.Delete(contacto_medio);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Contacto_Medio", new { id = contacto_medio.Id_ContactoMedio });
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
