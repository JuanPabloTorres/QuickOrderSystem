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

        [HttpGet("[action]/{storeId}")]
        public async Task<IEnumerable<WorkHour>> GetStoreWorkHours(string storeId)
        {
            return await _context.WorkHours.Where(wh => wh.StoreId.ToString() == storeId).ToListAsync();
        }

        // PUT: api/WorkHour/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutWorkHour( WorkHour workHour)
        {           

            _context.Entry(workHour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkHourExists(workHour.WorkHourId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            
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
