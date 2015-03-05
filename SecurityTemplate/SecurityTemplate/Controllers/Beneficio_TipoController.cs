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
    public class Beneficio_TipoController : Controller
    {
        private readonly IRepo<Beneficio_tipo> repo;

        public Beneficio_TipoController(IRepo<Beneficio_tipo> Repo) 
        {
            this.repo = Repo;
        }

        // GET: /Beneficio_Tipo/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Beneficio = String.IsNullOrEmpty(sortOrder) ? "Beneficio_desc" : "";
            ViewBag.Beneficio = sortOrder == "Beneficio" ? "Beneficio" : "Beneficio_desc";            

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Beneficio_tipo> beneficio_tip = repo.GetAll().OrderBy(x => x.Nombre);

            var modelo = from s in beneficio_tip select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Beneficio":
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
                case "Beneficio_desc":
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

        // GET: /Beneficio_Tipo/Details/5
        public ActionResult Details(int id = 0)//Byte
        {
            Beneficio_tipo beneficio_tipo = repo.Get(id);
            if (beneficio_tipo == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", beneficio_tipo);
        }

        // GET: /Beneficio_Tipo/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: /Beneficio_Tipo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_BeneficioTipo,Nombre")] Beneficio_tipo beneficio_tipo)
        {
            if (ModelState.IsValid)
            {
                repo.Add(beneficio_tipo);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Beneficio_Tipo");
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create", beneficio_tipo);
        }

        // GET: /Beneficio_Tipo/Edit/5
        public ActionResult Edit(int id = 0)//Byte
        {
            Beneficio_tipo beneficio_tipo = repo.Get(id);
            if (beneficio_tipo == null || id == 0)
            {
                return RedirectToAction("Index");
            }

            return PartialView("_Edit", beneficio_tipo);
        }

        // POST: /Beneficio_Tipo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_BeneficioTipo,Nombre")] Beneficio_tipo beneficio_tipo)
        {         
            if (ModelState.IsValid)
            {
                repo.Update(beneficio_tipo);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Beneficio_Tipo");
                return Json(new { success = true, url = url });   
            }
            return PartialView("_Edit", beneficio_tipo);
        }

        // GET: /Beneficio_Tipo/Delete/5
        public ActionResult Delete(int id = 0)//Byte
        {
            Beneficio_tipo beneficio_tipo = repo.Get(id);
            if (beneficio_tipo == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", beneficio_tipo);
        }

        // POST: /Beneficio_Tipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            try
            {
                Beneficio_tipo beneficio_tipo = repo.Get(id);
                repo.Delete(beneficio_tipo);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Beneficio_Tipo", new { id = beneficio_tipo.Id_BeneficioTipo });
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
