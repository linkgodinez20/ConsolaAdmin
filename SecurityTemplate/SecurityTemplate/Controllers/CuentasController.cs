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
    public class CuentasController : Controller
    {        
        private readonly IRepo<Cuentas> repo;
        private readonly IRepo<Baja> baja;
        private readonly IRepo<LogIn> login;
        private readonly IRepo<Perfiles> perfiles;
        private readonly IRepo<Sistemas> sistemas;

        public CuentasController(IRepo<Cuentas> Repo, IRepo<Baja> Baja, IRepo<LogIn> Login, IRepo<Perfiles> Perfiles, IRepo<Sistemas> Sistemas) 
        {
            this.repo = Repo;
            this.baja = Baja;
            this.login = Login;
            this.perfiles = Perfiles;
            this.sistemas = Sistemas;
        } 

        // GET: /Cuentas/
        public ActionResult Index()
        {
            var cuentas = repo.GetAll().Include(c => c.Baja).Include(c => c.LogIn).Include(c => c.Perfiles).Include(c => c.Sistemas);
            return View(cuentas.ToList());
        }

        // GET: /Cuentas/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuentas cuentas = repo.Get(id);
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            return View(cuentas);
        }

        // GET: /Cuentas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre");
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario");
            ViewBag.Id_Perfil = new SelectList(perfiles.GetAll(), "Id_Perfil", "Nombre");
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre");
            return View();
        }

        // POST: /Cuentas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Cuenta,Id_Login,Id_Perfil,FechaCreacion,FechaModificacion,IntentosCnn,InicioBloqueo,Id_Sistema,Estatus,Id_Baja,RegistroCompleto")] Cuentas cuentas)
        {
            if (ModelState.IsValid)
            {
                repo.Add(cuentas);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", cuentas.Id_Baja);
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario", cuentas.Id_Login);
            ViewBag.Id_Perfil = new SelectList(perfiles.GetAll(), "Id_Perfil", "Nombre", cuentas.Id_Perfil);
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", cuentas.Id_Sistema);
            return View(cuentas);
        }

        // GET: /Cuentas/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuentas cuentas =repo.Get(id);
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", cuentas.Id_Baja);
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario", cuentas.Id_Login);
            ViewBag.Id_Perfil = new SelectList(perfiles.GetAll(), "Id_Perfil", "Nombre", cuentas.Id_Perfil);
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", cuentas.Id_Sistema);        
            return View(cuentas);
        }

        // POST: /Cuentas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Cuenta,Id_Login,Id_Perfil,FechaCreacion,FechaModificacion,IntentosCnn,InicioBloqueo,Id_Sistema,Estatus,Id_Baja,RegistroCompleto")] Cuentas cuentas)
        {
            if (ModelState.IsValid)
            {
                repo.Update(cuentas);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", cuentas.Id_Baja);
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario", cuentas.Id_Login);
            ViewBag.Id_Perfil = new SelectList(perfiles.GetAll(), "Id_Perfil", "Nombre", cuentas.Id_Perfil);
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", cuentas.Id_Sistema);   
            return View(cuentas);
        }

        // GET: /Cuentas/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuentas cuentas = repo.Get(id);
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            return View(cuentas);
        }

        // POST: /Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuentas cuentas = repo.Get(id);
            repo.Delete(cuentas);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                baja.Dispose();
                login.Dispose();
                perfiles.Dispose();
                sistemas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
