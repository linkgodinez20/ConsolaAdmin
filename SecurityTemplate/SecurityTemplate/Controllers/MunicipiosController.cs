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
using Security.Data;
using PagedList;

namespace Security.Controllers
{
    public class MunicipiosController : Controller
    {
        private readonly IRepo<Municipios> repo;
        private readonly IRepo<Paises> Repo_Pais;
        private readonly IRepo<Entidades> Repo_Entidad;

        //private RepoHelper<Municipios> municipio_Key;

        public MunicipiosController(IRepo<Municipios> repo, IRepo<Paises> Repo_Pais, IRepo<Entidades> Repo_Entidad) //, RepoHelper<Municipios> municipio_Key
        {
            this.repo = repo;
            this.Repo_Pais = Repo_Pais;
            this.Repo_Entidad = Repo_Entidad;
            //this.municipio_Key = municipio_Key;
        }

        // GET: Municipios
        public ActionResult Index(Int16? Pais, Int16? Entidad, string sortOrder, string currentFilter, string searchString, int? page)
        {
            if ((null != Pais) && (null != Entidad) && (1 != page))
            {
                Session["Municipios_index_IdPais"] = Convert.ToInt16(Pais);
                Session["Municipios_index_IdEntidad"] = Convert.ToInt16(Entidad);
                Session["Municipios_Page"] = Convert.ToInt32(page);
            }
            
            ViewBag.CurrentSort = sortOrder;            

            ViewBag.SortByMunicipio = sortOrder == "Municipio_desc" ? "Municipio_desc" : "Municipio";
            ViewBag.SortByEntidad = sortOrder == "Entidad_desc" ? "Entidad_desc" : "Entidad";

            if (searchString != null)
            {
                Int32 pagina = Convert.ToInt32(Session["Municipios_Page"]);
                page = pagina;//1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            Int16? Mun_idx_IdPais = Convert.ToInt16(Session["Municipios_index_IdPais"]);
            Int16? Mun_idx_IdEntidad = Convert.ToInt16(Session["Municipios_index_IdEntidad"]);

            IOrderedQueryable<Municipios> ordena_municipios = repo.GetAll().Where(p => p.Id_Pais == Mun_idx_IdPais).Where(e => e.Id_Entidad == Mun_idx_IdEntidad)
                .OrderBy(x => x.Id_Pais).OrderBy(y => y.Id_Entidad).OrderBy(z => z.Id_Municipio);


            //entidadSeleccionada = Convert.ToInt16(Session["EntidadSelect"]);

            var municipios = from s in ordena_municipios                             
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                municipios = municipios.Where(s => s.Entidades.Nombre.Contains(searchString)
                                       || s.Nombre.Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "Municipio":
                    municipios = municipios.OrderBy(p => p.Nombre);
                    break;
                case "Municipio_desc":
                    municipios = municipios.OrderByDescending(p => p.Nombre);
                    break;
                case "Entidad":
                    municipios = municipios.OrderBy(p => p.Entidades.Nombre);
                    break;
                case "Entidad_desc":
                    municipios = municipios.OrderByDescending(p => p.Entidades.Nombre);
                    break;
                
                default:  
                    municipios = municipios.OrderBy(e => e.Nombre);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(municipios.ToPagedList(pageNumber, pageSize));
        }

        // GET: Municipios/Details/5
        public ActionResult Details(short id = 0, short id2 = 0, short id3 = 0)
        {

            Municipios municipios = repo.Get(id,id2,id3);
            if (municipios == null || id == 0 || id2 == 0 || id3 == 0)
            {
                return RedirectToAction("Index");
            }
            return View(municipios);
        }

        // GET: Municipios/Create
        public ActionResult Create()
        {
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

            return View(municipios);
        }

        // GET: Municipios/Edit/5
        public ActionResult Edit(short id = 0, short id2 = 0, short id3 = 0)
        {
            Municipios municipios = repo.Get(id,id2,id3);

            if (municipios == null || id == 0 || id2 == 0 || id3 == 0)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Id_Pais = new SelectList(Repo_Pais.GetAll(), "Id_Pais", "Nombre", municipios.Id_Pais);
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
            ViewBag.Id_Pais = new SelectList(Repo_Pais.GetAll(), "Id_Pais", "Nombre", municipios.Id_Pais);
            ViewBag.Id_Entidad = new SelectList(Repo_Entidad.GetAll(), "Id_Entidad", "Nombre", municipios.Id_Entidad);
            ViewBag.Id_Municipio = new SelectList(repo.GetAll(), "Id_Municipio", "Nombre", municipios.Id_Municipio);

            return View(municipios);
        }

        // GET: Municipios/Delete/5
        public ActionResult Delete(short id = 0, short id2 = 0, short id3 = 0)
        {
            Municipios municipios = repo.Get(id,id2,id3);
            if (municipios == null || id == 0 || id2 == 0 || id3 == 0)
            {
                return RedirectToAction("Index");
            }
            return View(municipios);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id, short id2, short id3)
        {
            Municipios municipios = repo.Get(id,id2,id3);
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
