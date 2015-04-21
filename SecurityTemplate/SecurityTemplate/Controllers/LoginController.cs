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
using Security.Services;

namespace Security.Controllers
{
    public class LoginController : Controller, IUserService
    {        
        private readonly IRepo<LogIn> repo;
        private readonly IRepo<Personas> personas; 
        private readonly IRepo<Cuentas> cuentas;
        private readonly IRepo<Cuentas_x_Personas> cuentasXPers;        
        public LoginController(IRepo<LogIn> Repo, IRepo<Personas> Personas, IRepo<Cuentas> Cuentas, IRepo<Cuentas_x_Personas> CuentasXPers)
        {
            this.repo = Repo;
            this.personas = Personas;
            this.cuentas = Cuentas;
            this.cuentasXPers = CuentasXPers;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Usuario = String.IsNullOrEmpty(sortOrder) ? "Usuario_desc" : "";
            ViewBag.Persona = sortOrder == "Persona" ? "Persona" : "Persona_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<LogIn> login = repo.GetAll().OrderBy(x => x.Usuario);

            var modelo = from s in login select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Usuario.Contains(searchString) || s.Personas.APaterno.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Usuario":
                    modelo = modelo.OrderBy(s => s.Usuario);
                    break;
                case "Usuario_desc":
                    modelo = modelo.OrderByDescending(s => s.Usuario);
                    break;
                case "Persona":
                    modelo = modelo.OrderBy(s => s.Personas.APaterno);
                    break;
                case "Persona_desc":
                    modelo = modelo.OrderByDescending(s => s.Personas.APaterno);
                    break;  
                default:
                    modelo = modelo.OrderBy(s => s.Usuario);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult Details(int id = 0)
        {
            LogIn login = repo.Get(id);
            if (login == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", login);
        }

        public ActionResult Create()
        {
            ViewBag.Id_Persona = new SelectList(personas.GetAll(), "Id_Persona", "APaterno");
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Login,Usuario,Senha,Salt,UsoSalt,Id_Persona")] LogIn login)
        {
            if (ModelState.IsValid)
            {
                repo.Add(login);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Login");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_Persona = new SelectList(personas.GetAll(), "Id_Persona", "APaterno", login.Id_Persona);
            return PartialView("_Create", login);
        }

        public ActionResult Edit(int id = 0)
        {            
            LogIn login = repo.Get(id);
            if (login == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Id_Persona = new SelectList(personas.GetAll(), "Id_Persona", "APaterno", login.Id_Persona);
            return PartialView("_Edit", login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Login,Usuario,Senha,Salt,UsoSalt,Id_Persona")] LogIn login)
        {
            if (ModelState.IsValid)
            {
                repo.Update(login); 
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Login");
                return Json(new { success = true, url = url });  
            }
            ViewBag.Id_Persona = new SelectList(personas.GetAll(), "Id_Persona", "APaterno", login.Id_Persona);
            return PartialView("_Edit", login);
        }

        public ActionResult Delete(int id = 0)
        {
            LogIn login = repo.Get(id);
            if (login == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", login);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LogIn login = repo.Get(id);
                repo.Delete(login);
                repo.Save();
                //return RedirectToAction("Index");
                string url = Url.Action("Index", "Login", new { id = login.Id_Login });
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
                personas.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool IsUnique(string login)
        {
            LogIn cosa = repo.Get();
            throw new NotImplementedException();
        }

        public bool CambiarPassword(int id, string OldPassword, string newPassword)
        {
            bool res = false;
            LogIn login = repo.Get(id);
            if (login.Senha.Equals(OldPassword)) 
            {
                login.Senha = newPassword;
                repo.Update(login);
                repo.Save();
                string url = Url.Action("Index", "Login");                  
            }              
            return res;
        }
                
        public void Ingresar([Bind(Include = "Usuario,Senha")] LogIn login)
        {
            Services.
            UsuarioIngreso user = new UsuarioIngreso();
            Session["NombreUsr"] = null;
            Session["IDUser"] = null;
            Session["FotoUsr"] = null;
            try 
            {
                var query = from l in repo.GetAll()
                            join p in personas.GetAll() on l.Id_Persona equals p.Id_Persona
                            join c in cuentas.GetAll() on l.Id_Login equals c.Id_Login
                            join cp in cuentasXPers.GetAll() on p.Id_Persona equals cp.Id_Persona
                            where l.Usuario == login.Usuario && l.Senha == login.Senha
                            select new
                            {
                                l.Usuario,
                                l.Senha,
                                p.Nombre,
                                p.APaterno,
                                p.AMaterno,
                                p.Email,
                                p.Foto,
                                c.Id_Cuenta,
                                p.Id_Persona
                            };
                foreach (var usr in query)
                {
                    user.Usuario = usr.Usuario;
                    user.Contraeña = usr.Senha;
                    user.Nombre = usr.Nombre; 
                    user.APaterno = usr.APaterno;
                    user.AMaterno = usr.AMaterno;
                    user.Email = usr.Email;
                    user.FotoUrl = usr.Foto;
                    user.IdCuenta = usr.Id_Cuenta;
                    user.IdPersona = usr.Id_Persona;                    
                    Console.WriteLine(usr.Nombre+" "+ usr.APaterno+" "+usr.Foto);
                }
                Session["NombreUsr"] = user.Nombre + " " + user.APaterno;
                Session["IDUser"] = user.IdPersona;
                Session["FotoUsr"] = user.FotoUrl;
                             
                if (System.Web.HttpContext.Current.Cache["Usuario"] == null)
                {                    
                    System.Web.HttpContext.Current.Cache["Usuario"] = user.Nombre +" "+ user.APaterno +" "+user.AMaterno;
                }                
            }
            catch (Exception ex)
            {                
                ViewBag.Error = ex.Message;
                ViewBag.True = 1;                
            }            
        }

        public void CerrarSesiom(int id)
        {
            Session["NombreUsr"] = null;
            Session["IDUser"] = null;
            Session["FotoUsr"] = null;
            Session.Timeout = 5;
        }

        public ActionResult Create()
        {
            HttpCookie cookie = new HttpCookie("Cookie");
            cookie.Value = "Hello Cookie! CreatedOn: " + DateTime.Now.ToShortTimeString();
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Remove()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
