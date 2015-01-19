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
    public class PerfilesController : Controller
    {
        private readonly IRepo<Perfiles> repo;
        private readonly IRepo<Sistemas> Repo_Sistemas;

        public PerfilesController(IRepo<Perfiles> repo, IRepo<Sistemas> Repo_Sistemas)
        {
            this.repo = repo;
            this.Repo_Sistemas = Repo_Sistemas;
        }

        // GET: Perfiles
        public ActionResult Index()
        {
            //var perfiles = db.Perfiles.Include(p => p.Sistemas);
            var perfiles = repo.GetAll().Include(p => p.Sistemas);
            return View(perfiles.ToList());
        }

        // GET: Perfiles/Details/5
        public ActionResult Details(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfiles perfiles = repo.Get(id);
            if (perfiles == null)
            {
                return HttpNotFound();
            }
            return View(perfiles);
        }

        // GET: Perfiles/Create
        public ActionResult Create()
        {
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre");
            return View();
        }

        // POST: Perfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Perfil,Nombre,Funcion,Tipo,Nivel,Id_Sistema")] Perfiles perfiles)
        {
            if (ModelState.IsValid)
            {
                repo.Add(perfiles);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", perfiles.Id_Sistema);
            return View(perfiles);
        }

        // GET: Perfiles/Edit/5
        public ActionResult Edit(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfiles perfiles = repo.Get(id);
            if (perfiles == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", perfiles.Id_Sistema);
            return View(perfiles);
        }

        // POST: Perfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Perfil,Nombre,Funcion,Tipo,Nivel,Id_Sistema")] Perfiles perfiles)
        {
            if (ModelState.IsValid)
            {
                repo.Update(perfiles);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", perfiles.Id_Sistema);
            return View(perfiles);
        }

        // GET: Perfiles/Delete/5
        public ActionResult Delete(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfiles perfiles = repo.Get(id);
            if (perfiles == null)
            {
                return HttpNotFound();
            }
            return View(perfiles);
        }

        // POST: Perfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Perfiles perfiles = repo.Get(id);
            repo.Delete(perfiles);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                Repo_Sistemas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
