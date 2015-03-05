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
using PagedList;
using System.IO;

using Security.Data;
using System.Diagnostics;


namespace Security.Controllers
{
    public class PersonasController : Controller
    {
        private readonly IRepo<Personas> repo;
        private readonly IRepo<Genero> genero;
        private readonly DefaultSettings config;

        public PersonasController(IRepo<Personas> repo, IRepo<Genero> genero, DefaultSettings config)
        {
            this.repo = repo;
            this.genero = genero;
            this.config = config;
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

            return View(personas.ToPagedList(pageNumber, pageSize));

        }

        // GET: Personas/Details/5
        public ActionResult Details(int id = 0)
        {
            Personas personas = repo.Get(id);
            if (personas == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Details", personas);
        }


        // GET: Personas/Create        
        public ActionResult Create()
        {            
            ViewBag.Id_Genero = new SelectList(genero.GetAll(), "Id_Genero", "Nombre");
            return PartialView("_Create");
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Persona,APaterno,AMaterno,Nombre,FechaCreacion,Email,Id_Genero,FechaNacimiento,RFC,Homoclave,CURP,Foto,Estatus")] Personas personas, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                personas.FechaCreacion = DateTime.Now;

                if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                {
                    String OriginalfileName = Path.GetFileName(file.FileName);

                    String sis_id = config.Sistema_iD.ToString("00");
                    String middle = sis_id + "-" + personas.Id_Persona.ToString("000000");
                    String FileExtension = System.IO.Path.GetExtension(file.FileName);

                    String filename = middle + FileExtension;

                    personas.Foto = filename;

                    var path = Path.Combine(Server.MapPath(config.Directorio_PersonasFoto), filename);

                    try
                    {
                        file.SaveAs(path);
                        repo.Add(personas);
                        repo.Save();
                    }
                    catch (Exception e) { 
                        
                    }

                }

                //repo.Add(personas);
                //repo.Save();

                string url = Url.Action("Index", "Personas");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_Genero = new SelectList(genero.GetAll(), "Id_Genero", "Nombre", personas.Id_Genero);
            return PartialView("_Create", personas);
        }

        // GET: Personas/Edit/5
        //[ChildActionOnly]
        public ActionResult Edit(int id = 0)
        {
            Personas personas = repo.Get(id);
            if (personas == null || id == 0)
            {
                return RedirectToAction("index");
            }

            ViewBag.Id_Genero = new SelectList(genero.GetAll(), "Id_Genero", "Nombre", personas.Id_Genero);
            ViewBag.Dir_PersonasFoto = config.Directorio_PersonasFoto;
            ViewBag.foto = personas.Foto;

            return PartialView("_Edit", personas);
        }

        // POST: Personas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Persona,APaterno,AMaterno,Nombre,FechaCreacion,Email,Id_Genero,FechaNacimiento,RFC,Homoclave,CURP,Foto,Estatus")] Personas personas, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                personas.FechaCreacion = DateTime.Now;

                //HttpPostedFileBase file = (HttpPostedFileBase)Session["file"];

                if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                {
                    String OriginalfileName = Path.GetFileName(file.FileName);

                    String sis_id = config.Sistema_iD.ToString("00");
                    String middle = sis_id + "-" + personas.Id_Persona.ToString("000000") + "-" + personas.APaterno;
                    String FileExtension = System.IO.Path.GetExtension(file.FileName);

                    String filename = middle + FileExtension;

                    personas.Foto = filename;

                    var path = Path.Combine(Server.MapPath(config.Directorio_PersonasFoto), filename);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    file.SaveAs(path);
                    Trace.Write("Guardado:" + path);

                }
                else {
                    ModelState.AddModelError("", "No ha seleccionado archivos");
                }

                repo.Update(personas);
                repo.Save();

                string url = Url.Action("Index", "Personas");
                return Json(new { success = true, url = url });
            }

            ViewBag.Id_Genero = new SelectList(genero.GetAll(), "Id_Genero", "Nombre", personas.Id_Genero);
            //return View(personas);
            return PartialView("_Edit", personas);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Personas personas = repo.Get(id);
            if (personas == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return PartialView("_Delete", personas);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personas personas = repo.Get(id);
            repo.Delete(personas);
            repo.Save();

            string url = Url.Action("Index", "Personas", new { id = personas.Id_Persona });
            return Json(new { success = true, url = url });
        }
        //, String APaterno, String Nombre, Int32 IdPersona
        [HttpPost]
        public JsonResult getFile(HttpPostedFileBase file) {

            if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
            {
                //FileModel fm = new FileModel();
                
                //String OriginalfileName = Path.GetFileName(file.FileName);
                //String sis_id = config.Sistema_iD.ToString("00");
                ////String middle = sis_id + "-" + IdPersona.ToString("000000") + "-" + Nombre + "-" + APaterno;

                //fm.extension = System.IO.Path.GetExtension(file.FileName);
                ////fm.filename = middle + fm.extension;
                //fm.fullpath = Path.Combine(Server.MapPath(config.Directorio_PersonasFoto), fm.filename);

                //Session["file"] = fm;
                ////if (System.IO.File.Exists(path))
                ////{
                ////    System.IO.File.Delete(path);
                ////}

                ////file.SaveAs(path);
                Session["file"] = file;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
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
