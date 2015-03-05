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
    public class DomiciliosController : Controller
    {
        private readonly IRepo<Domicilios> repo;
        private readonly IRepo<Contacto_tipo> Repo_Contacto_tipo;
        private readonly IRepo<Paises> Repo_Paises;
        private readonly IRepo<Entidades> Repo_Entidades;
        private readonly IRepo<Municipios> Repo_Municipios;

        public DomiciliosController(IRepo<Domicilios> repo, IRepo<Contacto_tipo> Repo_Contacto_tipo, IRepo<Paises> Repo_Paises, IRepo<Entidades> Repo_Entidades, IRepo<Municipios> Repo_Municipios)
        {
            this.repo = repo;
            this.Repo_Contacto_tipo = Repo_Contacto_tipo;
            this.Repo_Paises = Repo_Paises;
            this.Repo_Entidades = Repo_Entidades;
            this.Repo_Municipios = Repo_Municipios;
        }

        // GET: Domicilios
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SortByMunicipio = sortOrder == "Municipio_desc" ? "Municipio_desc" : "Municipio";
            ViewBag.SortByColonia = sortOrder == "Colonia_desc" ? "Colonia_desc" : "";
            ViewBag.SortByTipo = sortOrder == "Tipo_desc" ? "Tipo_desc" : "Tipo";
            ViewBag.SortByFechaModificacion = sortOrder == "FechaModificacion_desc" ? "FechaModificacion_desc" : "";            

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Domicilios> ordena_domicilios = repo.GetAll()
                .OrderBy(x => x.Municipios.Nombre).OrderBy(y => y.Colonia);

            var domicilios = from s in ordena_domicilios
                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                domicilios = domicilios.Where(d => d.Municipios.Nombre.Contains(searchString)
                                       || d.Colonia.Contains(searchString)
                                       || d.Contacto_tipo.Nombre.Contains(searchString)
                                       || d.Domicilio.Contains(searchString)
                                       || d.FechaModificacion.Value.ToString().Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "Municipio_desc":
                    domicilios = domicilios.OrderByDescending(p => p.Municipios.Nombre);
                    break;
                case "Colonia":
                    domicilios = domicilios.OrderBy(p => p.Colonia);
                    break;
                case "Colonia_desc":
                    domicilios = domicilios.OrderByDescending(p => p.Colonia);
                    break;
                case "Tipo":
                    domicilios = domicilios.OrderBy(p => p.Contacto_tipo.Nombre);
                    break;
                case "Tipo_desc":
                    domicilios = domicilios.OrderByDescending(p => p.Contacto_tipo.Nombre);
                    break;
                default:
                    domicilios = domicilios.OrderBy(p => p.Municipios.Nombre).OrderBy(c => c.Colonia);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            //domicilios.Include(p => p.Municipios);

            return View(domicilios.ToPagedList(pageNumber, pageSize));

        }

        // GET: Domicilios/Details/5
        public ActionResult Details(int id = 0)
        {
            Domicilios domicilios = repo.Get(id);
            if (domicilios == null || id == 0)
            {
                //return HttpNotFound();
                return RedirectToAction("index");
            }
            return PartialView("_Details", domicilios);
        }

        // GET: Domicilios/Create
        public ActionResult Create()
        {
            ViewBag.Id_ContactoTipo = new SelectList(Repo_Contacto_tipo.GetAll(), "Id_ContactoTipo", "Nombre");

            return PartialView("_Create");
        }

        // POST: Domicilios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Domicilio,Id_ContactoTipo,Domicilio,NumeroExterior,NumeroInterior,Id_Pais,Id_Entidad,Id_Municipio,Colonia,EntreCalle,YCalle,Notas,FechaModificacion,Estatus")] Domicilios domicilios)
        {
            if (ModelState.IsValid)
            {
                domicilios.FechaModificacion = DateTime.Now;

                repo.Add(domicilios);
                repo.Save();

                string url = Url.Action("Index", "Domicilios");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_ContactoTipo = new SelectList(Repo_Contacto_tipo.GetAll(), "Id_ContactoTipo", "Nombre", domicilios.Id_ContactoTipo);
            ViewBag.Id_Pais = new SelectList(Repo_Municipios.GetAll(), "Id_Pais", "Nombre", domicilios.Id_Pais);

            return PartialView("_Create", domicilios);
        }

        // GET: Domicilios/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Domicilios domicilios = repo.Get(id);
            if (domicilios == null || id == 0)
            {
                return RedirectToAction("index");
            }

            //var getEntidad = from e in Repo_Entidades.GetAll()
            //                 where e.Id_Pais == domicilios.Id_Pais
            //                 select e;

            //var getMunicipio = from m in Repo_Municipios.GetAll()
            //                   where m.Id_Entidad == domicilios.Id_Entidad && m.Id_Pais == domicilios.Id_Pais
            //                   select m;

            ViewBag.Id_ContactoTipo = new SelectList(Repo_Contacto_tipo.GetAll(), "Id_ContactoTipo", "Nombre", domicilios.Id_ContactoTipo);

            ViewBag.Id_Pais = new SelectList(Repo_Paises.GetAll(), "Id_Pais", "Nombre", domicilios.Id_Pais);
            //ViewBag.Id_Entidad = new SelectList(getEntidad, "Id_Entidad", "Nombre", domicilios.Id_Entidad);
            //ViewBag.Id_Municipio = new SelectList(getMunicipio, "Id_Municipio", "Nombre", domicilios.Id_Municipio);

            return PartialView("_Edit", domicilios);
        }

        // POST: Domicilios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Domicilio,Id_ContactoTipo,Domicilio,NumeroExterior,NumeroInterior,Id_Pais,Id_Entidad,Id_Municipio,Colonia,EntreCalle,YCalle,Notas,FechaModificacion,Estatus")] Domicilios domicilios)
        {
            //var getEntidad = from e in Repo_Entidades.GetAll()
            //                 where e.Id_Pais == domicilios.Id_Pais
            //                 select e;

            //var getMunicipio = from m in Repo_Municipios.GetAll()
            //                   where m.Id_Entidad == domicilios.Id_Entidad && m.Id_Pais == domicilios.Id_Pais
            //                   select m;

            ViewBag.Id_ContactoTipo = new SelectList(Repo_Contacto_tipo.GetAll(), "Id_ContactoTipo", "Nombre", domicilios.Id_ContactoTipo);

            ViewBag.Id_Pais = new SelectList(Repo_Paises.GetAll(), "Id_Pais", "Nombre", domicilios.Id_Pais);
            //ViewBag.Id_Entidad = new SelectList(getEntidad, "Id_Entidad", "Nombre", domicilios.Id_Entidad);
            //ViewBag.Id_Municipio = new SelectList(getMunicipio, "Id_Municipio", "Nombre", domicilios.Id_Municipio);

            if (ModelState.IsValid)
            {
                domicilios.FechaModificacion = DateTime.Now;
                repo.Update(domicilios);
                repo.Save();

                string url = Url.Action("Index", "Domicilios");
                return Json(new { success = true, url = url });
            }

            return PartialView("_Edit", domicilios);
        }

        // GET: Domicilios/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Domicilios domicilios = repo.Get(id);
            if (domicilios == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Delete", domicilios);
        }

        // POST: Domicilios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Domicilios domicilios = repo.Get(id);
            repo.Delete(domicilios);
            repo.Save();

            string url = Url.Action("Index", "Domicilios", new { id = domicilios.Id_Domicilio });
            return Json(new { success = true, url = url });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                Repo_Contacto_tipo.Dispose();
                Repo_Paises.Dispose();
                Repo_Entidades.Dispose();
                Repo_Municipios.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
