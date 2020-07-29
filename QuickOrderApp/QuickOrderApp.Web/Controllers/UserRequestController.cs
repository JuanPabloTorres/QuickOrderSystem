using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickOrderApp.Web.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOrderApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRequestController : ControllerBase
    {
        private readonly QOContext _context;

        public UserRequestController(QOContext context)
        {
            _context = context;
        }

        // GET: api/UserRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRequest>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/UserRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRequest>> GetUserRequest(Guid id)
        {
            var userRequest = await _context.Requests.FindAsync(id);

            if (userRequest == null)
            {
                return NotFound();
            }

            return userRequest;
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IEnumerable<UserRequest>> GetRequestOfUser(Guid userId)
        {
            var userRequest = _context.Requests.Where(r => r.ToUser == userId);

            return userRequest;
        }

        [HttpGet("[action]/{storeId}")]
        public async Task<IEnumerable<UserRequest>> GetRequestAcceptedOfStore(Guid storeId)
        {
            var userRequest = _context.Requests.Where(r => r.FromStore == storeId);

            return userRequest;
        }

        // PUT: api/UserRequest/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutUserRequest(UserRequest userRequest)
        {
            var request = _context.Requests.Where(r => r.RequestId == userRequest.RequestId).FirstOrDefault();

            if (request != null)
            {
                _context.Requests.Remove(request);

                _context.Requests.Add(userRequest);

                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        // POST: api/UserRequest
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserRequest>> PostUserRequest(UserRequest userRequest)
        {
            _context.Requests.Add(userRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserRequest", new { id = userRequest.RequestId }, userRequest);
        }

        // DELETE: api/UserRequest/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserRequest>> DeleteUserRequest(Guid id)
        {
            var userRequest = await _context.Requests.FindAsync(id);
            if (userRequest == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(userRequest);
            await _context.SaveChangesAsync();

            return userRequest;
        }

        [HttpGet("[action]/{userId}/{storeId}")]
        public bool UserRequestExists(Guid userid, Guid storeId)
        {
            var result = _context.Requests.Where(e => e.FromStore == storeId && e.ToUser == userid).FirstOrDefault();

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
