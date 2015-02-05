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
    public class DirectoriosController : Controller
    {
        private readonly IRepo<Directorios> repo;
        private readonly IRepo<Directorio_tipo> dirTipo;

        public DirectoriosController(IRepo<Directorios> Repo, IRepo<Directorio_tipo> DirTipo) 
        {
            this.repo = Repo;
            this.dirTipo = DirTipo;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Directorio = String.IsNullOrEmpty(sortOrder) ? "Directorio_desc" : "";
            ViewBag.Estatus = sortOrder == "Estatus" ? "Estatus" : "Estatus_desc";
            ViewBag.TipoDir = sortOrder == "TipoDir" ? "TipoDir" : "TipoDir_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Directorios> dirs = repo.GetAll().OrderBy(x => x.Nombre);

            var modelo = from s in dirs select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Nombre.Contains(searchString) || s.Directorio_tipo.Nombre.Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "Directorio":
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
                case "Directorio_desc":
                    modelo = modelo.OrderByDescending(s => s.Nombre);
                    break;
                case "Estatus":
                    modelo = modelo.OrderBy(s => s.Estatus);
                    break;
                case "Estatus_desc":
                    modelo = modelo.OrderByDescending(s => s.Estatus);
                    break;
                case "TipoDir":
                    modelo = modelo.OrderBy(s => s.Directorio_tipo.Nombre);
                    break;
                case "TipoDir_desc":
                    modelo = modelo.OrderByDescending(s => s.Directorio_tipo.Nombre);
                    break;
                default:
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Directorios/Details/5
        public ActionResult Details(byte id)
        {
            Directorios directorios = repo.Get(id);
            if (directorios == null)
            {
                return HttpNotFound();
            }
            return View(directorios);
        }

        // GET: /Directorios/Create
        public ActionResult Create()
        {
            ViewBag.Id_DirectorioTipo = new SelectList(dirTipo.GetAll(), "Id_DirectorioTipo", "Nombre");
            return View();
        }

        // POST: /Directorios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Directorio,Nombre,Id_DirectorioTipo,Estatus")] Directorios directorios)
        {
            if (ModelState.IsValid)
            {
                repo.Add(directorios);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_DirectorioTipo = new SelectList(dirTipo.GetAll(), "Id_DirectorioTipo", "Nombre", directorios.Id_DirectorioTipo);
            return View(directorios);
        }

        // GET: /Directorios/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Directorios directorios = repo.Get(id);
            if (directorios == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Id_DirectorioTipo = new SelectList(dirTipo.GetAll(), "Id_DirectorioTipo", "Nombre", directorios.Id_DirectorioTipo);
            return View(directorios);
        }

        // POST: /Directorios/Edit/5     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Directorio,Nombre,Id_DirectorioTipo,Estatus")] Directorios directorios)
        {
            if (ModelState.IsValid)
            {
                repo.Update(directorios);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_DirectorioTipo = new SelectList(dirTipo.GetAll(), "Id_DirectorioTipo", "Nombre", directorios.Id_DirectorioTipo);
            return View(directorios);
        }

        // GET: /Directorios/Delete/5
        public ActionResult Delete(byte id)
        {
            Directorios directorios = repo.Get(id);
            if (directorios == null)
            {
                return HttpNotFound();
            }
            return View(directorios);
        }

        // POST: /Directorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            try
            {
                Directorios directorios = repo.Get(id);
                repo.Delete(directorios);
                repo.Save();
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                //ViewBag.Error = ex.Message;
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
                dirTipo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
