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
    public class SistemasController : ApiController
    {
        private SecurityEntities db = new SecurityEntities();

        // GET: api/Sistemas
        public IQueryable<Sistemas> GetSistemas()
        {
            return db.Sistemas;
        }

        // GET: api/Sistemas/5
        [ResponseType(typeof(Sistemas))]
        public IHttpActionResult GetSistemas(byte id)
        {
            Sistemas sistemas = db.Sistemas.Find(id);
            if (sistemas == null)
            {
                return NotFound();
            }

            return Ok(sistemas);
        }



        public IQueryable GetNombreSistema([FromUri] string idSistema = "")
        {
            var cuentas = from p in db.Sistemas
                          select new
                          {
                              Nombre = p.Nombre,
                              Id_Sistema = p.Id_Sistema
                          };

            if (idSistema != "")
            {
                Byte id_sistema = Convert.ToByte(idSistema);

                cuentas.Where(p => p.Id_Sistema == id_sistema);
            }

            return cuentas;
        }


        // PUT: api/Sistemas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSistemas(byte id, Sistemas sistemas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sistemas.Id_Sistema)
            {
                return BadRequest();
            }

            db.Entry(sistemas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SistemasExists(id))
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

        // POST: api/Sistemas
        [ResponseType(typeof(Sistemas))]
        public IHttpActionResult PostSistemas(Sistemas sistemas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sistemas.Add(sistemas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sistemas.Id_Sistema }, sistemas);
        }

        // DELETE: api/Sistemas/5
        [ResponseType(typeof(Sistemas))]
        public IHttpActionResult DeleteSistemas(byte id)
        {
            Sistemas sistemas = db.Sistemas.Find(id);
            if (sistemas == null)
            {
                return NotFound();
            }

            db.Sistemas.Remove(sistemas);
            db.SaveChanges();

            return Ok(sistemas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SistemasExists(byte id)
        {
            return db.Sistemas.Count(e => e.Id_Sistema == id) > 0;
        }
    }
}