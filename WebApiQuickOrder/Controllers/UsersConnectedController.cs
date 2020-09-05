using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using WebApiQuickOrder.Context;
using System.Threading.Tasks.Dataflow;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersConnectedController : ControllerBase
    {
        private readonly QOContext _context;

        public UsersConnectedController(QOContext context)
        {
            _context = context;
        }

        // GET: api/UsersConnected
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersConnected>>> GetusersConnecteds()
        {
            return await _context.usersConnecteds.ToListAsync();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<bool>> ModifyOldConnections(UsersConnected usersConnected)
        {
            var userconnections =await _context.usersConnecteds.Where(usc => usc.UserID == usersConnected.UserID &&   usc.ConnecteDate < usersConnected.ConnecteDate  && usc.IsDisable == false).ToListAsync();


            if (userconnections.Count() > 0)
            {

                foreach (var item in userconnections)
                {
                    item.IsDisable = true;

                    _context.Entry(item).State = EntityState.Modified;

                _context.SaveChanges();

                }


                return true;
            }
            else
            {
                return NotFound();
            }
        }


        //public async Task<ActionResult<bool>> DisconnectUser(string hubId)
        //{
            
        //}

        // GET: api/UsersConnected/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersConnected>> GetUsersConnected(string id)
        {
            var usersConnected = await _context.usersConnecteds.FindAsync(id);

            if (usersConnected == null)
            {
                return NotFound();
            }

            return usersConnected;
        }

        // PUT: api/UsersConnected/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<ActionResult<bool>> PutUsersConnected(UsersConnected usersConnected)
        {
           

            _context.Entry(usersConnected).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersConnectedExists(usersConnected.HubConnectionID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

           
        }

        // POST: api/UsersConnected
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UsersConnected>> PostUsersConnected(UsersConnected usersConnected)
        {
            _context.usersConnecteds.Add(usersConnected);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsersConnectedExists(usersConnected.HubConnectionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsersConnected", new { id = usersConnected.HubConnectionID }, usersConnected);
        }

        // DELETE: api/UsersConnected/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUsersConnected(string id)
        {
            var usersConnected = await _context.usersConnecteds.FindAsync(id);
            if (usersConnected == null)
            {
                return NotFound();
            }

            _context.usersConnecteds.Remove(usersConnected);
            await _context.SaveChangesAsync();

            if (_context.usersConnecteds.Any(u=>u.HubConnectionID == id))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private bool UsersConnectedExists(string id)
        {
            return _context.usersConnecteds.Any(e => e.HubConnectionID == id);
        }
        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<UsersConnected>> GetUserConnectedID(Guid userId)
        {
            var result = await _context.usersConnecteds.Where(u => u.UserID == userId && u.IsDisable == false).FirstOrDefaultAsync();

            return result;
        }
    }
}
