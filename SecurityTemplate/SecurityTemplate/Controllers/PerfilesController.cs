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
        public ActionResult Details(short id = 0)
        {
            Perfiles perfiles = repo.Get(id);
            if (perfiles == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Details", perfiles);
        }

        // GET: Perfiles/Create
        public ActionResult Create()
        {
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre");
            return PartialView("_Create");
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

                string url = Url.Action("Index", "Perfiles");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", perfiles.Id_Sistema);
            return PartialView("_Create", perfiles);
        }

        // GET: Perfiles/Edit/5
        public ActionResult Edit(short id = 0)
        {
            Perfiles perfiles = repo.Get(id);
            if (perfiles == null || id == 0)
            {
                return RedirectToAction("index");
            }
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", perfiles.Id_Sistema);
            return PartialView("_Edit", perfiles);
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

                string url = Url.Action("Index", "Perfiles");
                return Json(new { success = true, url = url });
            }
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", perfiles.Id_Sistema);
            return PartialView("_Edit", perfiles);
        }

        // GET: Perfiles/Delete/5
        public ActionResult Delete(short id = 0)
        {
            Perfiles perfiles = repo.Get(id);
            if (perfiles == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Delete", perfiles);
        }

        // POST: Perfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Perfiles perfiles = repo.Get(id);
            repo.Delete(perfiles);
            repo.Save();

            string url = Url.Action("Index", "Perfiles", new { id = perfiles.Id_Perfil });
            return Json(new { success = true, url = url });
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
