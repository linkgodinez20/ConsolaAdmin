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
    public class ControladoresController : Controller
    {
        private readonly IRepo<Controladores> repo;
        private readonly IRepo<Sistemas> repo_Sistemas;

        public ControladoresController(IRepo<Controladores> repo, IRepo<Sistemas> repo_Sistemas)
        {
            this.repo = repo;
            this.repo_Sistemas = repo_Sistemas;
        }

        // GET: Controladores
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SortByName = sortOrder == "Nombre_desc" ? "Nombre_desc" : "Nombre";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Controladores> ordena_controladores = repo.GetAll()
                .OrderBy(x => x.Nombre);

            var controladores = from s in ordena_controladores
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                controladores = controladores.Where(s => s.Nombre.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Nombre":
                    controladores = controladores.OrderBy(p => p.Nombre);
                    break;
                case "Nombre_desc":
                    controladores = controladores.OrderByDescending(p => p.Nombre);
                    break;

                default:
                    controladores = controladores.OrderBy(n => n.Nombre);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(controladores.ToPagedList(pageNumber, pageSize));
        }

        // GET: Controladores/Details/5
        public ActionResult Details(short id = 0)
        {
            Controladores controladores = repo.Get(id);
            
            if (controladores == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return View(controladores);
        }

        // GET: Controladores/Create
        public ActionResult Create()
        {
            ViewBag.Id_Sistema = new SelectList(repo_Sistemas.GetAll(), "Id_Sistema", "Nombre");

            return View();
        }

        // POST: Controladores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Controlador,Nombre,Estatus,Id_Sistema")] Controladores controladores)
        {
            if (ModelState.IsValid)
            {
                repo.Add(controladores);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Sistema = new SelectList(repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", controladores.Id_Sistema);

            return View(controladores);
        }

        // GET: Controladores/Edit/5
        public ActionResult Edit(short id = 0)
        {
            Controladores controladores = repo.Get(id);

            if (controladores == null || id == 0)
            {
                return RedirectToAction("index");
            }
            ViewBag.Id_Sistema = new SelectList(repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", controladores.Id_Sistema);

            return View(controladores);
        }

        // POST: Controladores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Controlador,Nombre,Estatus,Id_Sistema")] Controladores controladores)
        {
            if (ModelState.IsValid)
            {
                repo.Update(controladores);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Sistema = new SelectList(repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", controladores.Id_Sistema);

            return View(controladores);
        }

        // GET: Controladores/Delete/5
        public ActionResult Delete(short id = 0)
        {
            Controladores controladores = repo.Get(id);

            if (controladores == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return View(controladores);
        }

        // POST: Controladores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Controladores controladores = repo.Get(id);
            repo.Delete(controladores);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                repo_Sistemas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
