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
    public class BajasController : Controller
    {
        private readonly IRepo<Baja> repo;

        public BajasController(IRepo<Baja> repo)
        {
            this.repo = repo;
        }

        // GET: Bajas
        public ActionResult Index()
        {
            return View(repo.GetAll().ToList());
        }

        // GET: Bajas/Details/5
        public ActionResult Details(byte id = 0)
        {
            Baja baja = repo.Get(id);

            if (baja == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", baja);
        }

        // GET: Bajas/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: Bajas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Baja,Nombre")] Baja baja)
        {
            if (ModelState.IsValid)
            {
                repo.Add(baja);
                repo.Save();

                string url = Url.Action("Index", "Bajas");
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create", baja);
        }

        // GET: Bajas/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Baja baja = repo.Get(id);

            if (baja == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", baja);
        }

        // POST: Bajas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Baja,Nombre")] Baja baja)
        {
            if (ModelState.IsValid)
            {
                repo.Update(baja);
                repo.Save();

                string url = Url.Action("Index", "Bajas");
                return Json(new { success = true, url = url });
            }
            return PartialView("_Edit", baja);
        }

        // GET: Bajas/Delete/5
        public ActionResult Delete(byte id = 0)
        {
            Baja baja = repo.Get(id);
            if (baja == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", baja);
        }

        // POST: Bajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Baja baja = repo.Get(id);
            repo.Delete(baja);
            repo.Save();

            string url = Url.Action("Index", "Bajas", new { id = baja.Id_Baja });
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
