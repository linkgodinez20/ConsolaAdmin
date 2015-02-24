using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Security.Core.Model;

namespace Security.Controllers.svc
{
    public class MunicipiosController : ApiController
    {
        private SecurityEntities db = new SecurityEntities();

        public MunicipiosController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Municipios
        // GET: api/Municipios?IdEntidad=14
        public IQueryable GetMunicipios([FromUri]string IdEntidad)
        {
            Int16 idEntidad = Convert.ToInt16(IdEntidad);

            var municipios = from mun in db.Municipios
                             where mun.Id_Entidad == idEntidad
                            select new
                            {
                                Id_Municipio = mun.Id_Municipio,
                                Nombre = mun.Nombre
                            };

            return municipios;
        }

        // GET: api/Municipios/CountMunicipio?IdEntidad=14
        [HttpGet]
        public int CountMunicipio([FromUri]string IdEntidad)
        {
            Int16 idEntidad = Convert.ToInt16(IdEntidad);

            Int32 Contador = (from e in db.Municipios
                              where e.Id_Entidad == idEntidad
                       select e).Count();

            return (Contador + 1);
        }

        // GET: api/Municipios/5
        [ResponseType(typeof(Municipios))]
        public IHttpActionResult GetMunicipios(short id)
        {
            Municipios municipios = db.Municipios.Find(id);
            if (municipios == null)
            {
                return NotFound();
            }

            return Ok(municipios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MunicipiosExists(short id)
        {
            return db.Municipios.Count(e => e.Id_Pais == id) > 0;
        }
    }
}