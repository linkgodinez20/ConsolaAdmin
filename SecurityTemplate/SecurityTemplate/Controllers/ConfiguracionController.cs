using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Security.Core.Model;
using Security.Core.Repository;

namespace Security.Controllers
{
    public class ConfiguracionController : Controller
    {
        private readonly IRepo<Personas> repo_personas;

        public ConfiguracionController(IRepo<Personas> repo_personas)
        {
            this.repo_personas = repo_personas;
        }
        // GET: Configuracion
        public ActionResult Index()
        {
            return View();
        }

        //public CrearCuenta(){
            
        //}
    }
}