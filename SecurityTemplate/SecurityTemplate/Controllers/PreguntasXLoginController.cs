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
    public class PreguntasXLoginController : Controller
    {
        private readonly IRepo<Preguntas_x_Login> repo;
        private readonly IRepo<LogIn> login; 
        private readonly IRepo<Preguntas> preguntas;
        public PreguntasXLoginController(IRepo<Preguntas_x_Login> Repo, IRepo<LogIn> Login, IRepo<Preguntas> Preguntas)
        {
            this.repo = Repo;
            this.login = Login;
            this.preguntas = Preguntas;
        }
        // GET: /PreguntasXLogin/
        //public ActionResult Index()
        //{
        //    var preguntas_x_login = repo.GetAll().Include(p => p.LogIn).Include(p => p.Preguntas);
        //    return View(preguntas_x_login.ToList());
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Respuesta = String.IsNullOrEmpty(sortOrder) ? "Respuesta_desc" : "";
            ViewBag.Usuario = sortOrder == "Usuario" ? "Usuario" : "Usuario_desc";
            ViewBag.Pregunta = sortOrder == "Pregunta" ? "Pregunta" : "Pregunta_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Preguntas_x_Login> pXl = repo.GetAll().OrderBy(x => x.Respuesta);

            var modelo = from s in pXl select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelo = modelo.Where(s => s.Respuesta.Contains(searchString) || s.LogIn.Usuario.Contains(searchString) || s.Preguntas.Pregunta.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Respuesta":
                    modelo = modelo.OrderBy(s => s.Respuesta);
                    break;
                case "Respuesta_desc":
                    modelo = modelo.OrderByDescending(s => s.Respuesta);
                    break;
                case "Usuario":
                    modelo = modelo.OrderBy(s => s.LogIn.Usuario);
                    break;
                case "Usuario_desc":
                    modelo = modelo.OrderByDescending(s => s.LogIn.Usuario);
                    break;
                case "Pregunta":
                    modelo = modelo.OrderBy(s => s.Preguntas.Pregunta);
                    break;
                case "Pregunta_desc":
                    modelo = modelo.OrderByDescending(s => s.Preguntas.Pregunta);
                    break;
                default:
                    modelo = modelo.OrderBy(s => s.Respuesta);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(modelo.ToPagedList(pageNumber, pageSize));
        }

        // GET: /PreguntasXLogin/Details/5
        public ActionResult Details(int id = 0)
        {
            Preguntas_x_Login preguntas_x_login = repo.Get(id);
            if (preguntas_x_login == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return View(preguntas_x_login);
        }

        // GET: /PreguntasXLogin/Create
        public ActionResult Create()
        {
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario");
            ViewBag.Id_Pregunta = new SelectList(preguntas.GetAll(), "Id_Pregunta", "Pregunta");
            return View();
        }

        // POST: /PreguntasXLogin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_Login,Id_Pregunta,Respuesta")] Preguntas_x_Login preguntas_x_login)
        {
            if (ModelState.IsValid)
            {
                repo.Add(preguntas_x_login);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario", preguntas_x_login.Id_Login);
            ViewBag.Id_Pregunta = new SelectList(preguntas.GetAll(), "Id_Pregunta", "Pregunta", preguntas_x_login.Id_Pregunta);
            return View(preguntas_x_login);
        }

        // GET: /PreguntasXLogin/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Preguntas_x_Login preguntas_x_login = repo.Get(id);
            if (preguntas_x_login == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario", preguntas_x_login.Id_Login);
            ViewBag.Id_Pregunta = new SelectList(preguntas.GetAll(), "Id_Pregunta", "Pregunta", preguntas_x_login.Id_Pregunta);
            return View(preguntas_x_login);
        }

        // POST: /PreguntasXLogin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_Login,Id_Pregunta,Respuesta")] Preguntas_x_Login preguntas_x_login)
        {
            if (ModelState.IsValid)
            {
                repo.Update(preguntas_x_login);
                repo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Login = new SelectList(login.GetAll(), "Id_Login", "Usuario", preguntas_x_login.Id_Login);
            ViewBag.Id_Pregunta = new SelectList(preguntas.GetAll(), "Id_Pregunta", "Pregunta", preguntas_x_login.Id_Pregunta);
            return View(preguntas_x_login);
        }

        // GET: /PreguntasXLogin/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Preguntas_x_Login preguntas_x_login = repo.Get(id);
            if (preguntas_x_login == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            return View(preguntas_x_login);
        }

        // POST: /PreguntasXLogin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Preguntas_x_Login preguntas_x_login = repo.Get(id);
                repo.Delete(preguntas_x_login);
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
                login.Dispose();
                preguntas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
