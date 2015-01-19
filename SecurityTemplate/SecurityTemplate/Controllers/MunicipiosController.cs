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
    public class MunicipiosController : Controller
    {
        private readonly IRepo<Municipios> repo;
        private readonly IRepo<Paises> Repo_Pais;
        private readonly IRepo<Entidades> Repo_Entidad;

        public MunicipiosController(IRepo<Municipios> repo, IRepo<Paises> Repo_Pais, IRepo<Entidades> Repo_Entidad)
        {
            this.repo = repo;
            this.Repo_Pais = Repo_Pais;
            this.Repo_Entidad = Repo_Entidad;
        }

        // GET: Municipios
        public ActionResult Index()
        {
            //var municipios = db.Municipios.Include(m => m.Entidades);
            var municipios = repo.GetAll().Include(m => m.Entidades);
            return View(municipios.ToList());
        }

        // GET: Municipios/Details/5
        public ActionResult Details(short id)
        {

            Municipios municipios = repo.Get(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            return View(municipios);
        }

        // GET: Municipios/Create
        public ActionResult Create()
        {
            //ViewBag.Id_Pais = new SelectList(db.Entidades, "Id_Pais", "Nombre"); //Original
            ViewBag.Id_Pais = new SelectList(Repo_Pais.GetAll(), "Id_Pais", "Nombre");
            ViewBag.Id_Entidad = new SelectList(Repo_Entidad.GetAll(), "Id_Entidad", "Nombre");
            ViewBag.Id_Municipio = new SelectList(repo.GetAll(), "Id_Municipio", "Nombre");
            return View();
        }

        // POST: Municipios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Pais,Id_Entidad,Id_Municipio,Nombre,Abreviatura")] Municipios municipios)
        {
            if (ModelState.IsValid)
            {
                repo.Add(municipios);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Pais = new SelectList(Repo_Pais.GetAll(), "Id_Pais", "Nombre");
            ViewBag.Id_Entidad = new SelectList(Repo_Entidad.GetAll(), "Id_Entidad", "Nombre", municipios.Id_Pais);
            ViewBag.Id_Municipio = new SelectList(repo.GetAll(), "Id_Municipio", "Nombre", municipios.Id_Entidad);

            return View(municipios);
        }

        // GET: Municipios/Edit/5
        public ActionResult Edit(short id)
        {
            Municipios municipios = repo.Get(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Pais = new SelectList(Repo_Entidad.GetAll(), "Id_Pais", "Nombre", municipios.Id_Pais);
            ViewBag.Id_Entidad = new SelectList(Repo_Entidad.GetAll(), "Id_Entidad", "Nombre", municipios.Id_Entidad);
            ViewBag.Id_Municipio = new SelectList(repo.GetAll(), "Id_Municipio", "Nombre", municipios.Id_Municipio);


            return View(municipios);
        }

        // POST: Municipios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Pais,Id_Entidad,Id_Municipio,Nombre,Abreviatura")] Municipios municipios)
        {
            if (ModelState.IsValid)
            {
                repo.Update(municipios);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Pais = new SelectList(Repo_Entidad.GetAll(), "Id_Pais", "Nombre", municipios.Id_Pais);
            return View(municipios);
        }

        // GET: Municipios/Delete/5
        public ActionResult Delete(short id)
        {
            Municipios municipios = repo.Get(id);
            if (municipios == null)
            {
                return HttpNotFound();
            }
            return View(municipios);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Municipios municipios = repo.Get(id);
            repo.Delete(municipios);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                Repo_Entidad.Dispose();
                Repo_Pais.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
