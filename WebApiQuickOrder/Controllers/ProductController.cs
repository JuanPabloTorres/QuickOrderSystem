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
    public class ProductController : ControllerBase
    {
        private readonly QOContext _context;

        public ProductController(QOContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("[action]/{StoreId}")]
        public IEnumerable<Product> GetProductFromStore(Guid StoreId)
        {
            var result = _context.Products.Where(p => p.StoreId == StoreId);

            return result;
        }

        [HttpGet("[action]/{storeId}/{type}")]
        public async Task<IEnumerable<Product>> GetSpecificProductTypeFromStore(Guid storeId,ProductType type)
        {
            var result =  _context.Products.Where(p => p.Type == type && p.StoreId == storeId).ToList();

            return result;
        }

        [HttpGet("[action]/{storeid}/{lowquantity}")]
        public IEnumerable<Product> GetProductWithLowQuantity(Guid storeid, int lowquantity)
        {
            var result = _context.Products.Where(p => p.InventoryQuantity <= lowquantity && p.StoreId == storeid);

            return result;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutProduct(Product product)
        {
            var oldproducts = _context.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();

            if (oldproducts != null)
            {
                _context.Products.Remove(oldproducts);

                _context.SaveChanges();

                _context.Products.Add(product);

                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        // POST: api/Product
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<bool> UpdateInventoryFromOrderSubmited(Guid productId, int quantity)
        {
            var product = _context.Products.Where(p => p.ProductId == productId).FirstOrDefault();


            if (product != null)
            {

                _context.Products.Remove(product);

                _context.SaveChanges();

                product.InventoryQuantity -= quantity;

                _context.Products.Add(product);

                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }



        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
