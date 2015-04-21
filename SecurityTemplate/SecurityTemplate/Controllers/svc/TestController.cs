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

using Security.Core.Repository;
using Security.Core.Services;

namespace Security.Controllers.svc
{
    public class TestController : ApiController
    {
        //private SecurityEntities db = new SecurityEntities();
        private readonly IRepo<Entidades> repo_entidades;

        public TestController(IRepo<Entidades> repo_entidades)
        {
            this.repo_entidades = repo_entidades;

            repo_entidades.ProxyCreationEnabled(false);
        }

        // GET: api/Test
        public IList<EntidadesViewModel> GetEntidades()
        {
            var x = from ent in repo_entidades.GetAll()
                     select new { 
                        Id_Entidad = ent.Id_Entidad,
                        Nombre = ent.Nombre
                     };

            List<EntidadesViewModel> lista = new List<EntidadesViewModel>();
            
            foreach (var item in x)
            {
                EntidadesViewModel e = new EntidadesViewModel();

                e.Id_Entidad = item.Id_Entidad;
                e.Nombre = item.Nombre;

                lista.Add(e);
            }

            return lista;
        }

        // GET: api/Test/5
        [ResponseType(typeof(Entidades))]
        public IHttpActionResult GetEntidades(short id)
        {
            Entidades entidades = repo_entidades.Get(id);
            if (entidades == null)
            {
                return NotFound();
            }

            return Ok(entidades);
        }

        // PUT: api/Test/5
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

            repo_entidades.Update(entidades);

            try
            {
                repo_entidades.Save();
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

        // POST: api/Test
        [ResponseType(typeof(Entidades))]
        public IHttpActionResult PostEntidades(Entidades entidades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            repo_entidades.Add(entidades);
            

            try
            {
                repo_entidades.Save();
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

        // DELETE: api/Test/5
        [ResponseType(typeof(Entidades))]
        public IHttpActionResult DeleteEntidades(short id)
        {
            Entidades entidades = repo_entidades.Get(id);
            if (entidades == null)
            {
                return NotFound();
            }

            repo_entidades.Delete(entidades);
            repo_entidades.Save();

            return Ok(entidades);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo_entidades.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntidadesExists(short id)
        {
            //int x = (from re in repo_entidades.GetAll()
            //        where re.Id_Pais == id
            //        select re).Count();

            //if (x > 0) {
            //    return true;
            //}
            //else {
            //    return false;
            //}
            //return db.Entidades.Count(e => e.Id_Pais == id) > 0;
            return repo_entidades.Count(e => e.Id_Pais == id) > 0;
            
        }
    }
}