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

namespace Security.Controllers
{
    public class EntidadesSVCController : ApiController
    {
        private readonly IRepo<Entidades> repo;
        private readonly IRepo<Paises> Repo_Paises;

        public EntidadesSVCController() {

        }

        public EntidadesSVCController(IRepo<Entidades> repo, IRepo<Paises> Repo_Paises)
        {
            this.repo = repo;
            this.Repo_Paises = Repo_Paises;
        }

        // GET: api/EntidadesSVC
        public IQueryable<Entidades> GetEntidades()
        {
            return repo.GetAll();
        }

        // GET: api/EntidadesSVC/5
        [ResponseType(typeof(Entidades))]
        public IHttpActionResult GetEntidades(short id, short id2)
        {
            Entidades entidades = repo.Get(id,id2);
            if (entidades == null)
            {
                return NotFound();
            }

            return Ok(entidades);
        }

        // PUT: api/EntidadesSVC/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntidades(short id, short id2, Entidades entidades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entidades.Id_Pais)
            {
                return BadRequest();
            }

            repo.Update(entidades);
            //db.Entry(entidades).State = EntityState.Modified;

            try
            {
                //db.SaveChanges();
                repo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntidadesExists(id, id2))
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

        // POST: api/EntidadesSVC
        [ResponseType(typeof(Entidades))]
        public IHttpActionResult PostEntidades(Entidades entidades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Entidades.Add(entidades);
            repo.Add(entidades);

            try
            {
                repo.Save();
            }
            catch (DbUpdateException)
            {
                if (EntidadesExists(entidades.Id_Pais, entidades.Id_Entidad))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = entidades.Id_Pais, id2 = entidades.Id_Entidad }, entidades);
        }

        // DELETE: api/EntidadesSVC/5
        [ResponseType(typeof(Entidades))]
        public IHttpActionResult DeleteEntidades(short id, short id2)
        {
            Entidades entidades = repo.Get(id, id2); // db.Entidades.Find(id);
            if (entidades == null)
            {
                return NotFound();
            }

            repo.Delete(entidades);
            repo.Save();

            //db.Entidades.Remove(entidades);
            //db.SaveChanges();

            return Ok(entidades);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                Repo_Paises.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntidadesExists(short id, short id2)
        {
            //return db.Entidades.Count(e => e.Id_Pais == id) > 0;
            //var entidades = repo.GetAll().Where(e => e.Id_Pais == id).Where(f => f.Id_Entidad == id2).Count();
            return repo.GetAll().Where(e => e.Id_Pais == id).Where(f => f.Id_Entidad == id2).Count() > 0;
        }
    }
}