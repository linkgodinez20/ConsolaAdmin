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
    public class CuentasController : ApiController
    {
        private SecurityEntities db = new SecurityEntities();

        public CuentasController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Cuentas
        public IQueryable GetCuentas([FromUri] string idCuenta = "")
        {
            var cuentas = from p in db.Cuentas
                           select new {
                                Cuenta = p.LogIn.Usuario,
                                Id_Cuenta = p.Id_Cuenta
                           };

            if (idCuenta != "") { 
                int id_cuenta = Convert.ToInt32(idCuenta);

                cuentas.Where(p => p.Id_Cuenta == id_cuenta);
            }

            return cuentas;
        }

        [HttpGet]
        public IQueryable getPropietario_x_Cuenta()
        {
            var propietario = from c in db.Cuentas
                              select new
                              {
                                  Id_Cuenta = c.Id_Cuenta,
                                  Propietario = c.LogIn.Personas.Nombre + " " + c.LogIn.Personas.APaterno + " " + c.LogIn.Personas.AMaterno
                              };



            return propietario;
        }

        // GET: api/Cuentas/5
        [ResponseType(typeof(Cuentas))]
        public IHttpActionResult GetCuentas(int id)
        {
            Cuentas cuentas = db.Cuentas.Find(id);
            if (cuentas == null)
            {
                return NotFound();
            }

            return Ok(cuentas);
        }

        // PUT: api/Cuentas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCuentas(int id, Cuentas cuentas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuentas.Id_Cuenta)
            {
                return BadRequest();
            }

            db.Entry(cuentas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentasExists(id))
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

        // POST: api/Cuentas
        [ResponseType(typeof(Cuentas))]
        public IHttpActionResult PostCuentas(Cuentas cuentas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cuentas.Add(cuentas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cuentas.Id_Cuenta }, cuentas);
        }

        // DELETE: api/Cuentas/5
        [ResponseType(typeof(Cuentas))]
        public IHttpActionResult DeleteCuentas(int id)
        {
            Cuentas cuentas = db.Cuentas.Find(id);
            if (cuentas == null)
            {
                return NotFound();
            }

            db.Cuentas.Remove(cuentas);
            db.SaveChanges();

            return Ok(cuentas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuentasExists(int id)
        {
            return db.Cuentas.Count(e => e.Id_Cuenta == id) > 0;
        }
    }
}