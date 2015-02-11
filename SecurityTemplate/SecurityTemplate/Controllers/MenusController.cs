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
    public class MenusController : Controller
    {
        private readonly IRepo<Menu> repo;
        private readonly IRepo<Paginas> Repo_Paginas;
        private readonly IRepo<Menu_categoria> Repo_Categoria;
        private readonly IRepo<Sistemas> Repo_Sistemas;

        public MenusController(IRepo<Menu> repo, IRepo<Paginas> Repo_Paginas, IRepo<Menu_categoria> Repo_Categoria, IRepo<Sistemas> Repo_Sistemas)
        {
            this.repo = repo;
            this.Repo_Paginas = Repo_Paginas;
            this.Repo_Categoria = Repo_Categoria;
            this.Repo_Sistemas = Repo_Sistemas;
        }

        // GET: Menus
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string EntidadSeleccionada)
        {
            // Revisar si se desactiva el eager loading
            //ViewBag.Entidades_ddl = new SelectList(Repo_Entidad.GetAll(), "Id_Entidad", "Nombre");

            ViewBag.CurrentSort = sortOrder;

            ViewBag.SortByParent = sortOrder == "Parent_desc" ? "Parent_desc" : "Parent";
            ViewBag.SortByCategoria = sortOrder == "Categoria_desc" ? "Categoria_desc" : "Categoria";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Menu> ordena_menus = repo.GetAll()
                .OrderBy(x => x.Parent).OrderBy(y => y.Menu_categoria.Nombre);

            var menus = from s in ordena_menus
                             select s;


            if (!String.IsNullOrEmpty(searchString))
            {
                menus = menus.Where(s => s.Menu_categoria.Nombre.Contains(searchString)
                                       || s.Nombre.Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "Parent":
                    menus = menus.OrderBy(p => p.Parent);
                    break;
                case "Parent_desc":
                    menus = menus.OrderByDescending(p => p.Parent);
                    break;
                case "Categoria":
                    menus = menus.OrderBy(p => p.Menu_categoria.Nombre);
                    break;
                case "Categoria_desc":
                    menus = menus.OrderByDescending(p => p.Menu_categoria.Nombre);
                    break;

                default:
                    menus = menus.OrderBy(e => e.Nombre);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(menus.ToPagedList(pageNumber, pageSize));

            //var menu = db.Menu.Include(m => m.Menu_categoria).Include(m => m.Paginas).Include(m => m.Sistemas);
            //return View(menu.ToList());
        }

        // GET: Menus/Details/5
        public ActionResult Details(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = repo.Get(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            ViewBag.Id_MenuCategoria = new SelectList(Repo_Categoria.GetAll(), "Id_MenuCategoria", "Nombre");
            ViewBag.Id_Pagina = new SelectList(Repo_Paginas.GetAll(), "Id_Pagina", "Nombre");
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Menu,Nombre,Parent,Tooltip,Id_Pagina,Orden,Habilitado,Id_MenuCategoria,Id_Sistema")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                repo.Add(menu);
                repo.Save();

                return RedirectToAction("Index");
            }

            ViewBag.Id_MenuCategoria = new SelectList(Repo_Categoria.GetAll(), "Id_MenuCategoria", "Nombre", menu.Id_MenuCategoria);
            ViewBag.Id_Pagina = new SelectList(Repo_Paginas.GetAll(), "Id_Pagina", "Nombre", menu.Id_Pagina);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", menu.Id_Sistema);
            return View(menu);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = repo.Get(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_MenuCategoria = new SelectList(Repo_Categoria.GetAll(), "Id_MenuCategoria", "Nombre", menu.Id_MenuCategoria);
            ViewBag.Id_Pagina = new SelectList(Repo_Paginas.GetAll(), "Id_Pagina", "Nombre", menu.Id_Pagina);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", menu.Id_Sistema);
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Menu,Nombre,Parent,Tooltip,Id_Pagina,Orden,Habilitado,Id_MenuCategoria,Id_Sistema")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                repo.Update(menu);
                repo.Save();

                return RedirectToAction("Index");
            }
            ViewBag.Id_MenuCategoria = new SelectList(Repo_Categoria.GetAll(), "Id_MenuCategoria", "Nombre", menu.Id_MenuCategoria);
            ViewBag.Id_Pagina = new SelectList(Repo_Paginas.GetAll(), "Id_Pagina", "Nombre", menu.Id_Pagina);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", menu.Id_Sistema);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(short id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = repo.Get(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Menu menu = repo.Get(id);
            repo.Delete(menu);
            repo.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                Repo_Categoria.Dispose();
                Repo_Paginas.Dispose();
                Repo_Sistemas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
