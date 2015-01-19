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
    public class EquiposController : Controller
    {
        private readonly IRepo<Equipos> repo;
        private readonly IRepo<Equipos_tipo> Repo_EquiposTipo;
        private readonly IRepo<Cuentas> Repo_Cuentas;
        private readonly IRepo<Sistemas> Repo_Sistemas;

        public EquiposController(IRepo<Equipos> repo, IRepo<Equipos_tipo> Repo_EquiposTipo, IRepo<Cuentas> Repo_Cuentas, IRepo<Sistemas> Repo_Sistemas)
        {
            this.repo = repo;
            this.Repo_EquiposTipo = Repo_EquiposTipo;
            this.Repo_Cuentas = Repo_Cuentas;
            this.Repo_Sistemas = Repo_Sistemas;

        }

        // GET: Equipos
        public ActionResult Index()
        {
            //var equipos = db.Equipos.Include(e => e.Cuentas).Include(e => e.Equipos_tipo).Include(e => e.Sistemas);
            var equipos = repo.GetAll().Include(p => p.Cuentas).Include(q => q.Equipos_tipo).Include(r => r.Sistemas);
            return View(equipos.ToList());
        }

        // GET: Equipos/Details/5
        public ActionResult Details(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = repo.Get(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // GET: Equipos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cuenta = new SelectList(Repo_Cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta");
            ViewBag.Id_EquipoTipo = new SelectList(Repo_EquiposTipo.GetAll(), "Id_EquipoTipo", "Nombre");
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre");
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Equipo,Hostname,IP,MacAddress,Id_EquipoTipo,Descripcion,Ubiacion,FechaAlta,FechaCaducidad,Id_Cuenta,Id_Sistema,Estatus")] Equipos equipos)
        {
            if (ModelState.IsValid)
            {
                repo.Add(equipos);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cuenta = new SelectList(Repo_Cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta", equipos.Id_Cuenta);
            ViewBag.Id_EquipoTipo = new SelectList(Repo_EquiposTipo.GetAll(), "Id_EquipoTipo", "Nombre", equipos.Id_EquipoTipo);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", equipos.Id_Sistema);
            return View(equipos);
        }

        // GET: Equipos/Edit/5
        public ActionResult Edit(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = repo.Get(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cuenta = new SelectList(Repo_Cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta", equipos.Id_Cuenta);
            ViewBag.Id_EquipoTipo = new SelectList(Repo_EquiposTipo.GetAll(), "Id_EquipoTipo", "Nombre", equipos.Id_EquipoTipo);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", equipos.Id_Sistema);
            return View(equipos);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Equipo,Hostname,IP,MacAddress,Id_EquipoTipo,Descripcion,Ubiacion,FechaAlta,FechaCaducidad,Id_Cuenta,Id_Sistema,Estatus")] Equipos equipos)
        {
            if (ModelState.IsValid)
            {
                repo.Update(equipos);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cuenta = new SelectList(Repo_Cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta", equipos.Id_Cuenta);
            ViewBag.Id_EquipoTipo = new SelectList(Repo_EquiposTipo.GetAll(), "Id_EquipoTipo", "Nombre", equipos.Id_EquipoTipo);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", equipos.Id_Sistema);
            return View(equipos);
        }

        // GET: Equipos/Delete/5
        public ActionResult Delete(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = repo.Get(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Equipos equipos = repo.Get(id);
            repo.Delete(equipos);
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
