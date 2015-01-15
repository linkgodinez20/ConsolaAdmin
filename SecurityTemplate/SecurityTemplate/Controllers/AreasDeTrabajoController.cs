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
    public class AreasDeTrabajoController : Controller
    {

        private readonly IRepo<AreasDeTrabajo> repo;
        private readonly IRepo<Sistemas> sistemas;
       
        public AreasDeTrabajoController(IRepo<AreasDeTrabajo> Repo, IRepo<Sistemas> Sistemas)
        {
            this.repo = Repo;
            this.sistemas = Sistemas;
        }      

        // GET: /AreasDeTrabajo/
        public ActionResult Index()
        {
            var areasdetrabajo = repo.GetAll();
            return View(areasdetrabajo.ToList());
        }

        // GET: /AreasDeTrabajo/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreasDeTrabajo areasdetrabajo = repo.Get(id);
            if (areasdetrabajo == null)
            {
                return HttpNotFound();
            }
            return View(areasdetrabajo);
        }

        // GET: /AreasDeTrabajo/Create
        public ActionResult Create()
        {
            ViewBag.Id_Sistema = new SelectList( sistemas.GetAll(), "Id_Sistema", "Nombre");
            return View();
        }

        // POST: /AreasDeTrabajo/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_AreaDeTrabajo,Nombre,Id_Sistema")] AreasDeTrabajo areasdetrabajo)
        {
            if (ModelState.IsValid)
            {
                repo.Add(areasdetrabajo);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", areasdetrabajo.Id_Sistema);
            return View(areasdetrabajo);
        }

        // GET: /AreasDeTrabajo/Edit/5
        public ActionResult Edit(int id)
        {
            AreasDeTrabajo areasdetrabajo = repo.Get(id);
            if (areasdetrabajo == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", areasdetrabajo.Id_Sistema);
            return View(areasdetrabajo);
        }

        // POST: /AreasDeTrabajo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_AreaDeTrabajo,Nombre,Id_Sistema")] AreasDeTrabajo areasdetrabajo)
        {            
            if (ModelState.IsValid)
            {
                repo.Update(areasdetrabajo);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", areasdetrabajo.Id_Sistema);
            return View(areasdetrabajo);
        }

        // GET: /AreasDeTrabajo/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreasDeTrabajo areasdetrabajo = repo.Get(id);
            if (areasdetrabajo == null)
            {
                return HttpNotFound();
            }
            return View(areasdetrabajo);
        }

        // POST: /AreasDeTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            AreasDeTrabajo areasdetrabajo = repo.Get(id);
            repo.Delete(areasdetrabajo);            
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
