using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
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
        public readonly HubConnection hubConnection;
        public UsersConnectedController(QOContext context)
        {


            _context = context;

            hubConnection = new HubConnectionBuilder().WithUrl("http://juantorres9-001-site1.etempurl.com" + "/comunicationhub").Build();
            //hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5000" + "/comunicationhub").Build();
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
            var userconnections = await _context.usersConnecteds.Where(usc => usc.UserID == usersConnected.UserID && usc.IsDisable == false).ToListAsync();


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


        [HttpGet("[action]/{storeId}/{orderId}")]
        public async Task<bool> SendOrdersToEmployees(string storeId,string orderId)
        {

            await hubConnection.StartAsync();
            var employees =await  _context.Employees.Where(emp => emp.StoreId.ToString() == storeId).ToListAsync();

            List<UsersConnected> usersConnecteds = new List<UsersConnected>();

            foreach (var item in employees)
            {

                var userconnection = _context.usersConnecteds.Where(c => c.UserID == item.UserId && c.IsDisable == false).FirstOrDefault();

                if (userconnection != null)
                {

                    usersConnecteds.Add(userconnection);
                }


            }

            string Preparemessage = $"Order: { orderId}";

            foreach (var item in usersConnecteds)
            {

                await hubConnection.InvokeAsync("OrderPreparer", Preparemessage, item.HubConnectionID);
            }


            await hubConnection.StopAsync();

            return true;


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
