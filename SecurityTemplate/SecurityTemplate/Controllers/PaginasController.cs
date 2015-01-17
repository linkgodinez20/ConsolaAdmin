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
    public class PaginasController : Controller
    {
        private readonly IRepo<Paginas> repo;
        private readonly IRepo<Sistemas> sistemas;
        public PaginasController(IRepo<Paginas> Repo, IRepo<Sistemas> Sistemas)
        {
            this.repo = Repo;
            this.sistemas = Sistemas;
        }      

        // GET: /Paginas/
        public ActionResult Index()
        {
            var paginas = repo.GetAll().Include(p => p.Sistemas);
            return View(paginas.ToList());
        }

        // GET: /Paginas/Details/5
        public ActionResult Details(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paginas paginas = repo.Get(id);
            if (paginas == null)
            {
                return HttpNotFound();
            }
            return View(paginas);
        }

        // GET: /Paginas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre");
            return View();
        }

        // POST: /Paginas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Pagina,Nombre,Titulo,Id_Sistema")] Paginas paginas)
        {
            if (ModelState.IsValid)
            {
                repo.Add(paginas);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", paginas.Id_Sistema);
            return View(paginas);
        }

        // GET: /Paginas/Edit/5
        public ActionResult Edit(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paginas paginas = repo.Get(id);
            if (paginas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", paginas.Id_Sistema);
            return View(paginas);
        }

        // POST: /Paginas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Pagina,Nombre,Titulo,Id_Sistema")] Paginas paginas)
        {
            if (ModelState.IsValid)
            {
                repo.Update(paginas);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Sistema = new SelectList(sistemas.GetAll(), "Id_Sistema", "Nombre", paginas.Id_Sistema);
            return View(paginas);
        }

        // GET: /Paginas/Delete/5
        public ActionResult Delete(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paginas paginas = repo.Get(id);
            if (paginas == null)
            {
                return HttpNotFound();
            }
            return View(paginas);
        }

        // POST: /Paginas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Paginas paginas = repo.Get(id);
            repo.Delete(paginas);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                sistemas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
