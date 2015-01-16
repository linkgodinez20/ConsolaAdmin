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

        // GET: /Directorios/
        public ActionResult Index()
        {
            var directorios = repo.GetAll().Include(d => d.Directorio_tipo);
            return View(directorios.ToList());
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
        public ActionResult Edit(byte id)
        {
            Directorios directorios = repo.Get(id);
            if (directorios == null)
            {
                return HttpNotFound();
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
            Directorios directorios = repo.Get(id);
            repo.Delete(directorios);
            repo.Save();
            return RedirectToAction("Index");
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
