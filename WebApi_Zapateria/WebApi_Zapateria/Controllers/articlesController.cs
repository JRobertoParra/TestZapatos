using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi_Zapateria.DbObjects;
using WebApi_Zapateria.Models;

namespace WebApi_Zapateria.Controllers
{
    public class articlesController : ApiController
    {
        private dbZapateriaEntities1 db = new dbZapateriaEntities1();

        // GET: api/articles
        [Route("api/ArticlesCount")]
        public int articlesCount()
        {
            return this.Getarticles().Count();
        }

       
        [Route("services/articles/stores")]
        // GET: api/articles
        public async Task<List<articlesViewModel>> GetarticlesStore(int id)
        {
            //var config = new MapperConfiguration(cfg =>
            //   cfg.CreateMap<stores, articlesViewModel>().ReverseMap());
            //var mapper = config.CreateMapper();


            return (from a in db.articles
                    join b in db.stores on a.store_id equals b.store_id
                    where a.id == id
                    select new articlesViewModel() { Id = a.id, Name = a.name,
                        description = a.description ,price= a.price,total_in_shelf = a.total_in_shelf,
                        total_in_vault =a.total_in_vault, store_id = a.store_id, store_name = b.name
                    }).ToList();

        }

        // GET: api/articles
        public IQueryable<articles> Getarticles()
        {
            return db.articles;
        }

        // GET: api/articles/5
        [ResponseType(typeof(articles))]
        public async Task<IHttpActionResult> Getarticles(int id)
        {
            articles articles = await db.articles.FindAsync(id);
            if (articles == null)
            {
                return NotFound();
            }

            return Ok(articles);
        }

        // PUT: api/articles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putarticles(int id, articles articles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articles.id)
            {
                return BadRequest();
            }

            db.Entry(articles).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!articlesExists(id))
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

        // POST: api/articles
        [ResponseType(typeof(articles))]
        public async Task<IHttpActionResult> Postarticles(articles articles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.articles.Add(articles);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (articlesExists(articles.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = articles.id }, articles);
        }


        // POST: api/articles
        [HttpPost, Route("api/Postarticles")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> PostarticlesId([FromBody] articles articles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = db.articles.Max(i => i.id);
            //Se actualiza el id para tomar el maximo
            articles.id = id + 1;
            db.articles.Add(articles);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (articlesExists(articles.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(true);
        }

        // DELETE: api/articles/5
        [ResponseType(typeof(articles))]
        public async Task<IHttpActionResult> Deletearticles(int id)
        {
            articles articles = await db.articles.FindAsync(id);
            if (articles == null)
            {
                return NotFound();
            }

            db.articles.Remove(articles);
            await db.SaveChangesAsync();

            return Ok(articles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool articlesExists(int id)
        {
            return db.articles.Count(e => e.id == id) > 0;
        }
    }
}