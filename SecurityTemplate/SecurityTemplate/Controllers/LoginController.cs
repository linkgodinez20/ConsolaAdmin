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
    public class LoginController : Controller
    {        
        private readonly IRepo<LogIn> repo;
        private readonly IRepo<Personas> personas;
        public LoginController(IRepo<LogIn> Repo, IRepo<Personas> Personas) 
        {
            this.repo = Repo;
            this.personas = Personas;
        }

        // GET: /Login/
        public ActionResult Index()
        {
            var login = repo.GetAll().Include(l => l.Personas);
            return View(login.ToList());
        }

        // GET: /Login/Details/5
        public ActionResult Details(int id)
        {
            LogIn login = repo.Get(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: /Login/Create
        public ActionResult Create()
        {
            ViewBag.Id_Persona = new SelectList(personas.GetAll(), "Id_Persona", "APaterno");
            return View();
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
                return RedirectToAction("Index");
            }

            ViewBag.Id_Persona = new SelectList(personas.GetAll(), "Id_Persona", "APaterno", login.Id_Persona);
            return View(login);
        }

        // GET: /Login/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogIn login = repo.Get(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Persona = new SelectList(personas.GetAll(), "Id_Persona", "APaterno", login.Id_Persona);
            return View(login);
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
                return RedirectToAction("Index");
            }
            ViewBag.Id_Persona = new SelectList(personas.GetAll(), "Id_Persona", "APaterno", login.Id_Persona);
            return View(login);
        }

        // GET: /Login/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogIn login = repo.Get(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: /Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogIn login = repo.Get(id);
            repo.Delete(login);
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
