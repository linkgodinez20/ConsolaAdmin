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
        //public ActionResult Index()
        //{
        //    var cuentas = repo.GetAll().Include(c => c.Baja).Include(c => c.LogIn).Include(c => c.Perfiles).Include(c => c.Sistemas);
        //    return View(cuentas.ToList());
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Baja = String.IsNullOrEmpty(sortOrder) ? "Baja_desc" : "";
            ViewBag.Login = sortOrder == "Login" ? "Login" : "Login_desc";
            ViewBag.Perfil = sortOrder == "Perfil" ? "Perfil" : "Perfil_desc";
            ViewBag.Sistema = sortOrder == "Sistema" ? "Sistema" : "Sistema_desc";
            ViewBag.FechaCrea = sortOrder == "FechaCrea" ? "FechaCrea" : "FechaCrea_desc";
            ViewBag.FechaMod = sortOrder == "FechaMod" ? "FechaMod" : "FechaMod_desc";
            ViewBag.Intentos = sortOrder == "Intentos" ? "Intentos" : "Intentos_desc";            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Cuentas> cuentas = repo.GetAll().OrderBy(x => x.Perfiles.Nombre);

            var modelo = from s in cuentas select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Perfiles.Nombre.Contains(searchString) || s.Baja.Nombre.Contains(searchString) || s.LogIn.Usuario.Contains(searchString) || s.Sistemas.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Login":
                    modelo = modelo.OrderBy(s => s.LogIn.Usuario);
                    break;
                case "Login_desc":
                    modelo = modelo.OrderByDescending(s => s.LogIn.Usuario);
                    break;
                case "Perfil":
                    modelo = modelo.OrderBy(s => s.Perfiles.Nombre);
                    break;
                case "Perfil_desc":
                    modelo = modelo.OrderByDescending(s => s.Perfiles.Nombre);
                    break;
                case "Sistema":
                    modelo = modelo.OrderBy(s => s.Sistemas.Nombre);
                    break;
                case "Sistema_desc":
                    modelo = modelo.OrderByDescending(s => s.Sistemas.Nombre);
                    break;
                case "FechaCrea":
                    modelo = modelo.OrderBy(s => s.FechaCreacion);
                    break;
                case "FechaCrea_desc":
                    modelo = modelo.OrderByDescending(s => s.FechaCreacion);
                    break;
                case "FechaMod":
                    modelo = modelo.OrderBy(s => s.FechaModificacion);
                    break;
                case "FechaMod_desc":
                    modelo = modelo.OrderByDescending(s => s.FechaModificacion);
                    break;
                case "Intentos":
                    modelo = modelo.OrderBy(s => s.IntentosCnn);
                    break;
                case "Intentos_desc":
                    modelo = modelo.OrderByDescending(s => s.IntentosCnn);
                    break;
                case "Baja":
                    modelo = modelo.OrderBy(s => s.Baja.Nombre);
                    break;
                case "Baja_desc":
                    modelo = modelo.OrderByDescending(s => s.Baja.Nombre);
                    break;               
                default:
                    modelo = modelo.OrderBy(s => s.LogIn.Usuario);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Cuentas/Details/5
        public ActionResult Details(int id = 0)
        {
            Cuentas cuentas = repo.Get(id);
            if (cuentas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", cuentas);
        }

        // GET: /Cuentas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre");
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario");
            ViewBag.Id_Perfil = new SelectList(perfiles.GetAll(), "Id_Perfil", "Nombre");
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre");
            return PartialView("_Create");
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
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Cuentas");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", cuentas.Id_Baja);
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario", cuentas.Id_Login);
            ViewBag.Id_Perfil = new SelectList(perfiles.GetAll(), "Id_Perfil", "Nombre", cuentas.Id_Perfil);
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", cuentas.Id_Sistema);
            return PartialView("_Create", cuentas);
        }

        // GET: /Cuentas/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Cuentas cuentas =repo.Get(id);
            if (cuentas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", cuentas.Id_Baja);
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario", cuentas.Id_Login);
            ViewBag.Id_Perfil = new SelectList(perfiles.GetAll(), "Id_Perfil", "Nombre", cuentas.Id_Perfil);
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", cuentas.Id_Sistema);
            return PartialView("_Edit", cuentas);
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
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "AreasDeTrabajo");
                return Json(new { success = true, url = url });   
            }
            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", cuentas.Id_Baja);
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario", cuentas.Id_Login);
            ViewBag.Id_Perfil = new SelectList(perfiles.GetAll(), "Id_Perfil", "Nombre", cuentas.Id_Perfil);
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", cuentas.Id_Sistema);
            return PartialView("_Edit", cuentas);
        }

        // GET: /Cuentas/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Cuentas cuentas = repo.Get(id);
            if (cuentas == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", cuentas);
        }

        // POST: /Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Cuentas cuentas = repo.Get(id);
                repo.Delete(cuentas);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "AreasDeTrabajo", new { id = cuentas.Id_Cuenta });
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
                login.Dispose();
                perfiles.Dispose();
                sistemas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
