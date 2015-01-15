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
using Ninject;

namespace Security.Controllers
{
    public class PersonasController : Controller
    {
        private readonly IRepo<Personas> repo;
        private readonly IRepo<Genero> genero;

        public PersonasController(IRepo<Personas> repo, IRepo<Genero> genero)
        {
            this.repo = repo;
            this.genero = genero;
        }

        // GET: Personas
        public ActionResult Index()
        {
            var personas = repo.GetAll().Include(p => p.Genero);
            return View(personas.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int id)
        {
            Personas personas = repo.Get(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }


        // GET: Personas/Create        
        public ActionResult Create()
        {            
            ViewBag.Id_Genero = new SelectList(genero.GetAll(), "Id_Genero", "Nombre");
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Persona,APaterno,AMaterno,Nombre,FechaCreacion,Email,Id_Genero,FechaNacimiento,RFC,Homoclave,CURP,Foto,Estatus")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                repo.Add(personas);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Genero = new SelectList(genero.GetAll(), "Id_Genero", "Nombre", personas.Id_Genero);
            return View(personas);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int id)
        {
            Personas personas = repo.Get(id);
            if (personas == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Genero = new SelectList(genero.GetAll(), "Id_Genero", "Nombre", personas.Id_Genero);
            return View(personas);
        }

        // POST: Personas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Persona,APaterno,AMaterno,Nombre,FechaCreacion,Email,Id_Genero,FechaNacimiento,RFC,Homoclave,CURP,Foto,Estatus")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                repo.Update(personas);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Genero = new SelectList(genero.GetAll(), "Id_Genero", "Nombre", personas.Id_Genero);
            return View(personas);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int id)
        {
            Personas personas = repo.Get(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personas personas = repo.Get(id);
            repo.Delete(personas);
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
