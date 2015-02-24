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
using System.IO;

namespace Security.Controllers
{
    public class PaisesController : Controller
    {
        private readonly IRepo<Paises> repo;
        private readonly DefaultSettings config;

        public PaisesController(IRepo<Paises> repo, DefaultSettings config)
        {
            this.repo = repo;
            this.config = config;
        }

        // GET: Paises
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Dir_PaisesBandera = config.Directorio_PaisesBanera;

            ViewBag.SortByName = sortOrder == "Name_desc" ? "Name_desc" : "Name";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Paises> ordena_paises = repo.GetAll()
                .OrderBy(x => x.Nombre);

            var paises = from s in ordena_paises
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                paises = paises.Where(d => d.Nombre.Contains(searchString)
                                       || d.FIPS.Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "Name_desc":
                    paises = paises.OrderByDescending(p => p.Nombre);
                    break;
                case "Name":
                    paises = paises.OrderBy(p => p.Nombre);
                    break;
                default:
                    paises = paises.OrderBy(p => p.Nombre);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(paises.ToPagedList(pageNumber, pageSize));
        }

        // GET: Paises/Details/5
        public ActionResult Details(short id = 0)
        {
            Paises paises = repo.Get(id);
            if (paises == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return View(paises);
        }

        // GET: Paises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Pais,FIPS,Nombre,Prioridad,Bandera")] Paises paises)
        {
            if (ModelState.IsValid)
            {

                paises.Bandera = paises.FIPS.ToString().ToLower() + ".png";

                repo.Add(paises);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(paises);
        }

        // GET: Paises/Edit/5
        public ActionResult Edit(short id = 0)
        {
            Paises paises = repo.Get(id);
            if (paises == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return View(paises);
        }

        // POST: Paises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Pais,FIPS,Nombre,Prioridad,Bandera")] Paises paises)
        {
            if (ModelState.IsValid)
            {
                repo.Update(paises);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(paises);
        }

        // GET: Paises/Delete/5
        public ActionResult Delete(short id = 0)
        {
            Paises paises = repo.Get(id);
            if (paises == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return View(paises);
        }

        // POST: Paises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Paises paises = repo.Get(id);
            repo.Delete(paises);
            repo.Save();
            return RedirectToAction("Index");
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
