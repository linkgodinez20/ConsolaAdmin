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

            var contactos = from s in ordena_contactos
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                contactos = contactos.Where(s => s.Contacto_tipo.Nombre.Contains(searchString)
                                       || s.Contacto_medio.Nombre.Contains(searchString)
                                       || s.Contacto.Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "Medio":
                    contactos = contactos.OrderBy(p => p.Contacto_medio.Nombre);
                    break;
                case "Medio_desc":
                    contactos = contactos.OrderByDescending(p => p.Contacto_medio.Nombre);
                    break;
                case "Tipo":
                    contactos = contactos.OrderBy(p => p.Contacto_tipo.Nombre);
                    break;
                case "Tipo_desc":
                    contactos = contactos.OrderByDescending(p => p.Contacto_tipo.Nombre);
                    break;
                case "Contacto":
                    contactos = contactos.OrderBy(p => p.Contacto);
                    break;
                case "Contacto_desc":
                    contactos = contactos.OrderByDescending(p => p.Contacto);
                    break;
                default:
                    contactos = contactos.OrderBy(ct => ct.Contacto_tipo.Nombre).OrderBy(cm => cm.Contacto_medio.Nombre).OrderBy(c => c.Contacto);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(contactos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Contactos/Details/5
        public ActionResult Details(int id = 0)
        {
            Contactos contactos = repo.Get(id);
            if (contactos == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Details", contactos);
        }

        // GET: Contactos/Create
        public ActionResult Create()
        {
            ViewBag.Id_ContactoMedio = new SelectList(Repo_ContactoMedio.GetAll(), "Id_ContactoMedio", "Nombre");
            ViewBag.Id_ContactoTipo = new SelectList(Repo_ContactoTipo.GetAll(), "Id_ContactoTipo", "Nombre");

            return PartialView("_Create");
        }

        // POST: Contactos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Contacto,Id_ContactoTipo,Contacto,Id_ContactoMedio,Notas,Estatus")] Contactos contactos)
        {
            if (ModelState.IsValid)
            {
                repo.Add(contactos);
                repo.Save();

                //string url = Url.Action("Index", "Contactos", new { id = contactos.Id_Contacto });
                string url = Url.Action("Index", "Contactos");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_ContactoMedio = new SelectList(Repo_ContactoMedio.GetAll(), "Id_ContactoMedio", "Nombre", contactos.Id_ContactoMedio);
            ViewBag.Id_ContactoTipo = new SelectList(Repo_ContactoTipo.GetAll(), "Id_ContactoTipo", "Nombre", contactos.Id_ContactoTipo);

            return PartialView("_Create", contactos);
        }

        // GET: Contactos/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Contactos contactos = repo.Get(id);
            if (contactos == null || id == 0)
            {
                return RedirectToAction("index");

            }
            ViewBag.Id_ContactoMedio = new SelectList(Repo_ContactoMedio.GetAll(), "Id_ContactoMedio", "Nombre", contactos.Id_ContactoMedio);
            ViewBag.Id_ContactoTipo = new SelectList(Repo_ContactoTipo.GetAll(), "Id_ContactoTipo", "Nombre", contactos.Id_ContactoTipo);

            return PartialView("_Edit", contactos);
        }

        // POST: Contactos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Contacto,Id_ContactoTipo,Contacto,Id_ContactoMedio,Notas,Estatus")] Contactos contactos)
        {
            if (ModelState.IsValid)
            {
                repo.Update(contactos);
                repo.Save();

                //string url = Url.Action("Index", "Contactos", new { id = contactos.Id_Contacto });
                string url = Url.Action("Index", "Contactos");
                return Json(new { success = true, url = url });   
            }
            ViewBag.Id_ContactoMedio = new SelectList(Repo_ContactoMedio.GetAll(), "Id_ContactoMedio", "Nombre", contactos.Id_ContactoMedio);
            ViewBag.Id_ContactoTipo = new SelectList(Repo_ContactoTipo.GetAll(), "Id_ContactoTipo", "Nombre", contactos.Id_ContactoTipo);

            return PartialView("_Edit", contactos);
        }

        // GET: Contactos/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Contactos contactos = repo.Get(id);
            if (contactos == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Delete", contactos);
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contactos contactos = repo.Get(id);
            repo.Delete(contactos);
            repo.Save();

            string url = Url.Action("Index", "Contactos", new { id = contactos.Id_Contacto });
            return Json(new { success = true, url = url });
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
