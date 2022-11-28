using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiQuickOrder.Context;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly QOContext _context;

        public OrderProductController (QOContext context)
        {
            _context = context;
        }

        // DELETE: api/OrderProduct/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteOrderProduct (Guid id)
        {
            var orderProduct = _context.OrderProducts.Where(p => p.OrderProductId == id).FirstOrDefault();
            if( orderProduct == null )
            {
                return false;
            }

            _context.OrderProducts.Remove(orderProduct);

            await _context.SaveChangesAsync();

            return true;
        }

        // GET: api/OrderProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProduct>> GetOrderProduct (Guid id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);

            if( orderProduct == null )
            {
                return NotFound();
            }

            return orderProduct;
        }

        // GET: api/OrderProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts ()
        {
            return await _context.OrderProducts.ToListAsync();
        }

        [HttpGet("[action]/{id}")]
        public bool OrderProductExists (Guid id)
        {
            return _context.OrderProducts.Any(e => e.OrderProductId == id);
        }

        [HttpGet("[action]/{userid}/{productId}/{orderId}")]
        public bool OrderProductOfUserExistInOrder (Guid userid, Guid productId, Guid orderId)
        {
            return _context.OrderProducts.Any(e => e.BuyerId == userid && e.ProductIdReference == productId && e.ID == orderId);
        }

        [HttpGet("[action]/{productId}/{orderId}")]
        public OrderProduct OrderProductOfUserExistOnOrder (Guid productId, Guid orderId)
        {
            return _context.OrderProducts.Where(e => e.ProductIdReference == productId && e.ID == orderId).FirstOrDefault();
        }

        // POST: api/OrderProduct
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OrderProduct>> PostOrderProduct (OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderProduct", new { id = orderProduct.OrderProductId }, orderProduct);
        }

        // PUT: api/OrderProduct/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutOrderProduct (Guid id, OrderProduct orderProduct)
        {
            var product = _context.OrderProducts.Where(o => o.OrderProductId == orderProduct.OrderProductId).FirstOrDefault();

            if( product != null )
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
    }
}