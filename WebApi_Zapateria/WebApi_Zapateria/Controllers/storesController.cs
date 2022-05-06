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
using AutoMapper;
using WebApi_Zapateria.Models;

namespace WebApi_Zapateria.Controllers
{
    public class storesController : ApiController
    {
        private dbZapateriaEntities1 db = new dbZapateriaEntities1();

        [Route("services/stores")]
        [ResponseType(typeof(List<StoreViewModel>))]
        // GET: api/stores
        public async Task<List<StoreViewModel>> Getstores()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<stores, StoreViewModel>().ReverseMap());
            var mapper = config.CreateMapper();

            //var Automapper = new Mapper(config);
            var sto = db.stores.ToList();
            var empDTO = mapper.Map<List<StoreViewModel>>(sto);

            return empDTO;
        }

        // GET: api/stores/5
        [ResponseType(typeof(stores))]
        public async Task<IHttpActionResult> Getstores(int id)
        {
            stores stores = await db.stores.FindAsync(id);
            if (stores == null)
            {
                return NotFound();
            }

            return Ok(stores);
        }

        // PUT: api/stores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putstores(int id, stores stores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stores.store_id)
            {
                return BadRequest();
            }

            db.Entry(stores).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!storesExists(id))
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

        // POST: api/stores
        [ResponseType(typeof(stores))]
        public async Task<IHttpActionResult> Poststores(stores stores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.stores.Add(stores);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (storesExists(stores.store_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = stores.store_id }, stores);
        }

        // DELETE: api/stores/5
        [ResponseType(typeof(stores))]
        public async Task<IHttpActionResult> Deletestores(int id)
        {
            stores stores = await db.stores.FindAsync(id);
            if (stores == null)
            {
                return NotFound();
            }

            db.stores.Remove(stores);
            await db.SaveChangesAsync();

            return Ok(stores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool storesExists(int id)
        {
            return db.stores.Count(e => e.store_id == id) > 0;
        }
    }
}