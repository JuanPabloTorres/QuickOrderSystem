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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        // GET: api/Order/5
        [HttpGet("[action]/{userid}/{storeid}/{status}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersOfStoreOfUserWithSpecifiStatus(Guid userid, Guid storeid,Status status)
        {
            var _completeOrders = await _context.Orders.Where(o => o.StoreId == storeid && o.BuyerId == userid && o.OrderStatus == status).Include(op => op.OrderProducts).ToListAsync();
            return _completeOrders;
        }

        // GET: api/Order/5
        [HttpGet("[action]/{userid}/{status}")]
        [Authorize(Policy = Policies.User)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersOfUserWithSpecificStatus(Guid userid, Status status)
        {
            var result = await _context.Orders.Where(o => o.BuyerId == userid && o.OrderStatus == status && o.IsDisisble == false).Include(op => op.OrderProducts).ToListAsync();

            List<Order> orders = new List<Order>();

            foreach (var item in result)
            {
                orders.Add(item);

                if (orders.Count() == 5)
                {
                    return orders.ToList();
                }
            }

            return orders.ToList();
        }

        // GET: api/Order/5
        [HttpPost("[action]/{status}/{userid}")]       
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersOfUserWithSpecificStatusDifferent(IEnumerable<Order> ordersAdded,Status status,Guid userid)
        {
            if (ordersAdded.Count() < _context.Orders.Count())
            {

                List<Order> orders = new List<Order>();


                var result = await _context.Orders.Where(o => o.BuyerId == userid && o.OrderStatus == status).Include(op=>op.OrderProducts).ToListAsync();

                foreach (var item in result)
                {
                    if (!item.IsDisisble)
                    {
                        if (!ordersAdded.Any(x => x.OrderId == item.OrderId))
                        {
                            orders.Add(item);

                            if (orders.Count == 5)
                            {
                                return orders;
                            }
                        }

                    }
                }



                return orders.ToList();
            }

            return null;
        }



        // GET: api/Order/5
        [HttpGet("[action]/{userid}/{storeid}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetSubmitedOrderOfStoreSpecificUser(Guid userid, Guid storeid)
        {
            var _completeOrders = await _context.Orders.Where(o => o.StoreId == storeid && o.BuyerId == userid && o.OrderStatus == Status.Submited).Include(op => op.OrderProducts).ToListAsync();
            return _completeOrders;

        }

        [HttpGet("[action]/{userid}/{storeid}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetNotSubmitedOrderOfStoreSpecificUser(Guid userid, Guid storeid)
        {
            var _completeOrders = await _context.Orders.Where(o => o.StoreId == storeid && o.BuyerId == userid && o.OrderStatus == Status.NotSubmited).Include(op => op.OrderProducts).ToListAsync();
            return _completeOrders;

        }

        // PUT: api/Order/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.


        [HttpPut]
        public async Task<ActionResult<bool>> PutOrder(Order orderupdated)
        {
            _context.Entry(orderupdated).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                

                if (orderupdated.OrderStatus == Status.Submited)
                {
                    foreach (var item in orderupdated.OrderProducts)
                    {
                        var product = _context.Products.Where(p => p.StoreId == item.StoreId && p.ProductId == item.ProductIdReference).FirstOrDefault();

                        if (product != null && product.InventoryQuantity > 0)
                        {
                            product.InventoryQuantity -= item.Quantity;

                            _context.Entry(product).State = EntityState.Modified;

                           await _context.SaveChangesAsync();
                          

                        }
                    }
                }

                //_context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(orderupdated.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    return false;
                }
            }

           
        }

        //[HttpPut]
        //public async Task<bool> PutOrder(Order order)
        //{

            
        //    var oldOrder = _context.Orders.Where(o => o.OrderId == order.OrderId).FirstOrDefault();



        //    if (oldOrder != null)
        //    {

        //        try
        //        {

        //            _context.Orders.Remove(oldOrder);

        //            _context.SaveChanges();

        //            _context.Add(order);

        //            //order.StoreOrder= null;

        //            if (order.StoreOrder != null)
        //            {

        //                _context.Attach(order.StoreOrder);

        //                if (order.StoreOrder.Products != null)
        //                {

        //                    foreach (var item in order.StoreOrder.Products)
        //                    {

        //                        _context.Attach(item);
        //                    }
        //                }


        //                order.StoreOrder.WorkHours = null;

        //                if (order.StoreOrder.WorkHours != null)
        //                {

        //                    foreach (var item in order.StoreOrder.WorkHours)
        //                    {
        //                        _context.Attach(item);
        //                    }
        //                }
        //            }

        //            ProductController productController = new ProductController(_context);
        //            if (order.OrderStatus == Status.Submited)
        //            {
        //                foreach (var item in order.OrderProducts)
        //                {
        //                    var product = _context.Products.Where(p => p.StoreId == item.StoreId && p.ProductName == item.ProductName).FirstOrDefault();

        //                    if (product != null && product.InventoryQuantity > 0)
        //                    {

        //                        var result = productController.UpdateInventoryFromOrderSubmited(product.ProductId, item.Quantity);

        //                    }


        //                }
        //            }




        //            _context.SaveChanges();
        //        }
        //        catch (Exception e)
        //        {

        //            Console.WriteLine(e);
        //        }


        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}


        [HttpGet("[action]/{orderId}")]
        public async Task<bool> DisableOrder(Guid orderId)
        {

            var orderToDisable = await _context.Orders.Where(o => o.OrderId == orderId).FirstOrDefaultAsync();

            orderToDisable.IsDisisble = true;
            _context.Entry(orderToDisable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(orderToDisable.OrderId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
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
        [Authorize(Policy = Policies.User)]
        public Order HaveOrderOfSpecificStore(Guid userid, Guid storeid)
        {
            return _context.Orders.Where(e => e.BuyerId == userid && e.StoreId == storeid).FirstOrDefault();
        }

        [HttpGet("[action]/{userid}")]
        [Authorize(Policy = Policies.User)]
        public IEnumerable<Order> GetUserOrders(Guid userid)
        {
            return _context.Orders.Where(e => e.BuyerId == userid).Include(o => o.OrderProducts).ToList();
        }

        [HttpGet("[action]/{userid}/{storeid}")]
        public IEnumerable<Order> GetUserOrdersOfStore(Guid userid,Guid storeid)
        {
            return _context.Orders.Where(e => e.BuyerId == userid && e.StoreId == storeid).Include(o => o.OrderProducts).ToList();
        }

        [HttpGet("[action]/{userid}")]
        [Authorize(Policy = Policies.User)]
        public IEnumerable<Order> GetUserOrdersWithToken(Guid userid)
        {
            return _context.Orders.Where(e => e.BuyerId == userid).Include(o => o.OrderProducts).ToList();
        }

        [HttpGet("[action]/{storeId}")]
        [Authorize(Policy  = Policies.StoreControl)]       
        public IEnumerable<Order> GetStoreOrders(Guid storeId)
        {
            return _context.Orders.Where(e => e.StoreId == storeId && e.OrderStatus == Status.Submited).Include(o => o.OrderProducts).ToList();
        }
    }
}
