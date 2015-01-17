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
    public class Baja_MotivosController : Controller
    {        
        private readonly IRepo<Baja_motivos> repo;
        private readonly IRepo<Baja> baja;

        public Baja_MotivosController(IRepo<Baja_motivos> Repo, IRepo<Baja> Baja)
        {
            this.repo = Repo;
            this.baja = Baja;
        }

        // GET: /Baja_Motivos/
        public ActionResult Index()
        {
            var baja_motivos = repo.GetAll();
            return View(baja_motivos.ToList());
        }

        // GET: /Baja_Motivos/Details/5
        public ActionResult Details(int id)//byte
        {
            Baja_motivos baja_motivos = repo.Get(id);
            if (baja_motivos == null)
            {
                return HttpNotFound();
            }
            return View(baja_motivos);
        }

        // GET: /Baja_Motivos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre");
            return View();
        }

        // POST: /Baja_Motivos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_MotivoBaja,Descripcion,Id_Baja")] Baja_motivos baja_motivos)
        {
            if (ModelState.IsValid)
            {
                repo.Add(baja_motivos);                
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", baja_motivos.Id_Baja);
            return View(baja_motivos);
        }

        // GET: /Baja_Motivos/Edit/5
        public ActionResult Edit(byte id)
        {
            Baja_motivos baja_motivos = repo.Get(id);
            if (baja_motivos == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", baja_motivos.Id_Baja);
            return View(baja_motivos);
        }

        // POST: /Baja_Motivos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_MotivoBaja,Descripcion,Id_Baja")] Baja_motivos baja_motivos)
        {
            if (ModelState.IsValid)
            {
                repo.Update(baja_motivos);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Baja = new SelectList(baja.GetAll(), "Id_Baja", "Nombre", baja_motivos.Id_Baja);
            return View(baja_motivos);
        }

        // GET: /Baja_Motivos/Delete/5
        public ActionResult Delete(byte id)
        {           
            Baja_motivos baja_motivos = repo.Get(id);
            if (baja_motivos == null)
            {
                return HttpNotFound();
            }
            return View(baja_motivos);
        }

        // POST: /Baja_Motivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Baja_motivos baja_motivos = repo.Get(id);
            repo.Delete(baja_motivos);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                baja.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
