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

namespace Security.Controllers.svc
{
    public class GenerosController : ApiController
    {
        private SecurityEntities db = new SecurityEntities();

        public GenerosController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Generos
        public IQueryable<Genero> GetGenero()
        {
            return db.Genero;
        }

        // GET: api/Generos/5
        [ResponseType(typeof(Genero))]
        public IHttpActionResult GetGenero(byte id)
        {
            Genero genero = db.Genero.Find(id);
            if (genero == null)
            {
                return NotFound();
            }

            return Ok(genero);
        }

        // PUT: api/Generos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGenero(byte id, Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genero.Id_Genero)
            {
                return BadRequest();
            }

            db.Entry(genero).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
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

        // POST: api/Generos
        [ResponseType(typeof(Genero))]
        public IHttpActionResult PostGenero(Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Genero.Add(genero);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = genero.Id_Genero }, genero);
        }

        // DELETE: api/Generos/5
        [ResponseType(typeof(Genero))]
        public IHttpActionResult DeleteGenero(byte id)
        {
            Genero genero = db.Genero.Find(id);
            if (genero == null)
            {
                return NotFound();
            }

            db.Genero.Remove(genero);
            db.SaveChanges();

            return Ok(genero);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GeneroExists(byte id)
        {
            return db.Genero.Count(e => e.Id_Genero == id) > 0;
        }
    }
}