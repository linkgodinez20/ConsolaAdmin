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
        public ActionResult Index()
        {
            return View(repo.GetAll());
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
