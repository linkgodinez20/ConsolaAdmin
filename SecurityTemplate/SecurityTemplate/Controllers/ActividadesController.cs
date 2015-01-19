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
    public class ActividadesController : Controller
    {
        private readonly IRepo<Actividades> repo;
        private readonly IRepo<Directorios> Repo_Directorios;
        private readonly IRepo<Sistemas> Repo_Sistemas;


        public ActividadesController(IRepo<Actividades> repo, IRepo<Directorios> RepoDir, IRepo<Sistemas> RepoSis)
        {
            this.repo = repo;
            this.Repo_Directorios = RepoDir;
            this.Repo_Sistemas = RepoSis;
        }

        // GET: Actividades
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SortByNombre = String.IsNullOrEmpty(sortOrder) ? "Nombre_desc" : "";
            ViewBag.SortByDirectorio = sortOrder == "DirNombre_desc" ? "DirNombre_desc" : "DirNombre";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Actividades> ordena_actividades = repo.GetAll()
                .OrderBy(x => x.Directorios.Nombre).OrderBy(y => y.Nombre);

            var actividades = from s in ordena_actividades
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                actividades = actividades.Where(s => s.Directorios.Nombre.Contains(searchString)
                                       || s.Nombre.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "DirNombre":
                    actividades = actividades.OrderBy(p => p.Directorios.Nombre);
                    break;
                case "DirNombre_desc":
                    actividades = actividades.OrderByDescending(p => p.Directorios.Nombre);
                    break;
                case "Nombre_desc":
                    actividades = actividades.OrderByDescending(p => p.Nombre);
                    break;
                default:
                    actividades = actividades.OrderBy(p => p.Nombre);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(actividades.ToPagedList(pageNumber, pageSize));

            //var actividades = repo.GetAll().Include(p => p.Directorios).Include(a => a.Sistemas);
            //return View(actividades.ToList());
        }

        // GET: Actividades/Details/5
        public ActionResult Details(short id)
        {
            Actividades actividades = repo.Get(id);
            if (actividades == null)
            {
                return HttpNotFound();
            }
            return View(actividades);
        }

        // GET: Actividades/Create
        public ActionResult Create()
        {
            ViewBag.Id_Directorio = new SelectList(Repo_Directorios.GetAll(), "Id_Directorio", "Nombre");
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre");
            return View();
        }

        // POST: Actividades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Actividad,Nombre,Estatus,Id_Directorio,Id_Sistema")] Actividades actividades)
        {
            if (ModelState.IsValid)
            {
                repo.Add(actividades);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Directorio = new SelectList(Repo_Directorios.GetAll(), "Id_Directorio", "Nombre", actividades.Id_Directorio);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", actividades.Id_Sistema);
            return View(actividades);
        }

        // GET: Actividades/Edit/5
        public ActionResult Edit(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividades actividades = repo.Get(id);
            if (actividades == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Directorio = new SelectList(Repo_Directorios.GetAll(), "Id_Directorio", "Nombre", actividades.Id_Directorio);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", actividades.Id_Sistema);
            return View(actividades);
        }

        // POST: Actividades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Actividad,Nombre,Estatus,Id_Directorio,Id_Sistema")] Actividades actividades)
        {
            if (ModelState.IsValid)
            {
                repo.Update(actividades);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Directorio = new SelectList(Repo_Directorios.GetAll(), "Id_Directorio", "Nombre", actividades.Id_Directorio);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", actividades.Id_Sistema);
            return View(actividades);
        }

        // GET: Actividades/Delete/5
        public ActionResult Delete(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividades actividades = repo.Get(id);
            if (actividades == null)
            {
                return HttpNotFound();
            }
            return View(actividades);
        }

        // POST: Actividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Actividades actividades = repo.Get(id);
            repo.Delete(actividades);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                Repo_Directorios.Dispose();
                Repo_Sistemas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
