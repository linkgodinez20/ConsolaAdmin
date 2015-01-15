﻿using System;
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
    public class Equipos_TipoController : Controller
    {        
        private readonly IRepo<Equipos_tipo> repo;

        public Equipos_TipoController(IRepo<Equipos_tipo> Repo)
        {
            this.repo = Repo;
        }

        // GET: /Equipos_Tipo/
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        // GET: /Equipos_Tipo/Details/5
        public ActionResult Details(byte id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos_tipo equipos_tipo = repo.Get(id);
            if (equipos_tipo == null)
            {
                return HttpNotFound();
            }
            return View(equipos_tipo);
        }

        // GET: /Equipos_Tipo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Equipos_Tipo/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_EquipoTipo,Nombre")] Equipos_tipo equipos_tipo)
        {
            if (ModelState.IsValid)
            {
                repo.Add(equipos_tipo);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(equipos_tipo);
        }

        // GET: /Equipos_Tipo/Edit/5
        public ActionResult Edit(byte id)
        {
            Equipos_tipo equipos_tipo = repo.Get(id);
            if (equipos_tipo == null)
            {
                return HttpNotFound();
            }
            return View(equipos_tipo);
        }

        // POST: /Equipos_Tipo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_EquipoTipo,Nombre")] Equipos_tipo equipos_tipo)
        {
            if (ModelState.IsValid)
            {
                repo.Update(equipos_tipo);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(equipos_tipo);
        }

        // GET: /Equipos_Tipo/Delete/5
        public ActionResult Delete(byte id)
        {
            Equipos_tipo equipos_tipo = repo.Get(id);
            if (equipos_tipo == null)
            {
                return HttpNotFound();
            }
            return View(equipos_tipo);
        }

        // POST: /Equipos_Tipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Equipos_tipo equipos_tipo = repo.Get(id);
            repo.Delete(equipos_tipo);
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