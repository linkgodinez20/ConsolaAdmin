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
    public class Contacto_MedioController : Controller
    {
        private readonly IRepo<Contacto_medio> repo;       

        public Contacto_MedioController( IRepo<Contacto_medio> Repo)
        {
            this.repo = Repo;
        }

        // GET: /Contacto_Medio/
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        // GET: /Contacto_Medio/Details/5
        public ActionResult Details(int id)//Byte
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto_medio contacto_medio = repo.Get(id);
            if (contacto_medio == null)
            {
                return HttpNotFound();
            }
            return View(contacto_medio);
        }

        // GET: /Contacto_Medio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Contacto_Medio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_ContactoMedio,Nombre")] Contacto_medio contacto_medio)
        {
            if (ModelState.IsValid)
            {
                repo.Add(contacto_medio);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(contacto_medio);
        }

        // GET: /Contacto_Medio/Edit/5
        public ActionResult Edit(int id)//Byte
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto_medio contacto_medio = repo.Get(id);
            if (contacto_medio == null)
            {
                return HttpNotFound();
            }
            return View(contacto_medio);
        }

        // POST: /Contacto_Medio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_ContactoMedio,Nombre")] Contacto_medio contacto_medio)
        {
            if (ModelState.IsValid)
            {
                repo.Update(contacto_medio);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(contacto_medio);
        }

        // GET: /Contacto_Medio/Delete/5
        public ActionResult Delete(int id)//Byte
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto_medio contacto_medio = repo.Get(id);
            if (contacto_medio == null)
            {
                return HttpNotFound();
            }
            return View(contacto_medio);
        }

        // POST: /Contacto_Medio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)//Byte
        {
            Contacto_medio contacto_medio = repo.Get(id);
            repo.Delete(contacto_medio);
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
