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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.AreasDeTrabajo = String.IsNullOrEmpty(sortOrder) ? "AreasDeTrabajo_desc" : "";
            ViewBag.Sistema = sortOrder == "Sistema" ? "Sistema" : "Sistema_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<AreasDeTrabajo> areas = repo.GetAll()
                .OrderBy(x => x.Nombre);

            var AreasTrabajo = from s in areas
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                AreasTrabajo = AreasTrabajo.Where(s => s.Nombre.Contains(searchString) || s.Sistemas.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "AreasDeTrabajo":
                    AreasTrabajo = AreasTrabajo.OrderBy(s => s.Nombre);
                    break;
                case "AreasDeTrabajo_desc":
                    AreasTrabajo = AreasTrabajo.OrderByDescending(s => s.Nombre);
                    break;
                case "Sistema":
                    AreasTrabajo = AreasTrabajo.OrderBy(s => s.Sistemas.Nombre);
                    break;
                case "Sistema_desc":
                    AreasTrabajo = AreasTrabajo.OrderByDescending(s => s.Sistemas.Nombre);
                    break;               
                default:
                    AreasTrabajo = AreasTrabajo.OrderBy(s => s.Nombre);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(AreasTrabajo.ToPagedList(pageNumber, pageSize));         
        }

        // GET: /AreasDeTrabajo/Details/5
        public ActionResult Details(int id = 0)
        {
            AreasDeTrabajo areasdetrabajo = repo.Get(id);
            if (areasdetrabajo == null || id == 0)
            {
                return RedirectToAction("Index");            
            }
            return PartialView("_Details", areasdetrabajo);
        }

        // GET: /AreasDeTrabajo/Create
        public ActionResult Create()
        {
            ViewBag.Id_Sistema = new SelectList( sistemas.GetAll(), "Id_Sistema", "Nombre");
            return PartialView("_Create");
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
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "AreasDeTrabajo");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", areasdetrabajo.Id_Sistema);
            return PartialView("_Create", areasdetrabajo);
        }

        // GET: /AreasDeTrabajo/Edit/5
        public ActionResult Edit(int id = 0)
        {
            AreasDeTrabajo areasdetrabajo = repo.Get(id);
            if (areasdetrabajo == null || id == 0)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", areasdetrabajo.Id_Sistema);
            return PartialView("_Edit", areasdetrabajo);
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
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "AreasDeTrabajo");
                return Json(new { success = true, url = url });   
            }
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", areasdetrabajo.Id_Sistema);
            return PartialView("_Edit", areasdetrabajo);
        }

        // GET: /AreasDeTrabajo/Delete/5
        public ActionResult Delete(int id = 0)
        {
            AreasDeTrabajo areasdetrabajo = repo.Get(id);
            if (areasdetrabajo == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", areasdetrabajo);
        }

        // POST: /AreasDeTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            try
            {
                AreasDeTrabajo areasdetrabajo = repo.Get(id);
                repo.Delete(areasdetrabajo);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "AreasDeTrabajo", new { id = areasdetrabajo.Id_AreaDeTrabajo });
                return Json(new { success = true, url = url });
            }
            catch (Exception ex)
            {
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
                sistemas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
