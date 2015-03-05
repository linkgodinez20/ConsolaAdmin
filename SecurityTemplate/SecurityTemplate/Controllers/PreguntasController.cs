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
    public class PreguntasController : Controller
    {
        private readonly IRepo<Preguntas> repo;

        public PreguntasController(IRepo<Preguntas> Repo)
        {
            this.repo = Repo;            
        }      

        // GET: /Preguntas/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Directorio = String.IsNullOrEmpty(sortOrder) ? "Pregunta_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Preguntas> preg = repo.GetAll().OrderBy(x => x.Pregunta);

            var modelo = from s in preg select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Pregunta.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Pregunta":
                    modelo = modelo.OrderBy(s => s.Pregunta);
                    break;
                case "Pregunta_desc":
                    modelo = modelo.OrderByDescending(s => s.Pregunta);
                    break;                
                default:
                    modelo = modelo.OrderBy(s => s.Pregunta);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Preguntas/Details/5
        public ActionResult Details(byte id = 0)
        {           
            Preguntas preguntas = repo.Get(id);
            if (preguntas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", preguntas);
        }

        // GET: /Preguntas/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: /Preguntas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Pregunta,Pregunta")] Preguntas preguntas)
        {
            if (ModelState.IsValid)
            {
                repo.Add(preguntas);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Preguntas");
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create", preguntas);
        }

        // GET: /Preguntas/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Preguntas preguntas = repo.Get(id);
            if (preguntas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", preguntas);
        }

        // POST: /Preguntas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Pregunta,Pregunta")] Preguntas preguntas)
        {
            if (ModelState.IsValid)
            {
                repo.Update(preguntas);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Preguntas");
                return Json(new { success = true, url = url });   
            }
            return PartialView("_Edit", preguntas);
        }

        // GET: /Preguntas/Delete/5
        public ActionResult Delete(byte id = 0)
        {
            Preguntas preguntas = repo.Get(id);
            if (preguntas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", preguntas);
        }

        // POST: /Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            try
            {
                Preguntas preguntas = repo.Get(id);
                repo.Delete(preguntas);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Preguntas", new { id = preguntas.Id_Pregunta });
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
            }
            base.Dispose(disposing);
        }
    }
}
