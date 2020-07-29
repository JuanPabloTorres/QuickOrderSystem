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
    public class WorkHourController : ControllerBase
    {
        private readonly QOContext _context;

        public WorkHourController(QOContext context)
        {
            _context = context;
        }

        // GET: api/WorkHour
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkHour>>> GetWorkHours()
        {
            return await _context.WorkHours.ToListAsync();
        }

        //// GET: api/WorkHour
        //[HttpGet("[action]/{empid}")]
        //public async Task<ActionResult<IEnumerable<WorkHour>>> GetWorkHoursOfEmployee(Guid empid)
        //{
        //    //return await _context.WorkHours.Where(e=>e.);
        //}

        // GET: api/WorkHour/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkHour>> GetWorkHour(Guid id)
        {
            var workHour = await _context.WorkHours.FindAsync(id);

            if (workHour == null)
            {
                return NotFound();
            }

            return workHour;
        }

        // PUT: api/WorkHour/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkHour(Guid id, WorkHour workHour)
        {
            if (id != workHour.WorkHourId)
            {
                return BadRequest();
            }

            _context.Entry(workHour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkHourExists(id))
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

        // POST: api/WorkHour
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<WorkHour>> PostWorkHour(WorkHour workHour)
        {
            _context.WorkHours.Add(workHour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkHour", new { id = workHour.WorkHourId }, workHour);
        }

        // DELETE: api/WorkHour/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkHour>> DeleteWorkHour(Guid id)
        {
            var workHour = await _context.WorkHours.FindAsync(id);
            if (workHour == null)
            {
                return NotFound();
            }

            _context.WorkHours.Remove(workHour);
            await _context.SaveChangesAsync();

            return workHour;
        }

        private bool WorkHourExists(Guid id)
        {
            return _context.WorkHours.Any(e => e.WorkHourId == id);
        }
    }
}
