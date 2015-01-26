using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Security.Core.Model;
using Security.Core.Repository;

namespace Security.Controllers
{
    public class CatalogosController : ApiController
    {
        private readonly IRepo<Paises> Repo_Paises;
        private readonly IRepo<Entidades> Repo_Entidades;
        private readonly IRepo<Municipios> Repo_Municipios;

        public CatalogosController(IRepo<Paises> Repo_Paises, IRepo<Entidades> Repo_Entidades, IRepo<Municipios> Repo_Municipios)
        {
            this.Repo_Paises = Repo_Paises;
            this.Repo_Entidades = Repo_Entidades;
            this.Repo_Municipios = Repo_Municipios;
        }
        public IHttpActionResult GetEntidades(String id)
        {
            List<SelectListItem> entidades = new List<SelectListItem>();

            var entities = from e in Repo_Entidades.GetAll()
                           where e.Id_Entidad == Convert.ToInt16(id)
                           select e;

            foreach (var p in entities)
            {
                entidades.Add(new SelectListItem { Text = p.Nombre, Value = Convert.ToString(p.Id_Entidad) });
            }

            
            return Ok(new SelectList(entidades, "Value", "Text"));
        }

        public IHttpActionResult GetMunicipios(String id)
        {
            List<SelectListItem> municipios = new List<SelectListItem>();

            var mun = from e in Repo_Municipios.GetAll()
                      where e.Id_Municipio == Convert.ToInt16(id)
                      select e;

            foreach (var p in mun)
            {
                municipios.Add(new SelectListItem { Text = p.Nombre, Value = Convert.ToString(p.Id_Municipio) });
            }

            return Ok(new SelectList(municipios, "Value", "Text"));
        }
    }
}
