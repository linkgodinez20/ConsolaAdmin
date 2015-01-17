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
    public class LocationsController : Controller
    {        
        private readonly IRepo<Locations> repo;
        public LocationsController(IRepo<Locations> Repo)
        {

            this.repo = Repo;
        }

        // GET: /Locations/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Ubicaciones = String.IsNullOrEmpty(sortOrder) ? "Ubicaciones" : "Ubicaciones_desc";
            ViewBag.Latitude = sortOrder == "Latitud" ? "Latitud" : "Latitud_desc";
            ViewBag.Longitud = sortOrder == "Longitud" ? "Longitud" : "Longitud_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Locations> ubicaciones = repo.GetAll()
                .OrderBy(x => x.LocationName);

            var locations = from s in ubicaciones
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(s => s.LocationName.Contains(searchString)
                                       || s.LocationName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Ubicaciones":
                    locations = locations.OrderBy(s => s.LocationName);
                    break;
                case "Ubicaciones_desc":
                    locations = locations.OrderByDescending(s => s.LocationName);
                    break;
                case "Latitud":
                    locations = locations.OrderBy(s => s.Latitude);
                    break;
                case "Latitud_desc":
                    locations = locations.OrderByDescending(s => s.Latitude);
                    break;
                case "Longitud":
                    locations = locations.OrderBy(s => s.Longitude);
                    break;
                case "Longitud_desc":
                    locations = locations.OrderByDescending(s => s.Longitude);
                    break; 
                default:  // Nombre ascendente
                    locations = locations.OrderBy(s => s.LocationName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(locations.ToPagedList(pageNumber, pageSize));                      
        }

        // GET: /Locations/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locations locations = repo.Get(id);
            if (locations == null)
            {
                return HttpNotFound();
            }
            return View(locations);
        }

        // GET: /Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="LocationId,LocationName,Latitude,Longitude")] Locations locations)
        {
            if (ModelState.IsValid)
            {
                repo.Add(locations);
                repo.Save();
                return RedirectToAction("Index");
            }

            return View(locations);
        }

        // GET: /Locations/Edit/5
        public ActionResult Edit(int id)
        {
            Locations locations = repo.Get(id);
            if (locations == null)
            {
                return HttpNotFound();
            }
            return View(locations);
        }

        // POST: /Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="LocationId,LocationName,Latitude,Longitude")] Locations locations)
        {
            if (ModelState.IsValid)
            {
                repo.Update(locations);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(locations);
        }

        // GET: /Locations/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locations locations = repo.Get(id);
            if (locations == null)
            {
                return HttpNotFound();
            }
            return View(locations);
        }

        // POST: /Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Locations locations = repo.Get(id);
            repo.Delete(locations);
            repo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string Location)
        {
            Locations modelos = new Locations();
            var result = repo.GetAll().Where(x => x.LocationName.StartsWith(Location)).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
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
