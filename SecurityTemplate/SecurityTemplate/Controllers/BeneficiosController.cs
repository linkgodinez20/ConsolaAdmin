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
        public ActionResult Details(byte id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficio beneficio = repo.Get(id);
            if (beneficio == null)
            {
                return HttpNotFound();
            }
            return View(beneficio);
        }

        // GET: Beneficios/Create
        public ActionResult Create()
        {
            ViewBag.Id_BeneficioTipo = new SelectList(Repo_BeneficioTipo.GetAll(), "Id_BeneficioTipo", "Nombre");
            return View();
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
                return RedirectToAction("Index");
            }

            ViewBag.Id_BeneficioTipo = new SelectList(Repo_BeneficioTipo.GetAll(), "Id_BeneficioTipo", "Nombre", beneficio.Id_BeneficioTipo);
            return View(beneficio);
        }

        // GET: Beneficios/Edit/5
        public ActionResult Edit(byte id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficio beneficio = repo.Get(id);
            if (beneficio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_BeneficioTipo = new SelectList(Repo_BeneficioTipo.GetAll(), "Id_BeneficioTipo", "Nombre", beneficio.Id_BeneficioTipo);
            return View(beneficio);
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
                return RedirectToAction("Index");
            }
            ViewBag.Id_BeneficioTipo = new SelectList(Repo_BeneficioTipo.GetAll(), "Id_BeneficioTipo", "Nombre", beneficio.Id_BeneficioTipo);
            return View(beneficio);
        }

        // GET: Beneficios/Delete/5
        public ActionResult Delete(byte id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficio beneficio = repo.Get(id);
            if (beneficio == null)
            {
                return HttpNotFound();
            }
            return View(beneficio);
        }

        // POST: Beneficios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Beneficio beneficio = repo.Get(id);
            repo.Delete(beneficio);
            repo.Save();
            return RedirectToAction("Index");
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
