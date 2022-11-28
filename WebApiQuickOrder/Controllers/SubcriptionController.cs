using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiQuickOrder.Context;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcriptionController : ControllerBase
    {
        private readonly QOContext _context;

        public SubcriptionController (QOContext context)
        {
            _context = context;
        }

        // DELETE: api/Subcription/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subcription>> DeleteSubcription (string id)
        {
            var subcription = await _context.Subcriptions.FindAsync(id);

            if( subcription == null )
            {
                return NotFound();
            }

            _context.Subcriptions.Remove(subcription);

            await _context.SaveChangesAsync();

            return subcription;
        }

        // GET: api/Subcription/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subcription>> GetSubcription (string id)
        {
            var subcription = await _context.Subcriptions.FindAsync(id);

            if( subcription == null )
            {
                return NotFound();
            }

            return subcription;
        }

        // GET: api/Subcription
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subcription>>> GetSubcriptions ()
        {
            return await _context.Subcriptions.ToListAsync();
        }

        // POST: api/Subcription
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Subcription>> PostSubcription (Subcription subcription)
        {
            _context.Subcriptions.Add(subcription);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch( DbUpdateException )
            {
                if( SubcriptionExists(subcription.StripeSubcriptionID) )
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubcription", new { id = subcription.StripeSubcriptionID }, subcription);
        }

        // PUT: api/Subcription/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubcription (string id, Subcription subcription)
        {
            if( id != subcription.StripeSubcriptionID )
            {
                return BadRequest();
            }

            _context.Entry(subcription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch( DbUpdateConcurrencyException )
            {
                if( !SubcriptionExists(id) )
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool SubcriptionExists (string id)
        {
            return _context.Subcriptions.Any(e => e.StripeSubcriptionID == id);
        }
    }
}