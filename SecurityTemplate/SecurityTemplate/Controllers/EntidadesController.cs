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
        //public ActionResult Index()
        //{
        //    var entidades = repo.GetAll().Include(e => e.Paises);
        //    return View(entidades.ToList());
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
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

            IOrderedQueryable<Entidades> entidades = repo.GetAll().OrderBy(x => x.Nombre);

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

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Entidades/Details/5
        public ActionResult Details(short id)
        {
            Entidades entidades = repo.Get(id);
            if (entidades == null)
            {
                return HttpNotFound();
            }
            return View(entidades);
        }

        // GET: /Entidades/Create
        public ActionResult Create()
        {
            ViewBag.Id_Pais = new SelectList(paises.GetAll(), "Id_Pais", "FIPS");
            return View();
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
                return RedirectToAction("Index");
            }

            ViewBag.Id_Pais = new SelectList(paises.GetAll(), "Id_Pais", "FIPS", entidades.Id_Pais);
            return View(entidades);
        }

        // GET: /Entidades/Edit/5
        public ActionResult Edit(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidades entidades = repo.Get(id);
            if (entidades == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Pais = new SelectList(paises.GetAll(), "Id_Pais", "FIPS", entidades.Id_Pais);
            return View(entidades);
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
                return RedirectToAction("Index");
            }
            ViewBag.Id_Pais = new SelectList(paises.GetAll(), "Id_Pais", "FIPS", entidades.Id_Pais);
            return View(entidades);
        }

        // GET: /Entidades/Delete/5
        public ActionResult Delete(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidades entidades = repo.Get(id);
            if (entidades == null)
            {
                return HttpNotFound();
            }
            return View(entidades);
        }

        // POST: /Entidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Entidades entidades = repo.Get(id);
            repo.Delete(entidades);
            repo.Save();
            return RedirectToAction("Index");
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
