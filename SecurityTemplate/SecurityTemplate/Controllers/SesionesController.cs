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
    public class SesionesController : Controller
    {
        private readonly IRepo<Sesiones> repo;
        private readonly IRepo<Cuentas> Repo_Cuentas;
        private readonly IRepo<Sistemas> Repo_Sistemas;

        public SesionesController(IRepo<Sesiones> repo, IRepo<Cuentas> Repo_Cuentas, IRepo<Sistemas> Repo_Sistemas)
        {
            this.repo = repo;
            this.Repo_Cuentas = Repo_Cuentas;
            this.Repo_Sistemas = Repo_Sistemas;
        }

        // GET: Sesiones
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SortByCuenta = sortOrder == "Cuenta_desc" ? "Cuenta_desc" : "Cuenta";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Sesiones> ordena_sesiones = repo.GetAll()
                .OrderBy(x => x.Cuentas.LogIn.Usuario);

            var sesiones = from s in ordena_sesiones
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sesiones = sesiones.Where(d => d.Cuentas.LogIn.Usuario.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Cuenta_desc":
                    sesiones = sesiones.OrderByDescending(p => p.Cuentas.LogIn.Usuario);
                    break;
                case "Cuenta":
                    sesiones = sesiones.OrderBy(p => p.Cuentas.LogIn.Usuario);
                    break;

                default:
                    sesiones = sesiones.OrderBy(p => p.Cuentas.LogIn.Usuario).OrderBy(o => o.OnLine).OrderByDescending(q => q.FechaHoraInicio);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(sesiones.ToPagedList(pageNumber, pageSize));	

            //var sesiones = db.Sesiones.Include(s => s.Cuentas).Include(s => s.Sistemas);
            //return View(sesiones.ToList());
        }

        // GET: Sesiones/Details/5
        public ActionResult Details(int id = 0)
        {
            Sesiones sesiones = repo.Get(id);
            if (sesiones == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return View(sesiones);
        }

        

        // GET: Sesiones/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Sesiones sesiones = repo.Get(id);
            if (sesiones == null || id == 0)
            {
                return RedirectToAction("index");
            }

            //ViewBag.Id_Cuenta = new SelectList(Repo_Cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta", sesiones.Id_Cuenta);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", sesiones.Id_Sistema);

            return View(sesiones);
        }

        // POST: Sesiones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Sesion,Identificador,Id_Cuenta,FechaHoraInicio,OnLine,CierreSesion,UltimoMovimiento,Id_Sistema,Estatus")] Sesiones sesiones)
        {
            if (ModelState.IsValid)
            {
                sesiones.FechaHoraInicio = Convert.ToDateTime(sesiones.FechaHoraInicio);
                sesiones.UltimoMovimiento = DateTime.Now;
                repo.Update(sesiones);
                repo.Save();
                return RedirectToAction("Index");
            }

            //ViewBag.Id_Cuenta = new SelectList(Repo_Cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta", sesiones.Id_Cuenta);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", sesiones.Id_Sistema);

            return View(sesiones);
        }

        // GET: Sesiones/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Sesiones sesiones = repo.Get(id);
            if (sesiones == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return View(sesiones);
        }

        // POST: Sesiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sesiones sesiones = repo.Get(id);
            repo.Delete(sesiones);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                Repo_Sistemas.Dispose();
                Repo_Cuentas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
