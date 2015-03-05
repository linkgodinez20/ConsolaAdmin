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
    public class LoginController : Controller
    {        
        private readonly IRepo<LogIn> repo;
        private readonly IRepo<Personas> personas;
        public LoginController(IRepo<LogIn> Repo, IRepo<Personas> Personas) 
        {
            this.repo = Repo;
            this.personas = Personas;
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

        // GET: /Login/Details/5
        public ActionResult Details(int id = 0)
        {
            LogIn login = repo.Get(id);
            if (login == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Details", login);
        }

        // GET: /Login/Create
        public ActionResult Create()
        {
            ViewBag.Id_Persona = new SelectList(personas.GetAll(), "Id_Persona", "APaterno");
            return PartialView("_Create");
        }

        // POST: /Login/Create
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

        // GET: /Login/Edit/5
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

        // POST: /Login/Edit/5
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

        // GET: /Login/Delete/5
        public ActionResult Delete(int id = 0)
        {
            LogIn login = repo.Get(id);
            if (login == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return PartialView("_Delete", login);
        }

        // POST: /Login/Delete/5
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
    }
}
