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
    public class Permisos_CuentasXActividadesController : Controller
    {        
        private readonly IRepo<Permisos_Cuentas_x_Actividades> repo;
        private readonly IRepo<Actividades> actividades;
        private readonly IRepo<Cuentas> cuentas;
        private readonly IRepo<Permisos> permisos;

        public Permisos_CuentasXActividadesController(IRepo<Permisos_Cuentas_x_Actividades> Repo, IRepo<Actividades> Actividades, IRepo<Cuentas> Cuentas, IRepo<Permisos> Permisos) 
        {
            this.repo = Repo;
            this.actividades = Actividades;
            this.cuentas = Cuentas;
            this.permisos = Permisos;
        }
        // GET: /Permisos_CuentasXActividades/
        public ActionResult Index()
        {
            var permisos_cuentas_x_actividades = repo.GetAll().Include(p => p.Actividades).Include(p => p.Cuentas).Include(p => p.Permisos);
            return View(permisos_cuentas_x_actividades.ToList());
        }

        // GET: /Permisos_CuentasXActividades/Details/5
        public ActionResult Details(int id)
        {
            Permisos_Cuentas_x_Actividades permisos_cuentas_x_actividades = repo.Get(id);
            if (permisos_cuentas_x_actividades == null)
            {
                return HttpNotFound();
            }
            return View(permisos_cuentas_x_actividades);
        }

        // GET: /Permisos_CuentasXActividades/Create
        public ActionResult Create()
        {            
            ViewBag.Id_Actividad = new SelectList(actividades.GetAll(), "Id_Actividad", "Nombre");
            ViewBag.Id_Cuenta = new SelectList(cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta");
            ViewBag.Id_Permiso = new SelectList(permisos.GetAll(), "Id_Permiso", "Nombre");
            return View();
        }                                   

        // POST: /Permisos_CuentasXActividades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Cuenta,Id_Actividad,Id_Permiso")] Permisos_Cuentas_x_Actividades permisos_cuentas_x_actividades)
        {
            if (ModelState.IsValid)
            {
                repo.Add(permisos_cuentas_x_actividades);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Actividad = new SelectList(actividades.GetAll(), "Id_Actividad", "Nombre", permisos_cuentas_x_actividades.Id_Actividad);
            ViewBag.Id_Cuenta = new SelectList(cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta", permisos_cuentas_x_actividades.Id_Cuenta);
            ViewBag.Id_Permiso = new SelectList(permisos.GetAll(), "Id_Permiso", "Nombre", permisos_cuentas_x_actividades.Id_Permiso);
            return View(permisos_cuentas_x_actividades);
        }

        // GET: /Permisos_CuentasXActividades/Edit/5
        public ActionResult Edit(int id)
        {
            Permisos_Cuentas_x_Actividades permisos_cuentas_x_actividades = repo.Get(id);
            if (permisos_cuentas_x_actividades == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Actividad = new SelectList(actividades.GetAll(), "Id_Actividad", "Nombre", permisos_cuentas_x_actividades.Id_Actividad);
            ViewBag.Id_Cuenta = new SelectList(cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta", permisos_cuentas_x_actividades.Id_Cuenta);
            ViewBag.Id_Permiso = new SelectList(permisos.GetAll(), "Id_Permiso", "Nombre", permisos_cuentas_x_actividades.Id_Permiso);
            return View(permisos_cuentas_x_actividades);
        }

        // POST: /Permisos_CuentasXActividades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Cuenta,Id_Actividad,Id_Permiso")] Permisos_Cuentas_x_Actividades permisos_cuentas_x_actividades)
        {
            if (ModelState.IsValid)
            {
                repo.Update(permisos_cuentas_x_actividades);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Actividad = new SelectList(actividades.GetAll(), "Id_Actividad", "Nombre", permisos_cuentas_x_actividades.Id_Actividad);
            ViewBag.Id_Cuenta = new SelectList(cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta", permisos_cuentas_x_actividades.Id_Cuenta);
            ViewBag.Id_Permiso = new SelectList(permisos.GetAll(), "Id_Permiso", "Nombre", permisos_cuentas_x_actividades.Id_Permiso);
            return View(permisos_cuentas_x_actividades);
        }

        // GET: /Permisos_CuentasXActividades/Delete/5
        public ActionResult Delete(int id)
        {
            Permisos_Cuentas_x_Actividades permisos_cuentas_x_actividades = repo.Get(id);
            if (permisos_cuentas_x_actividades == null)
            {
                return HttpNotFound();
            }
            return View(permisos_cuentas_x_actividades);
        }

        // POST: /Permisos_CuentasXActividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permisos_Cuentas_x_Actividades permisos_cuentas_x_actividades = repo.Get(id);
            repo.Delete(permisos_cuentas_x_actividades);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                actividades.Dispose();
                cuentas.Dispose();
                permisos.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
