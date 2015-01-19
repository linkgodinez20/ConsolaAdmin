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
using Ninject;
using PagedList;

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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            //ViewBag.SortByAFullName = String.IsNullOrEmpty(sortOrder) ? "FullName" : "";

            ViewBag.SortByAPaterno = sortOrder == "APaterno_desc" ? "APaterno_desc" : "APaterno";
            ViewBag.SortByAMaterno = sortOrder == "AMaterno" ? "AMaterno_desc" : "AMaterno";
            ViewBag.SortByNombre = sortOrder == "Nombre" ? "Nombre_desc" : "Nombre";
            ViewBag.SortByEmail = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.SortByRFC = sortOrder == "RFC" ? "RFC_desc" : "RFC";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Personas> ordena_personas = repo.GetAll()
                .OrderBy(x => x.APaterno).OrderBy(y => y.AMaterno).OrderBy(n => n.Nombre);

            var personas = from s in ordena_personas
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                personas = personas.Where(s => s.APaterno.Contains(searchString)
                                       || s.AMaterno.Contains(searchString)
                                       || s.Nombre.Contains(searchString)
                                       || s.Email.Contains(searchString)
                                       || s.RFC.Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "APaterno":
                    personas = personas.OrderBy(p => p.APaterno);
                    break;
                case "APaterno_desc":
                    personas = personas.OrderByDescending(p => p.APaterno);
                    break;
                case "AMaterno":
                    personas = personas.OrderBy(p => p.AMaterno);
                    break;
                case "AMaterno_desc":
                    personas = personas.OrderByDescending(p => p.AMaterno);
                    break;
                case "Nombre":
                    personas = personas.OrderBy(p => p.Nombre);
                    break;
                case "Nombre_desc":
                    personas = personas.OrderByDescending(p => p.Nombre);
                    break;
                case "Email":
                    personas = personas.OrderBy(p => p.Email);
                    break;
                case "Email_desc":
                    personas = personas.OrderByDescending(p => p.Email);
                    break;
                case "RFC":
                    personas = personas.OrderBy(p => p.RFC);
                    break;
                case "RFC_desc":
                    personas = personas.OrderByDescending(p => p.RFC);
                    break;
                default:  // Nombre ascendente
                    personas = personas.OrderBy(ap => ap.APaterno).OrderBy(am => am.AMaterno).OrderBy(n => n.Nombre);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //productos = db.Producto.Include(p => p.ProductoTipo).Include(p => p.UnidadMedida);

            return View(personas.ToPagedList(pageNumber, pageSize));
            //var personas = repo.GetAll().Include(p => p.Genero);
            //return View(personas.ToList());
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
