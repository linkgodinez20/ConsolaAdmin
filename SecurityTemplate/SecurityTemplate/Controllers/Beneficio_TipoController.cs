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
    public class Beneficio_TipoController : Controller
    {
        private readonly IRepo<Beneficio_tipo> repo;

        public Beneficio_TipoController(IRepo<Beneficio_tipo> Repo) 
        {
            this.repo = Repo;
        }

        // GET: /Beneficio_Tipo/
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        // GET: /Beneficio_Tipo/Details/5
        public ActionResult Details(int id)//Byte
        {
            Beneficio_tipo beneficio_tipo = repo.Get(id);
            if (beneficio_tipo == null)
            {
                return HttpNotFound();
            }
            return View(beneficio_tipo);
        }

        // GET: /Beneficio_Tipo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Beneficio_Tipo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_BeneficioTipo,Nombre")] Beneficio_tipo beneficio_tipo)
        {
            if (ModelState.IsValid)
            {
                repo.Add(beneficio_tipo);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(beneficio_tipo);
        }

        // GET: /Beneficio_Tipo/Edit/5
        public ActionResult Edit(int id)//Byte
        {
            Beneficio_tipo beneficio_tipo = repo.Get(id);
            if (beneficio_tipo == null)
            {
                return HttpNotFound();
            }

            return View(beneficio_tipo);
        }

        // POST: /Beneficio_Tipo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_BeneficioTipo,Nombre")] Beneficio_tipo beneficio_tipo)
        {         
            if (ModelState.IsValid)
            {
                repo.Update(beneficio_tipo);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(beneficio_tipo);
        }

        // GET: /Beneficio_Tipo/Delete/5
        public ActionResult Delete(int id)//Byte
        {
            Beneficio_tipo beneficio_tipo = repo.Get(id);
            if (beneficio_tipo == null)
            {
                return HttpNotFound();
            }
            return View(beneficio_tipo);
        }

        // POST: /Beneficio_Tipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Beneficio_tipo beneficio_tipo = repo.Get(id);
            repo.Delete(beneficio_tipo);
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
