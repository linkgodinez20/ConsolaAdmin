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
    public class PermisosController : Controller
    {
        private readonly IRepo<Permisos> repo;

        public PermisosController(IRepo<Permisos> Repo)
        {
            this.repo = Repo;            
        }      

        // GET: /Permisos/
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        // GET: /Permisos/Details/5
        public ActionResult Details(byte id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisos permisos = repo.Get(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // GET: /Permisos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Permisos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Permiso,Nombre,Estatus")] Permisos permisos)
        {
            if (ModelState.IsValid)
            {
                repo.Add(permisos);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(permisos);
        }

        // GET: /Permisos/Edit/5
        public ActionResult Edit(byte id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisos permisos = repo.Get(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // POST: /Permisos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Permiso,Nombre,Estatus")] Permisos permisos)
        {
            if (ModelState.IsValid)
            {
                repo.Update(permisos);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(permisos);
        }

        // GET: /Permisos/Delete/5
        public ActionResult Delete(byte id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisos permisos = repo.Get(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // POST: /Permisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Permisos permisos = repo.Get(id);
            repo.Delete(permisos);
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
