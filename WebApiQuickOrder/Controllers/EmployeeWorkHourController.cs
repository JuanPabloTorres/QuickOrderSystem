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
    public class EmployeeWorkHourController : ControllerBase
    {
        private readonly QOContext _context;

        public EmployeeWorkHourController(QOContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeWorkHour
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeWorkHour>>> GetEmployeeWorkHours()
        {
            return await _context.EmployeeWorkHours.ToListAsync();
        }

        // GET: api/EmployeeWorkHour
        [HttpGet("[action]/{empId}")]
        public async Task<IEnumerable<EmployeeWorkHour>> GetEmployeeWorkHours(string empId)
        {
            return _context.EmployeeWorkHours.Where(e => e.EmpId.ToString() == empId).ToList();
        }

        // GET: api/EmployeeWorkHour/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeWorkHour>> GetEmployeeWorkHour(Guid id)
        {
            var employeeWorkHour = await _context.EmployeeWorkHours.FindAsync(id);

            if (employeeWorkHour == null)
            {
                return NotFound();
            }

            return employeeWorkHour;
        }

        // PUT: api/EmployeeWorkHour/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeWorkHour(Guid id, EmployeeWorkHour employeeWorkHour)
        {
            if (id != employeeWorkHour.WorkHourId)
            {
                return BadRequest();
            }

            _context.Entry(employeeWorkHour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeWorkHourExists(id))
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

        // POST: api/EmployeeWorkHour
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EmployeeWorkHour>> PostEmployeeWorkHour(EmployeeWorkHour employeeWorkHour)
        {
            _context.EmployeeWorkHours.Add(employeeWorkHour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeWorkHour", new { id = employeeWorkHour.WorkHourId }, employeeWorkHour);
        }

        // DELETE: api/EmployeeWorkHour/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeWorkHour>> DeleteEmployeeWorkHour(Guid id)
        {
            var employeeWorkHour = await _context.EmployeeWorkHours.FindAsync(id);
            if (employeeWorkHour == null)
            {
                return NotFound();
            }

            _context.EmployeeWorkHours.Remove(employeeWorkHour);
            await _context.SaveChangesAsync();

            return employeeWorkHour;
        }

        private bool EmployeeWorkHourExists(Guid id)
        {
            return _context.EmployeeWorkHours.Any(e => e.WorkHourId == id);
        }
    }
}
