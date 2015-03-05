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
    public class Baja_MotivosController : Controller
    {        
        private readonly IRepo<Baja_motivos> repo;
        private readonly IRepo<Baja> baja;

        public Baja_MotivosController(IRepo<Baja_motivos> Repo, IRepo<Baja> Baja)
        {
            this.repo = Repo;
            this.baja = Baja;
        }

        // GET: /Baja_Motivos/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Motivo = String.IsNullOrEmpty(sortOrder) ? "Motivo_desc" : "";
            ViewBag.Baja = sortOrder == "Baja" ? "Baja" : "Baja_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Baja_motivos> baja_mot = repo.GetAll().OrderBy(x => x.Descripcion);

            var modelo = from s in baja_mot select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Descripcion.Contains(searchString) || s.Baja.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Motivo":
                    modelo = modelo.OrderBy(s => s.Descripcion);
                    break;
                case "Motivo_desc":
                    modelo = modelo.OrderByDescending(s => s.Descripcion);
                    break;
                case "Baja":
                    modelo = modelo.OrderBy(s => s.Baja.Nombre);
                    break;
                case "Baja_desc":
                    modelo = modelo.OrderByDescending(s => s.Baja.Nombre);
                    break;
                default:
                    modelo = modelo.OrderBy(s => s.Descripcion);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Baja_Motivos/Details/5
        public ActionResult Details(int id = 0)//byte
        {
            Baja_motivos baja_motivos = repo.Get(id);
            if (baja_motivos == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", baja_motivos);
        }

        // GET: /Baja_Motivos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre");
            return PartialView("_Create");
        }

        // POST: /Baja_Motivos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_MotivoBaja,Descripcion,Id_Baja")] Baja_motivos baja_motivos)
        {
            if (ModelState.IsValid)
            {
                repo.Add(baja_motivos);                
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Baja_Motivos");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", baja_motivos.Id_Baja);
            return PartialView("_Create", baja_motivos);
        }

        // GET: /Baja_Motivos/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Baja_motivos baja_motivos = repo.Get(id);
            if (baja_motivos == null || id == 0)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", baja_motivos.Id_Baja);
            return PartialView("_Edit", baja_motivos);
        }

        // POST: /Baja_Motivos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_MotivoBaja,Descripcion,Id_Baja")] Baja_motivos baja_motivos)
        {
            if (ModelState.IsValid)
            {
                repo.Update(baja_motivos);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Baja_Motivos");
                return Json(new { success = true, url = url }); 
            }
            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", baja_motivos.Id_Baja);
            return PartialView("_Edit", baja_motivos);
        }

        // GET: /Baja_Motivos/Delete/5
        public ActionResult Delete(byte id = 0)
        {           
            Baja_motivos baja_motivos = repo.Get(id);
            if (baja_motivos == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", baja_motivos);
        }

        // POST: /Baja_Motivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            try
            {
                Baja_motivos baja_motivos = repo.Get(id);
                repo.Delete(baja_motivos);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Baja_Motivos", new { id = baja_motivos.Id_MotivoBaja });
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
                baja.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
