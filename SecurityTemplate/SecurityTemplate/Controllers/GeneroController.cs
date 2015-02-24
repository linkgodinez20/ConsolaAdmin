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
    public class GeneroController : Controller
    {
        private readonly IRepo<Genero> repo;

        public GeneroController(IRepo<Genero> repo)
        {
            this.repo = repo;
        }

        // GET: Genero
        public ActionResult Index()
        {
            return View(repo.GetAll().ToList());
        }

        // GET: Genero/Details/5
        public ActionResult Details(byte id = 0)
        {
            Genero genero = repo.Get(id);
            if (genero == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return View(genero);
        }

        // GET: Genero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Genero,Nombre")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                repo.Add(genero);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(genero);
        }

        // GET: Genero/Edit/5
        public ActionResult Edit(byte id = 0)
        {
            Genero genero = repo.Get(id);
            if (genero == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return View(genero);
        }

        // POST: Genero/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Genero,Nombre")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                repo.Update(genero);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(genero);
        }

        // GET: Genero/Delete/5
        public ActionResult Delete(byte id = 0)
        {
            Genero genero = repo.Get(id);
            if (genero == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return View(genero);
        }

        // POST: Genero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Genero genero = repo.Get(id);
            repo.Delete(genero);
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
