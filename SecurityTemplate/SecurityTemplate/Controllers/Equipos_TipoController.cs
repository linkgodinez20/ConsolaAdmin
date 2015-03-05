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
    public class Equipos_TipoController : Controller
    {        
        private readonly IRepo<Equipos_tipo> repo;

        public Equipos_TipoController(IRepo<Equipos_tipo> Repo)
        {
            this.repo = Repo;
        }

        // GET: /Equipos_Tipo/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TipoEquipo = String.IsNullOrEmpty(sortOrder) ? "TipoEquipo_desc" : "";
          
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Equipos_tipo> equipTip = repo.GetAll().OrderBy(x => x.Nombre);

            var modelo = from s in equipTip select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Nombre.Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "TipoEquipo":
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
                case "TipoEquipo_desc":
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

        // GET: /Equipos_Tipo/Details/5
        public ActionResult Details(byte id = 0)
        {
            Equipos_tipo equipos_tipo = repo.Get(id);
            if (equipos_tipo == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", equipos_tipo);
        }

        // GET: /Equipos_Tipo/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: /Equipos_Tipo/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_EquipoTipo,Nombre")] Equipos_tipo equipos_tipo)
        {
            if (ModelState.IsValid)
            {
                repo.Add(equipos_tipo);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Equipos_Tipo");
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create", equipos_tipo);
        }

        // GET: /Equipos_Tipo/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Equipos_tipo equipos_tipo = repo.Get(id);
            if (equipos_tipo == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", equipos_tipo);
        }

        // POST: /Equipos_Tipo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_EquipoTipo,Nombre")] Equipos_tipo equipos_tipo)
        {
            if (ModelState.IsValid)
            {
                repo.Update(equipos_tipo);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Equipos_Tipo");
                return Json(new { success = true, url = url });   
            }
            return PartialView("_Edit", equipos_tipo);
        }

        // GET: /Equipos_Tipo/Delete/5
        public ActionResult Delete(byte id = 0)
        {
            Equipos_tipo equipos_tipo = repo.Get(id);
            if (equipos_tipo == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", equipos_tipo);
        }

        // POST: /Equipos_Tipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            try
            {
                Equipos_tipo equipos_tipo = repo.Get(id);
                repo.Delete(equipos_tipo);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Equipos_Tipo", new { id = equipos_tipo.Id_EquipoTipo });
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
