﻿using Library.Models;
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
    public class StoreController : ControllerBase
    {
        private readonly QOContext _context;

        public StoreController (QOContext context)
        {
            _context = context;
        }

        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete (Guid id)
        {
            var store = await _context.Stores.FindAsync(id);

            if( store == null )
            {
                return false;
            }

            _context.Stores.Remove(store);

            await _context.SaveChangesAsync();

            var result = await _context.Stores.AnyAsync(s => s.StoreId == id);

            if( result )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut("[action]")]
        public async Task<bool> DisableStore (Store store)
        {
            try
            {
                _context.Entry(store).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return true;
            }
            catch( DbUpdateConcurrencyException )
            {
                if( !StoreExists(store.StoreId) )
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: api/Store
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Store>>> GetAvailableStore ()
        {
            var result = await _context.Stores.Where(st => st.IsDisable == false).ToListAsync();

            List<Store> stores = new List<Store>();

            foreach( var item in result )
            {
                if( !item.IsDisable )
                {
                    stores.Add(item);

                    if( stores.Count() == 5 )
                    {
                        return stores;
                    }
                }
            }

            return stores.ToList();
        }

        // GET: api/Store/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Store>> GetAvailableStoreInformation (Guid id)
        {
            var store = await _context.Stores.Where(s => s.StoreId == id && s.IsDisable == false).Include(p => p.Products).Include(w => w.WorkHours).FirstOrDefaultAsync();

            if( store == null )
            {
                return NotFound();
            }

            return store;
        }

        // GET: api/Store
        [HttpPost("[action]")]
        public ActionResult<IEnumerable<Store>> GetDifferentStore (IEnumerable<Store> storesAdded)
        {
            if( storesAdded.Count() < _context.Stores.Count() )
            {
                List<Store> stores = new List<Store>();

                foreach( var item in _context.Stores )
                {
                    if( !item.IsDisable )
                    {
                        if( !storesAdded.Any(x => x.StoreId == item.StoreId) )
                        {
                            stores.Add(item);

                            if( stores.Count == 5 )
                            {
                                return stores;
                            }
                        }
                    }
                }

                return stores.ToList();
            }

            return null;
        }

        [HttpGet("[action]/{category}")]
        public IEnumerable<Store> GetSpecificStoreCategory (string category)
        {
            StoreType storeType;

            Enum.TryParse(category, out storeType);

            return _context.Stores.Where(s => s.StoreType == storeType).ToList();
        }

        // GET: api/Store/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore (Guid id)
        {
            var store = await _context.Stores.Where(s => s.StoreId == id).Include(p => p.Products).Include(w => w.WorkHours).FirstOrDefaultAsync();

            if( store == null )
            {
                return NotFound();
            }

            return store;
        }

        // GET: api/Store/5
        [HttpGet("[action]/{storeId}")]
        public async Task<ActionResult<string>> GetStoreDestinationPaymentKey (Guid storeId)
        {
            var keyResult = _context.Stores.Where(s => s.StoreId == storeId).FirstOrDefault().SKKey;

            if( keyResult == null )
            {
                return NotFound();
            }

            return keyResult;
        }

        // GET: api/Store/5
        [HttpGet("[action]/{storeId}")]
        public async Task<ActionResult<string>> GetStoreDestinationPublicPaymentKey (Guid storeId)
        {
            var keyResult = _context.Stores.Where(s => s.StoreId == storeId).FirstOrDefault().PBKey;

            if( keyResult == null )
            {
                return NotFound();
            }

            return keyResult;
        }

        // GET: api/Store
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores ()
        {
            return await _context.Stores.ToListAsync();
        }

        // GET: api/Store
        [HttpGet("[action]/{userid}")]
        public async Task<ActionResult<IEnumerable<Store>>> GetStoresFromUser (Guid userid)
        {
            return await _context.Stores.Where(s => s.UserId == userid && s.IsDisable == false).ToListAsync();
        }

        // POST: api/Store
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore (Store store)
        {
            _context.Stores.Add(store);

            //_context.WorkHours.Attach(store.WorkHours)
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStore", new { id = store.StoreId }, store);
        }

        // PUT: api/Store/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutStore (Store store)
        {
            try
            {
                _context.Entry(store).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                var result = _context.WorkHours.Where(w => w.StoreId == store.StoreId).ToList();

                _context.WorkHours.RemoveRange(result);

                await _context.SaveChangesAsync();

                _context.WorkHours.AddRange(store.WorkHours);

                await _context.SaveChangesAsync();

                return true;
            }
            catch( DbUpdateConcurrencyException )
            {
                if( !StoreExists(store.StoreId) )
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: api/Store
        [HttpGet("[action]/{searchStore}")]
        public async Task<IEnumerable<Store>> SearchStore (string searchStore)
        {
            return await _context.Stores.Where(s => s.StoreName == searchStore).ToListAsync();
        }

        private bool StoreExists (Guid id)
        {
            return _context.Stores.Any(e => e.StoreId == id);
        }
    }
}