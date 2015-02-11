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

        // PUT: api/Municipios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMunicipios(short id, Municipios municipios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != municipios.Id_Pais)
            {
                return BadRequest();
            }

            db.Entry(municipios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipiosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Municipios
        [ResponseType(typeof(Municipios))]
        public IHttpActionResult PostMunicipios(Municipios municipios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Municipios.Add(municipios);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MunicipiosExists(municipios.Id_Pais))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = municipios.Id_Pais }, municipios);
        }

        // DELETE: api/Municipios/5
        [ResponseType(typeof(Municipios))]
        public IHttpActionResult DeleteMunicipios(short id)
        {
            Municipios municipios = db.Municipios.Find(id);
            if (municipios == null)
            {
                return NotFound();
            }

            db.Municipios.Remove(municipios);
            db.SaveChanges();

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