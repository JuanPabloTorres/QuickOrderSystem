using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using QuickOrderApp.Web.Context;

namespace QuickOrderApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly QOContext _context;

        public OrderProductController(QOContext context)
        {
            _context = context;
        }

        // GET: api/OrderProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts()
        {
            return await _context.OrderProducts.ToListAsync();
        }

        // GET: api/OrderProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProduct>> GetOrderProduct(Guid id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);

            if (orderProduct == null)
            {
                return NotFound();
            }

            return orderProduct;
        }

        // PUT: api/OrderProduct/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutOrderProduct(Guid id, OrderProduct orderProduct)
        {

            var product = _context.OrderProducts.Where(o => o.OrderProductId == orderProduct.OrderProductId).FirstOrDefault();

            if (product != null)
            {
                _context.OrderProducts.Remove(product);

                _context.SaveChanges();

                _context.OrderProducts.Add(orderProduct);

                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

            //if (id != orderProduct.OrderProductId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(orderProduct).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!OrderProductExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/OrderProduct
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OrderProduct>> PostOrderProduct(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderProduct", new { id = orderProduct.OrderProductId }, orderProduct);
        }

        // DELETE: api/OrderProduct/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderProduct>> DeleteOrderProduct(Guid id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return orderProduct;
        }

        [HttpGet("[action]/{id}")]
        public bool OrderProductExists(Guid id)
        {
            return _context.OrderProducts.Any(e => e.OrderProductId == id);
        }

        [HttpGet("[action]/{userid}/{productname}")]
        public bool OrderProductOfUserExist(Guid userid,string productname)
        {
            return _context.OrderProducts.Any(e => e.BuyerId == userid && e.ProductName == productname);
        }

        [HttpGet("[action]/{productname}")]
        public OrderProduct OrderProductOfUserExistWith(string productname)
        {
            return _context.OrderProducts.Where(e => e.ProductName == productname).FirstOrDefault();
        }
    }
}
