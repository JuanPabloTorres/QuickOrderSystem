using Library.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiQuickOrder.Context;
using WebApiQuickOrder.Models;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            var order = _context.Orders.Where(o => o.OrderId == id).Include(op => op.OrderProducts).FirstOrDefault();

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

                try
                {

                    _context.Orders.Remove(oldOrder);

                    _context.SaveChanges();

                    _context.Add(order);

                    //order.StoreOrder= null;

                    if (order.StoreOrder != null)
                    {

                        _context.Attach(order.StoreOrder);

                        if (order.StoreOrder.Products != null)
                        {

                            foreach (var item in order.StoreOrder.Products)
                            {

                                _context.Attach(item);
                            }
                        }


                        order.StoreOrder.WorkHours = null;

                        if (order.StoreOrder.WorkHours != null)
                        {

                            foreach (var item in order.StoreOrder.WorkHours)
                            {
                                _context.Attach(item);
                            }
                        }
                    }

                    ProductController productController = new ProductController(_context);
                    if (order.OrderStatus == Status.Submited)
                    {
                        foreach (var item in order.OrderProducts)
                        {
                            var product = _context.Products.Where(p => p.StoreId == item.StoreId && p.ProductName == item.ProductName).FirstOrDefault();

                            if (product != null && product.InventoryQuantity > 0)
                            {

                                var result = productController.UpdateInventoryFromOrderSubmited(product.ProductId, item.Quantity);

                            }


                        }
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
        public Order HaveOrderOfSpecificStore(Guid userid, Guid storeid)
        {
            return _context.Orders.Where(e => e.BuyerId == userid && e.StoreId == storeid).FirstOrDefault();
        }

        [HttpGet("[action]/{userid}")]
        public IEnumerable<Order> GetUserOrders(Guid userid)
        {
            return _context.Orders.Where(e => e.BuyerId == userid).Include(o => o.OrderProducts).ToList();
        }

        [HttpGet("[action]/{userid}")]
        [Authorize(Policy = Policies.User)]
        public IEnumerable<Order> GetUserOrdersWithToken(Guid userid)
        {
            return _context.Orders.Where(e => e.BuyerId == userid).Include(o => o.OrderProducts).ToList();
        }

        [HttpGet("[action]/{storeId}")]
        public IEnumerable<Order> GetStoreOrders(Guid storeId)
        {
            return _context.Orders.Where(e => e.StoreId == storeId).Include(o => o.OrderProducts).ToList();
        }
    }
}
