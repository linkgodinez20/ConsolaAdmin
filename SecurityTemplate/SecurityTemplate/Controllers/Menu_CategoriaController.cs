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
    public class Menu_CategoriaController : Controller
    {
        private readonly IRepo<Menu_categoria> repo;

        public Menu_CategoriaController(IRepo<Menu_categoria> Repo)
        {
            this.repo = Repo;
        }

        // GET: /MenuCategoria/
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        // GET: /MenuCategoria/Details/5
        public ActionResult Details(short id)
        {
            Menu_categoria menu_categoria = repo.Get(id);
            if (menu_categoria == null)
            {
                return HttpNotFound();
            }
            return View(menu_categoria);
        }

        // GET: /MenuCategoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /MenuCategoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_MenuCategoria,Nombre,Orden")] Menu_categoria menu_categoria)
        {
            if (ModelState.IsValid)
            {
                repo.Add(menu_categoria);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(menu_categoria);
        }

        // GET: /MenuCategoria/Edit/5
        public ActionResult Edit(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu_categoria menu_categoria = repo.Get(id);
            if (menu_categoria == null)
            {
                return HttpNotFound();
            }
            return View(menu_categoria);
        }

        // POST: /MenuCategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_MenuCategoria,Nombre,Orden")] Menu_categoria menu_categoria)
        {
            if (ModelState.IsValid)
            {
                repo.Update(menu_categoria);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(menu_categoria);
        }

        // GET: /MenuCategoria/Delete/5
        public ActionResult Delete(short id)
        {
            Menu_categoria menu_categoria = repo.Get(id);
            if (menu_categoria == null)
            {
                return HttpNotFound();
            }
            return View(menu_categoria);
        }

        // POST: /MenuCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Menu_categoria menu_categoria = repo.Get(id);
            repo.Delete(menu_categoria);
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
