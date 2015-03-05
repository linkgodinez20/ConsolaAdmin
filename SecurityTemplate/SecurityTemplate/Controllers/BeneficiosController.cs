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
    public class BeneficiosController : Controller
    {
        private readonly IRepo<Beneficio> repo;
        private readonly IRepo<Beneficio_tipo> Repo_BeneficioTipo;

        public BeneficiosController(IRepo<Beneficio> repo, IRepo<Beneficio_tipo> Repo_BeneficioTipo)
        {
            this.repo = repo;
            this.Repo_BeneficioTipo = Repo_BeneficioTipo;
        }

        // GET: Beneficios
        public ActionResult Index()
        {
            //var beneficio = db.Beneficio.Include(b => b.Beneficio_tipo);
            var beneficio = repo.GetAll().Include(p => p.Beneficio_tipo);
            return View(beneficio.ToList());
        }

        // GET: Beneficios/Details/5
        public ActionResult Details(byte id = 0)
        {
            Beneficio beneficio = repo.Get(id);
            if (beneficio == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Details", beneficio);
        }

        // GET: Beneficios/Create
        public ActionResult Create()
        {
            ViewBag.Id_BeneficioTipo = new SelectList(Repo_BeneficioTipo.GetAll(), "Id_BeneficioTipo", "Nombre");
            return PartialView("_Create");
        }

        // POST: Beneficios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Beneficio,Nombre,Id_BeneficioTipo,Estatus")] Beneficio beneficio)
        {
            if (ModelState.IsValid)
            {
                repo.Add(beneficio);
                repo.Save();

                string url = Url.Action("Index", "Beneficios");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_BeneficioTipo = new SelectList(Repo_BeneficioTipo.GetAll(), "Id_BeneficioTipo", "Nombre", beneficio.Id_BeneficioTipo);
            return PartialView("_Create", beneficio);
        }

        // GET: Beneficios/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Beneficio beneficio = repo.Get(id);
            if (beneficio == null || id == 0)
            {
                return RedirectToAction("index");
            }
            ViewBag.Id_BeneficioTipo = new SelectList(Repo_BeneficioTipo.GetAll(), "Id_BeneficioTipo", "Nombre", beneficio.Id_BeneficioTipo);
            return PartialView("_Edit", beneficio);
        }

        // POST: Beneficios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Beneficio,Nombre,Id_BeneficioTipo,Estatus")] Beneficio beneficio)
        {
            if (ModelState.IsValid)
            {
                repo.Update(beneficio);
                repo.Save();

                string url = Url.Action("Index", "Beneficios");
                return Json(new { success = true, url = url });
            }
            ViewBag.Id_BeneficioTipo = new SelectList(Repo_BeneficioTipo.GetAll(), "Id_BeneficioTipo", "Nombre", beneficio.Id_BeneficioTipo);
            return PartialView("_Edit", beneficio);
        }

        // GET: Beneficios/Delete/5
        public ActionResult Delete(byte id = 0)
        {
            Beneficio beneficio = repo.Get(id);
            if (beneficio == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Delete", beneficio);
        }

        // POST: Beneficios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Beneficio beneficio = repo.Get(id);
            repo.Delete(beneficio);
            repo.Save();

            string url = Url.Action("Index", "Beneficios", new { id = beneficio.Id_Beneficio });
            return Json(new { success = true, url = url });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                Repo_BeneficioTipo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
