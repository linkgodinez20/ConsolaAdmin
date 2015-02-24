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
    public class EquiposController : Controller
    {
        private readonly IRepo<Equipos> repo;
        private readonly IRepo<Equipos_tipo> Repo_EquiposTipo;
        private readonly IRepo<Cuentas> Repo_Cuentas;
        private readonly IRepo<Sistemas> Repo_Sistemas;
        private readonly IRepo<Dispositivos_tipo> repo_DispositivoTipo;

        private readonly DefaultSettings Config;

        public EquiposController(IRepo<Equipos> repo, IRepo<Equipos_tipo> Repo_EquiposTipo, IRepo<Cuentas> Repo_Cuentas, IRepo<Sistemas> Repo_Sistemas, IRepo<Dispositivos_tipo> repo_DispositivoTipo, DefaultSettings Config)
        {
            this.repo = repo;
            this.Repo_EquiposTipo = Repo_EquiposTipo;
            this.Repo_Cuentas = Repo_Cuentas;
            this.Repo_Sistemas = Repo_Sistemas;
            this.repo_DispositivoTipo = repo_DispositivoTipo;
            this.Config = Config;
        }

        // GET: Equipos
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.SortByHostName = sortOrder == "Hostname_desc" ? "Hostname_desc" : "Hostname";
            ViewBag.SortByMacAddress = sortOrder == "MacAddress_desc" ? "MacAddress_desc" : "MacAddress";
            ViewBag.SortByCuenta = sortOrder == "Cuenta_desc" ? "Cuenta_desc" : "Cuenta";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IOrderedQueryable<Equipos> ordena_equipos = repo.GetAll()
                .OrderBy(x => x.Cuentas.LogIn.Usuario).OrderBy(y => y.Estatus);

            var personas = from s in ordena_equipos
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                personas = personas.Where(s => s.Hostname.Contains(searchString)
                                       || s.MacAddress.Contains(searchString)
                                       || s.Cuentas.LogIn.Usuario.Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "Hostname":
                    personas = personas.OrderBy(p => p.Hostname);
                    break;
                case "Hostname_desc":
                    personas = personas.OrderByDescending(p => p.Hostname);
                    break;
                case "MacAddress":
                    personas = personas.OrderBy(p => p.MacAddress);
                    break;
                case "MacAddress_desc":
                    personas = personas.OrderByDescending(p => p.MacAddress);
                    break;
                case "Cuenta":
                    personas = personas.OrderBy(p => p.Cuentas.LogIn.Usuario);
                    break;
                case "Cuenta_desc":
                    personas = personas.OrderByDescending(p => p.Cuentas.LogIn.Usuario);
                    break;

                default:
                    personas = personas.OrderBy(x => x.Cuentas.LogIn.Usuario).OrderBy(y => y.Estatus);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            //return View(personas.ToPagedList(pageNumber, pageSize));
            return View("Index", "~/Views/Shared/_Layout.cshtml", personas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Equipos/Details/5
        public ActionResult Details(short id = 0)
        {
            Equipos equipos = repo.Get(id);
            if (equipos == null || id == 0)
            {
                return RedirectToAction("index");
            }
            return View(equipos);
        }

        // GET: Equipos/Create
        public ActionResult Create()
        {
            ViewBag.Id_EquipoTipo = new SelectList(Repo_EquiposTipo.GetAll(), "Id_EquipoTipo", "Nombre");
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre");
            ViewBag.Id_DispositivoTipo = new SelectList(repo_DispositivoTipo.GetAll(), "Id_DispositivoTipo", "Nombre");
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Equipo,Hostname,IP,MacAddress,Id_EquipoTipo,Id_DispositivoTipo,Descripcion,Ubiacion,Id_Cuenta,Id_Sistema,Estatus")] Equipos equipos)
        {
            if (ModelState.IsValid)
            {
                equipos.FechaAlta = DateTime.Now;

                // Validar que no haya un equipo principal ya dado de alta

                if (equipos.Equipos_tipo.Nombre == "Principal")
                {
                    equipos.FechaCaducidad = DateTime.Now.AddDays(30); ///// Obtener valor de parámetros
                    
                }                

                repo.Add(equipos);
                repo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id_EquipoTipo = new SelectList(Repo_EquiposTipo.GetAll(), "Id_EquipoTipo", "Nombre", equipos.Id_EquipoTipo);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", equipos.Id_Sistema);
            ViewBag.Id_DispositivoTipo = new SelectList(repo_DispositivoTipo.GetAll(), "Id_DispositivoTipo", "Nombre", equipos.Id_DispositivoTipo);

            //return View(equipos);
            return PartialView(equipos);
        }

        // GET: Equipos/Edit/5
        public ActionResult Edit(short id = 0)
        {
            Equipos equipos = repo.Get(id);
            if (equipos == null || id == 0)
            {
                return HttpNotFound();
            }
            //ViewBag.Id_Cuenta = new SelectList(Repo_Cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta", equipos.Id_Cuenta);
            ViewBag.Id_EquipoTipo = new SelectList(Repo_EquiposTipo.GetAll(), "Id_EquipoTipo", "Nombre", equipos.Id_EquipoTipo);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", equipos.Id_Sistema);
            ViewBag.Id_DispositivoTipo = new SelectList(repo_DispositivoTipo.GetAll(), "Id_DispositivoTipo", "Nombre", equipos.Id_DispositivoTipo);

            return PartialView(equipos);
            //return View(equipos);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Equipo,Hostname,IP,MacAddress,Id_EquipoTipo,Id_DispositivoTipo,Descripcion,Ubiacion,FechaAlta,FechaCaducidad,Id_Cuenta,Id_Sistema,Estatus")] Equipos equipos)
        {
            if (ModelState.IsValid)
            {
                equipos.FechaAlta = equipos.FechaAlta;


                repo.Update(equipos);
                repo.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.Id_Cuenta = new SelectList(Repo_Cuentas.GetAll(), "Id_Cuenta", "Id_Cuenta", equipos.Id_Cuenta);
            ViewBag.Id_EquipoTipo = new SelectList(Repo_EquiposTipo.GetAll(), "Id_EquipoTipo", "Nombre", equipos.Id_EquipoTipo);
            ViewBag.Id_Sistema = new SelectList(Repo_Sistemas.GetAll(), "Id_Sistema", "Nombre", equipos.Id_Sistema);
            ViewBag.Id_DispositivoTipo = new SelectList(repo_DispositivoTipo.GetAll(), "Id_DispositivoTipo", "Nombre", equipos.Id_DispositivoTipo);

            return View(equipos);
        }

        // GET: Equipos/Delete/5
        public ActionResult Delete(short id = 0)
        {
            Equipos equipos = repo.Get(id);
            if (equipos == null)
            {
                return RedirectToAction("index");
            }
            return View(equipos);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Equipos equipos = repo.Get(id);
            repo.Delete(equipos);
            repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                Repo_Cuentas.Dispose();
                repo_DispositivoTipo.Dispose();
                Repo_EquiposTipo.Dispose();
                Repo_Sistemas.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
