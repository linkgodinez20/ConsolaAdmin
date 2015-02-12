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
    public class ContactosController : Controller
    {
        private readonly IRepo<Contactos> repo;
        private readonly IRepo<Contacto_tipo> Repo_ContactoTipo;
        private readonly IRepo<Contacto_medio> Repo_ContactoMedio;

        public ContactosController(IRepo<Contactos> repo, IRepo<Contacto_tipo> Repo_ContactoTipo, IRepo<Contacto_medio> Repo_ContactoMedio)
        {
            this.repo = repo;
            this.Repo_ContactoTipo = Repo_ContactoTipo;
            this.Repo_ContactoMedio = Repo_ContactoMedio;
        }

        // GET: Contactos
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;

            ViewBag.SortByContactoTipo = sortOrder == "Tipo_desc" ? "Tipo_desc" : "Tipo";
            ViewBag.SortByContactoMedio = sortOrder == "Medio_desc" ? "Medio_desc" : "Medio";

            //Default
            ViewBag.SortByContacto = String.IsNullOrEmpty(sortOrder) ? "Contacto_desc" : "Contacto";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Contactos> ordena_contactos = repo.GetAll()
                .OrderBy(x => x.Contacto_tipo.Nombre).OrderBy(y => y.Contacto_medio.Nombre).OrderBy(z => z.Contacto);

            var actividades = from s in ordena_contactos
                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                actividades = actividades.Where(s => s.Contacto_tipo.Nombre.Contains(searchString)
                                       || s.Contacto_medio.Nombre.Contains(searchString)
                                       || s.Contacto.Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "Medio":
                    actividades = actividades.OrderBy(p => p.Contacto_medio.Nombre);
                    break;
                case "Medio_desc":
                    actividades = actividades.OrderByDescending(p => p.Contacto_medio.Nombre);
                    break;
                case "Tipo":
                    actividades = actividades.OrderBy(p => p.Contacto_tipo.Nombre);
                    break;
                case "Tipo_desc":
                    actividades = actividades.OrderByDescending(p => p.Contacto_tipo.Nombre);
                    break;
                case "Contacto":
                    actividades = actividades.OrderBy(p => p.Contacto);
                    break;
                case "Contacto_desc":
                    actividades = actividades.OrderByDescending(p => p.Contacto);
                    break;
                default:
                    actividades = actividades.OrderBy(ct => ct.Contacto_tipo.Nombre).OrderBy(cm => cm.Contacto_medio.Nombre).OrderBy(c => c.Contacto);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(actividades.ToPagedList(pageNumber, pageSize));


            //var contacto = repo.GetAll().Include(c => c.Contacto_medio).Include(c => c.Contacto_tipo);
            //return View(contacto.ToList());
        }

        // GET: Contactos/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Contactos contacto = repo.Get(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // GET: Contactos/Create
        public ActionResult Create()
        {
            ViewBag.Id_ContactoMedio = new SelectList(Repo_ContactoMedio.GetAll(), "Id_ContactoMedio", "Nombre");
            ViewBag.Id_ContactoTipo = new SelectList(Repo_ContactoTipo.GetAll(), "Id_ContactoTipo", "Nombre");
            return View();
        }

        // POST: Contactos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Contacto,Id_ContactoTipo,Contacto1,Id_ContactoMedio,Notas,Estatus")] Contactos contacto)
        {
            if (ModelState.IsValid)
            {
                repo.Add(contacto);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_ContactoMedio = new SelectList(Repo_ContactoMedio.GetAll(), "Id_ContactoMedio", "Nombre", contacto.Id_ContactoMedio);
            ViewBag.Id_ContactoTipo = new SelectList(Repo_ContactoTipo.GetAll(), "Id_ContactoTipo", "Nombre", contacto.Id_ContactoTipo);
            return View(contacto);
        }

        // GET: Contactos/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactos contacto = repo.Get(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_ContactoMedio = new SelectList(Repo_ContactoMedio.GetAll(), "Id_ContactoMedio", "Nombre", contacto.Id_ContactoMedio);
            ViewBag.Id_ContactoTipo = new SelectList(Repo_ContactoTipo.GetAll(), "Id_ContactoTipo", "Nombre", contacto.Id_ContactoTipo);
            return View(contacto);
        }

        // POST: Contactos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Contacto,Id_ContactoTipo,Contacto1,Id_ContactoMedio,Notas,Estatus")] Contactos contacto)
        {
            if (ModelState.IsValid)
            {
                repo.Update(contacto);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_ContactoMedio = new SelectList(Repo_ContactoMedio.GetAll(), "Id_ContactoMedio", "Nombre", contacto.Id_ContactoMedio);
            ViewBag.Id_ContactoTipo = new SelectList(Repo_ContactoTipo.GetAll(), "Id_ContactoTipo", "Nombre", contacto.Id_ContactoTipo);
            return View(contacto);
        }

        // GET: Contactos/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactos contacto = repo.Get(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contactos contacto = repo.Get(id);
            repo.Delete(contacto);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                Repo_ContactoMedio.Dispose();
                Repo_ContactoTipo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
