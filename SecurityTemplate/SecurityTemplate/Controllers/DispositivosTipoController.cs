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
    public class DispositivosTipoController : Controller
    {
        private readonly IRepo<Dispositivos_tipo> repo;
        public DispositivosTipoController(IRepo<Dispositivos_tipo> Repo)
        {
            this.repo = Repo;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Nombre = String.IsNullOrEmpty(sortOrder) ? "Nombre_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Dispositivos_tipo> dispTipo = repo.GetAll().OrderBy(x => x.Nombre);

            var modelo = from s in dispTipo
                               select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Nombre":
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
                case "Nombre_desc":
                    modelo = modelo.OrderByDescending(s => s.Nombre);
                    break;
                default:
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }

        // GET: DispositivosTipo/Details/5
        public ActionResult Details(byte id = 0)
        {
            Dispositivos_tipo dispositivos_tipo = repo.Get(id);
            if (dispositivos_tipo == null || id == 0)
            {
                return RedirectToAction("Index");    
            }
            return View(dispositivos_tipo);
        }

        // GET: DispositivosTipo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DispositivosTipo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_DispositivoTipo,Nombre")] Dispositivos_tipo dispositivos_tipo)
        {
            if (ModelState.IsValid)
            {
                repo.Add(dispositivos_tipo);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(dispositivos_tipo);
        }

        // GET: DispositivosTipo/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Dispositivos_tipo dispositivos_tipo = repo.Get(id);
            if (dispositivos_tipo == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return View(dispositivos_tipo);
        }

        // POST: DispositivosTipo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_DispositivoTipo,Nombre")] Dispositivos_tipo dispositivos_tipo)
        {
            if (ModelState.IsValid)
            {
                repo.Update(dispositivos_tipo);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(dispositivos_tipo);
        }

        // GET: DispositivosTipo/Delete/5
        public ActionResult Delete(byte id = 0)
        {            
            Dispositivos_tipo dispositivos_tipo = repo.Get(id);
            if (dispositivos_tipo == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return View(dispositivos_tipo);
        }

        // POST: DispositivosTipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Dispositivos_tipo dispositivos_tipo = repo.Get(id);
            repo.Delete(dispositivos_tipo);
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
