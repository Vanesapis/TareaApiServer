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
using APITarea1.Models;

namespace APITarea1.Controllers
{
    public class ProductosController : ApiController
    {
        private Tarea1Entities db = new Tarea1Entities();

        // GET: api/Productos
        public IQueryable<Producto> GetProducto()
        {
            return db.Producto;
        }
        // POST: api/CategoriaProductos
        [ResponseType(typeof(CategoriaProductos))]
        public IHttpActionResult PostaProducto(Producto Producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Producto.Add(Producto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductosExists(Producto.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = Producto.Id }, Producto);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductosExists(int id)
        {
            return db.CategoriaProductos.Count(e => e.Id == id) > 0;
        }

    }

}