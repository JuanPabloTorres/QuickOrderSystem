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
    public class EmployeeController : ControllerBase
    {
        private readonly QOContext _context;

        public EmployeeController (QOContext context)
        {
            _context = context;
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteEmployee (Guid id)
        {
            var toremoveEmp = _context.Employees.Where(e => e.ID == id).FirstOrDefault();

            if( toremoveEmp != null )
            {
                _context.Employees.Remove(toremoveEmp);

                _context.Requests.Remove(_context.Requests.Where(r => r.ToUser == toremoveEmp.UserId).FirstOrDefault());

                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee (Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if( employee == null )
            {
                return NotFound();
            }

            return employee;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees ()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("[action]/{storeId}")]
        public async Task<IEnumerable<Employee>> GetEmployeesOfStore (Guid storeId)
        {
            var employees = _context.Employees.Where(e => e.StoreId == storeId);

            return employees;
        }

        // GET: api/Employee/5
        [HttpGet("[action]/{userId}/{StoreId}")]
        public async Task<Employee> GetSpecificStoreEmployee (Guid userId, Guid StoreId)
        {
            var result = _context.Employees.Where(e => e.UserId == userId && e.StoreId == StoreId).FirstOrDefault();

            return result;
        }

        // GET: api/Employee
        [HttpGet("[action]/{userId}")]
        public async Task<IEnumerable<Employee>> GetUserEmployees (string userId)
        {
            var employees = _context.Employees.Where(e => e.UserId.ToString() == userId);

            return employees;
        }

        // GET: api/Employee/5
        [HttpGet("[action]/{storeId}/{userId}")]
        public async Task<ActionResult<bool>> IsEmployeeFromStore (Guid storeId, Guid userId)
        {
            var result = _context.Employees.Any(emp => emp.StoreId == storeId && emp.UserId == userId);
            return result;
        }

        // POST: api/Employee
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee (Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.ID }, employee);
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<bool> PutEmployee (Employee employee)
        {
            var oldemp = _context.Employees.Where(e => e.ID == employee.ID).FirstOrDefault();

            if( oldemp != null )
            {
                try
                {
                    //_context.EmployeeWorkHours.RemoveRange(oldemp.EmployeeWorkHours);
                    _context.Employees.Remove(oldemp);

                    _context.SaveChanges();

                    _context.Employees.Add(employee);

                    if( employee.EmployeeUser != null )
                    {
                        _context.Users.Attach(employee.EmployeeUser);
                    }

                    _context.SaveChanges();
                }
                catch( Exception e )
                {
                    Console.WriteLine(e);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool EmployeeExists (Guid id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}