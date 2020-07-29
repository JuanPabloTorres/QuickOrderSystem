using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickOrderApp.Web.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QuickOrderApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly QOContext _context;

        public UserController(QOContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<User> PutUser(User user)
        {
            var oldUser = _context.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();

            if (oldUser != null)
            {
                _context.Users.Remove(oldUser);

                _context.Users.Add(user);
                _context.Attach(user.UserLogin);

                if (user.Stores.Count > 0)
                {
                    foreach (var item in user.Stores)
                    {
                        _context.Attach(item);
                    }
                }


                try
                {

                    _context.SaveChanges();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }

                return user;
            }
            else
            {
                return null;
            }
        }

        // POST: api/User
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpGet("[action]/{username}/{password}")]
        public User CheckUserCredential(string username, string password)
        {
            var loginOfUser = _context.Logins.Where(c => c.Username == username && c.Password == password).FirstOrDefault();

            if (_context.Users.Count() > 0)
            {
                var user = _context.Users.Where(u => u.LoginId == loginOfUser.LoginId).Include(s => s.Stores).FirstOrDefault();

                //user.Employees = _context.Employees.Where(e => e.UserId == user.UserId).Include(s => s.EmployeeStore).ToList();
                return user;
            }
            else
            {
                return null;
            }

        }

        [HttpGet("[action]/{email}")]
        public bool ForgotCodeSend(string email)
        {
            var result = _context.Users.Where(u => u.Email == email).FirstOrDefault();

            if (result != null)
            {
                var forgotpassword = new ForgotPassword()
                {
                    Code = Guid.NewGuid().ToString(),
                    Email = result.Email,

                };

                _context.ForgotPasswords.Add(forgotpassword);

                _context.SaveChangesAsync();

                var senderEmail = new MailAddress("est.juanpablotorres@gmail.com", "Quick Order");
                var receiverEmail = new MailAddress(result.Email, result.Name);

                var sub = "Quick Order Forgot Password Code";
                var body = "<b>Forgot Secret Code:</b>" + forgotpassword.Code;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("est.juanpablotorres@gmail.com", "jp84704tt")
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    IsBodyHtml = true,
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }

                return true;

            }
            else
            {
                return false;
            }
        }

        [HttpGet("[action]/{code}")]
        public bool ConfirmCode(string code)
        {
            var result = _context.ForgotPasswords.Where(u => u.Code == code).FirstOrDefault();
            var userInfo = _context.Users.Where(u => u.Email == result.Email).Include(l => l.UserLogin).FirstOrDefault();

            if (result != null)
            {

                var senderEmail = new MailAddress("est.juanpablotorres@gmail.com", "Quick Order");
                var receiverEmail = new MailAddress(userInfo.Email, userInfo.Name);

                var sub = "Loging Credials";
                var body = "<div>Username" + userInfo.UserLogin.Username + "</div>" + "<div>Password" + userInfo.UserLogin.Password + "</div>";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("est.juanpablotorres@gmail.com", "jp84704tt")
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    IsBodyHtml = true,
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }

                _context.ForgotPasswords.Remove(result);
                _context.SaveChangesAsync();

                return true;

            }
            else
            {
                return false;
            }
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
