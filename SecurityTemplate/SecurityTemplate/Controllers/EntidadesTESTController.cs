using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Security.Core.Model;

namespace Security.Controllers
{
    public class EntidadesTESTController : ApiController
    {
        private SecurityEntities db = new SecurityEntities();

        public EntidadesTESTController() {

            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/EntidadesTEST
        public IQueryable<Entidades> GetEntidades()
        {
            return db.Entidades;
        }

        // GET: api/EntidadesTEST/5
        [ResponseType(typeof(Entidades))]
        public IHttpActionResult GetEntidades(short id)
        {
            Entidades entidades = db.Entidades.Find(id);
            if (entidades == null)
            {
                return NotFound();
            }

            return Ok(entidades);
        }

        // PUT: api/EntidadesTEST/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntidades(short id, Entidades entidades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entidades.Id_Pais)
            {
                return BadRequest();
            }

            db.Entry(entidades).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntidadesExists(id))
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

        // POST: api/EntidadesTEST
        [ResponseType(typeof(Entidades))]
        public IHttpActionResult PostEntidades(Entidades entidades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entidades.Add(entidades);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EntidadesExists(entidades.Id_Pais))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = entidades.Id_Pais }, entidades);
        }

        // DELETE: api/EntidadesTEST/5
        [ResponseType(typeof(Entidades))]
        public IHttpActionResult DeleteEntidades(short id)
        {
            Entidades entidades = db.Entidades.Find(id);
            if (entidades == null)
            {
                return NotFound();
            }

            db.Entidades.Remove(entidades);
            db.SaveChanges();

            return Ok(entidades);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntidadesExists(short id)
        {
            return db.Entidades.Count(e => e.Id_Pais == id) > 0;
        }
    }
}