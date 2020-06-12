using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ProductosController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Expiration(string dateInit, string dateEnd)
        {
            DateTime dateI = Convert.ToDateTime(dateInit);
            DateTime dateF = Convert.ToDateTime(dateEnd);
            using (inventarioEntities db = new inventarioEntities())
            {

                var product = (from p in db.productos
                              join m in db.detalles_movimientos on p.id_producto equals m.id_producto
                              where m.fecha_vencimiento >= dateI && m.fecha_vencimiento <= dateF
                              select new { Producto = p.nombre }).ToList();

                if (product != null)
                    return Ok(product);
                else
                    return NotFound();

            }
        }
    }
}
