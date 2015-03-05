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
    public class Contacto_tipoController : Controller
    {
        private readonly IRepo<Contacto_tipo> repo;

        public Contacto_tipoController(IRepo<Contacto_tipo> repo)
        {
            this.repo = repo;
        }

        // GET: Contacto_tipo
        public ActionResult Index()
        {
            return View(repo.GetAll().ToList());
        }

        // GET: Contacto_tipo/Details/5
        public ActionResult Details(byte id = 0)
        {
            Contacto_tipo contacto_tipo = repo.Get(id);
            if (contacto_tipo == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Details", contacto_tipo);
        }

        // GET: Contacto_tipo/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: Contacto_tipo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_ContactoTipo,Nombre")] Contacto_tipo contacto_tipo)
        {
            if (ModelState.IsValid)
            {
                repo.Add(contacto_tipo);
                repo.Save();

                string url = Url.Action("Index", "Contacto_tipo");
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create", contacto_tipo);
        }

        // GET: Contacto_tipo/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Contacto_tipo contacto_tipo = repo.Get(id);
            if (contacto_tipo == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Edit", contacto_tipo);
        }

        // POST: Contacto_tipo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_ContactoTipo,Nombre")] Contacto_tipo contacto_tipo)
        {
            if (ModelState.IsValid)
            {
                repo.Update(contacto_tipo);
                repo.Save();

                string url = Url.Action("Index", "Contacto_tipo");
                return Json(new { success = true, url = url });
            }
            return PartialView("_Edit", contacto_tipo);
        }

        // GET: Contacto_tipo/Delete/5
        public ActionResult Delete(byte id = 0)
        {
            Contacto_tipo contacto_tipo = repo.Get(id);
            if (contacto_tipo == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Delete", contacto_tipo);
        }

        // POST: Contacto_tipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Contacto_tipo contacto_tipo = repo.Get(id);
            repo.Delete(contacto_tipo);
            repo.Save();

            string url = Url.Action("Index", "Contacto_tipo", new { id = contacto_tipo.Id_ContactoTipo });
            return Json(new { success = true, url = url });
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
