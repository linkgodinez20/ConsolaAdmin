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
    public class Menu_CategoriaController : Controller
    {
        private readonly IRepo<Menu_categoria> repo;

        public Menu_CategoriaController(IRepo<Menu_categoria> Repo)
        {
            this.repo = Repo;
        }

        // GET: /MenuCategoria/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Categoria = String.IsNullOrEmpty(sortOrder) ? "Categoria_desc" : "";
            ViewBag.Orden = sortOrder == "Orden" ? "Orden" : "Orden_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Menu_categoria> menu_cat = repo.GetAll().OrderBy(x => x.Nombre);

            var modelo = from s in menu_cat select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Nombre.Contains(searchString) || s.Orden.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Categoria":
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
                case "Categoria_desc":
                    modelo = modelo.OrderByDescending(s => s.Nombre);
                    break;
                case "Orden":
                    modelo = modelo.OrderBy(s => s.Orden.ToString());
                    break;
                case "Orden_desc":
                    modelo = modelo.OrderByDescending(s => s.Orden.ToString());
                    break;                
                default:
                    modelo = modelo.OrderBy(s => s.Nombre);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }

        // GET: /MenuCategoria/Details/5
        public ActionResult Details(short id = 0)
        {
            Menu_categoria menu_categoria = repo.Get(id);
            if (menu_categoria == null || id == 0)
            {
                return RedirectToAction("Index");
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
        public ActionResult Edit(short id = 0)
        {
            Menu_categoria menu_categoria = repo.Get(id);
            if (menu_categoria == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return View(menu_categoria);
        }

        // POST: /MenuCategoria/Edit/5
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
        public ActionResult Delete(short id = 0)
        {
            Menu_categoria menu_categoria = repo.Get(id);
            if (menu_categoria == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return View(menu_categoria);
        }

        // POST: /MenuCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            try
            {
                Menu_categoria menu_categoria = repo.Get(id);
                repo.Delete(menu_categoria);
                repo.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Este registro no se puede eliminar por estar referenciado con otro registro.";
                ViewBag.True = 1;
                return View();
            }
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
