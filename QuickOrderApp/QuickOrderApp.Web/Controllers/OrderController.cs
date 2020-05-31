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
    public class OrderController : ControllerBase
    {
        private readonly QOContext _context;

        public OrderController(QOContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutOrder(Order order)
        {
            var oldOrder = _context.Orders.Where(o => o.OrderId == order.OrderId).FirstOrDefault();

            if (oldOrder != null)
            {
               

                //foreach (var item in order.OrderProducts)
                //{
                //    _context.Attach(item);
                //}

                try
                {
                    _context.Orders.Remove(oldOrder);

                    _context.SaveChanges();

                    _context.Add(order);
                    _context.Attach(order.StoreOrder);

                    foreach (var item in order.StoreOrder.Products)
                    {

                    _context.Attach(item);
                    }

                    foreach (var item in order.StoreOrder.WorkHours)
                    {
                        _context.Attach(item);
                    }
                    _context.SaveChanges();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }
               

                return true;
            }
            else
            {
                return false;
            }

        }

        // POST: api/Order
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }

        [HttpGet("[action]/{userid}/{storeid}")]
        public Order HaveOrder(Guid userid, Guid storeid)
        {
            return _context.Orders.Where(e => e.BuyerId == userid && e.StoreId == storeid).FirstOrDefault();
        }

        [HttpGet("[action]/{userid}")]
        public IEnumerable<Order> GetUserOrders(Guid userid)
        {
            return _context.Orders.Where(e => e.BuyerId == userid).Include(o=>o.OrderProducts).ToList();
        }

        [HttpGet("[action]/{storeId}")]
        public IEnumerable<Order> GetStoreOrders(Guid storeId)
        {
            return _context.Orders.Where(e => e.StoreId == storeId).Include(o => o.OrderProducts).ToList();
        }
    }
}
