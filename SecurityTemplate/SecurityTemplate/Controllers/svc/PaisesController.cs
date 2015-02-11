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
    public class PaisesController : ApiController
    {
        private SecurityEntities db = new SecurityEntities();

        public PaisesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Paises
        public IQueryable GetPaises()
        {
            var paises = (from p in db.Paises
                         select new { 
                            Id_Pais = p.Id_Pais,
                            Nombre = p.Nombre
                         });


            return paises;
        }

        // GET: api/Paises/5
        [ResponseType(typeof(Paises))]
        public IHttpActionResult GetPaises(short id)
        {
            Paises paises = db.Paises.Find(id);
            if (paises == null)
            {
                return NotFound();
            }

            return Ok(paises);
        }

        //// PUT: api/Paises/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutPaises(short id, Paises paises)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != paises.Id_Pais)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(paises).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PaisesExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Paises
        //[ResponseType(typeof(Paises))]
        //public IHttpActionResult PostPaises(Paises paises)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Paises.Add(paises);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (PaisesExists(paises.Id_Pais))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = paises.Id_Pais }, paises);
        //}

        //// DELETE: api/Paises/5
        //[ResponseType(typeof(Paises))]
        //public IHttpActionResult DeletePaises(short id)
        //{
        //    Paises paises = db.Paises.Find(id);
        //    if (paises == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Paises.Remove(paises);
        //    db.SaveChanges();

        //    return Ok(paises);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaisesExists(short id)
        {
            return db.Paises.Count(e => e.Id_Pais == id) > 0;
        }
    }
}