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
using ActorLibrary.Models;

namespace ActorLibrary.Controllers.API
{
    public class ActorsController : ApiController
    {
        private ActorContext db = new ActorContext();

        // GET: api/Actors
        public IQueryable<Actor> GetActors()
        {
            return db.Actors;
        }

        // GET: api/Actors/5
        [ResponseType(typeof(Actor))]
        public IHttpActionResult GetActor(int id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            return Ok(actor);
        }

        // PUT: api/Actors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActor(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actor.ActorId)
            {
                return BadRequest();
            }

            db.Entry(actor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        // POST: api/Actors
        [ResponseType(typeof(Actor))]
        public IHttpActionResult PostActor(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Actors.Add(actor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = actor.ActorId }, actor);
        }

        // DELETE: api/Actors/5
        [ResponseType(typeof(Actor))]
        public IHttpActionResult DeleteActor(int id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            db.Actors.Remove(actor);
            db.SaveChanges();

            return Ok(actor);
        }

        [Route("api/getdialects")]
        [HttpGet]
        public IEnumerable<DialectName> GetDialects()
        {
            var d = db.DialectNames.ToList();
            return db.DialectNames.ToList();
        }

        [Route("api/getonedialect/{id}")]
        [HttpGet]
        public DialectName GetOneDialect(int id)
        {
            var d = db.DialectNames.Find(id);
            return d;
        }

        //[Authorize]
        [Route("api/deletedialects")]
        [HttpPost]
        public string DeleteDialect(DialectName newDialect)
        {
            var id = newDialect.DialectListId;
            try
            {
                var dialect = db.DialectNames.FirstOrDefault(d => d.DialectListId == id);
                db.DialectNames.Remove(dialect);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return id.ToString();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActorExists(int id)
        {
            return db.Actors.Count(e => e.ActorId == id) > 0;
        }
    }
}