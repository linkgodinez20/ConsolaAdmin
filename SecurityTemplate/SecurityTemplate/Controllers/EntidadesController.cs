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
    public class EntidadesController : Controller
    {        
        private readonly IRepo<Entidades> repo;
        private readonly IRepo<Paises> paises;

        public EntidadesController(IRepo<Entidades> Repo, IRepo<Paises> Paises)
        {
            this.repo = Repo;
            this.paises = Paises;
        }

        // GET: /Entidades/
        public ActionResult Index(Int16? Pais, string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Pais != null)
            {
                Session["Entidades_Index_Id_Pais"] = Pais;

                var np = (from p in paises.GetAll()
                         where p.Id_Pais == Pais
                         select new
                         {
                             Nombre = p.Nombre
                         }).ToList();

                foreach (var p in np)
                {
                    Session["Entidades_Index_Id_PaisNombre"] = p.Nombre;
                }
            }
            
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Entidad = String.IsNullOrEmpty(sortOrder) ? "Entidad_desc" : "";
            ViewBag.Abreviatura = sortOrder == "Abreviatura" ? "Abreviatura" : "Abreviatura_desc";
            ViewBag.Pais = sortOrder == "Pais" ? "Pais" : "Pais_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            Int16? Ent_idx_IdPais = Convert.ToInt16(Session["Entidades_Index_Id_Pais"]);
            IOrderedQueryable<Entidades> entidades = repo.GetAll().Where(p => p.Id_Pais == Ent_idx_IdPais).OrderBy(x => x.Nombre);
            

            var modelo = from s in entidades select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Nombre.Contains(searchString) || s.Abreviatura.Contains(searchString) || s.Paises.FIPS.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Entidad":
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
                case "Entidad_desc":
                    modelo = modelo.OrderByDescending(s => s.Nombre);
                    break;
                case "Abreviatura":
                    modelo = modelo.OrderBy(s => s.Abreviatura);
                    break;
                case "Abreviatura_desc":
                    modelo = modelo.OrderByDescending(s => s.Abreviatura);
                    break;
                case "Pais":
                    modelo = modelo.OrderBy(s => s.Paises.FIPS);
                    break;
                case "Pais_desc":
                    modelo = modelo.OrderByDescending(s => s.Paises.FIPS);
                    break;
                default:
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Entidades/Details/5
        public ActionResult Details(short id = 0, short id2 = 0)
        {
            Entidades entidades = repo.Get(id,id2);
            if (entidades == null || id == 0 || id2 == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", entidades);
        }

        // GET: /Entidades/Create
        public ActionResult Create()
        {
            ViewBag.Id_Pais = new SelectList(paises.GetAll(), "Id_Pais", "FIPS");
            return PartialView("_Create");
        }

        // POST: /Entidades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Pais,Id_Entidad,Nombre,Abreviatura")] Entidades entidades)
        {
            if (ModelState.IsValid)
            {
                repo.Add(entidades);
                repo.Save();

                string url = Url.Action("Index", "Entidades");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_Pais = new SelectList(paises.GetAll(), "Id_Pais", "FIPS", entidades.Id_Pais);
            return PartialView("_Create", entidades);
        }

        // GET: /Entidades/Edit/5
        public ActionResult Edit(short id = 0, short id2 = 0)
        {
            Entidades entidades = repo.Get(id,id2);
            if (entidades == null || id == 0 || id2 == 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Id_Pais = new SelectList(paises.GetAll(), "Id_Pais", "FIPS", entidades.Id_Pais);
            return PartialView("_Edit", entidades);
        }

        // POST: /Entidades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Pais,Id_Entidad,Nombre,Abreviatura")] Entidades entidades)
        {
            if (ModelState.IsValid)
            {
                repo.Update(entidades);
                repo.Save();

                string url = Url.Action("Index", "Entidades");
                return Json(new { success = true, url = url });
            }
            ViewBag.Id_Pais = new SelectList(paises.GetAll(), "Id_Pais", "FIPS", entidades.Id_Pais);
            return PartialView("_Edit", entidades);
        }

        // GET: /Entidades/Delete/5
        public ActionResult Delete(short id = 0, short id2 = 0)
        {
            Entidades entidades = repo.Get(id,id2);
            if (entidades == null || id == 0 || id2 == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", entidades);
        }

        // POST: /Entidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int16 id,Int16 id2)
        {
            try
            {
                Entidades entidades = repo.Get(id,id2);
                repo.Delete(entidades);
                repo.Save();

                string url = Url.Action("Index", "Entidades", new { id = entidades.Id_Pais, id2 = entidades.Id_Entidad });
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
                paises.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
