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
        public ActionResult Index()
        {
            var entidades = repo.GetAll().Include(e => e.Paises);
            return View(entidades.ToList());
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
