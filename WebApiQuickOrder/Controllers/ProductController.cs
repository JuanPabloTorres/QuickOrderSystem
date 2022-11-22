using Library.Models;
using Microsoft.AspNetCore.Authorization;
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

        public ProductController (QOContext context)
        {
            _context = context;
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct (Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if( product == null )
            {
                return NotFound();
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return product;
        }

        [HttpPost("[action]/{StoreId}")]
        public async Task<IEnumerable<Product>> GetDifferentProductFromStore (IEnumerable<Product> productsAdded, Guid storeId)
        {
            var result = await _context.Products.Where(p => p.StoreId == storeId).ToListAsync();

            List<Product> products = new List<Product>();

            foreach( var item in result )
            {
                if( !productsAdded.Any(p => p.ProductId == item.ProductId) )
                {
                    products.Add(item);

                    if( products.Count == 5 )
                    {
                        return products;
                    }
                }
            }

            return products;
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct (Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if( product == null )
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("[action]/{StoreId}")]
        public async Task<IEnumerable<Product>> GetProductFromStore (Guid StoreId)
        {
            var result = await _context.Products.Where(p => p.StoreId == StoreId).ToListAsync();

            List<Product> products = new List<Product>();

            foreach( var item in result )
            {
                products.Add(item);

                if( products.Count() == 5 )
                {
                    return products;
                }
            }

            return products;
        }

        // GET: api/Product
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts ()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("[action]/{storeid}/{lowquantity}")]
        public IEnumerable<Product> GetProductWithLowQuantity (Guid storeid, int lowquantity)
        {
            var result = _context.Products.Where(p => p.InventoryQuantity <= lowquantity && p.StoreId == storeid);

            return result;
        }

        [HttpGet("[action]/{storeId}/{type}")]
        public async Task<IEnumerable<Product>> GetSpecificProductTypeFromStore (Guid storeId, ProductType type)
        {
            var result = await _context.Products.Where(p => p.Type == type && p.StoreId == storeId).ToListAsync();

            List<Product> products = new List<Product>();

            foreach( var item in result )
            {
                products.Add(item);

                if( products.Count() == 5 )
                {
                    return products;
                }
            }

            return products;
        }

        // POST: api/Product
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct (Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutProduct (Product product)
        {
            var oldproducts = _context.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();

            if( oldproducts != null )
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

        [HttpGet("[action]/{storeId}/{item}")]
        public async Task<Product> SearchItemOfStore (string storeId, string item)
        {
            var result = await _context.Products.Where(p => p.StoreId.ToString() == storeId && p.ProductName == item).FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> UpdateInventoryFromOrderSubmited (Guid productId, int quantity)
        {
            var product = await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();

            if( product != null )
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

        [HttpGet("[action]")]
        public async Task<bool> UpdateInventoryItem (Guid productId, int quantity)
        {
            var product = await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();

            product.InventoryQuantity -= quantity;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();

                return true;
            }
            catch( Exception e )
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        private bool ProductExists (Guid id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}