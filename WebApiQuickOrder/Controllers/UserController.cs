using Library.DTO;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiQuickOrder.Context;
using WebApiQuickOrder.Models;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly QOContext _context;
        private readonly IConfiguration _configuration;

        public UserController(QOContext context, IConfiguration configuration)
        {
            _context = context;
            this._configuration = configuration;
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

        // GET: api/User/5
        [HttpGet("[action]/{name}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserWithName(string name)
        {
            var _users = await _context.Users.Where(user => user.Name == name).ToListAsync();

            if (_users == null)
            {
                return NotFound();
            }

            List<UserDTO> userDTOs = new List<UserDTO>();

            foreach (var item in _users)
            {
                var userDTO = new UserDTO()
                {
                    UserId = item.UserId,
                    Name = item.Name,
                    Address = item.Address,
                    Email = item.Email,
                    Gender = item.Gender,
                    Phone = item.Phone
                };

                userDTOs.Add(userDTO);

            }



            return userDTOs;
        }


        [HttpGet("[action]/{email}")]
        public async Task<bool> EmailExist(string email)
        {
            var result = await _context.Users.AnyAsync(e => e.Email == email);

            return result;

        }

        [HttpGet("[action]/{code}/{userid}")]
        public async Task<ActionResult<bool>> ValidateEmail(string code, string userid)
        {
            var validate = await _context.EmailValidations.Where(email => email.ValidationCode == code && email.UserId.ToString() == userid && DateTime.Now <= email.ExpDate).FirstOrDefaultAsync();


            if (validate != null)
            {
                var user = await _context.Users.Where(u => u.UserId.ToString() == userid).FirstOrDefaultAsync();

                user.IsValidUser = true;

                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;

        }

        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<bool>> ResendCode(string userId)
        {

            var user = _context.Users.Where(u => u.UserId.ToString() == userId).FirstOrDefault();

            if (user != null)
            {
                string validationcode = Guid.NewGuid().ToString().Substring(0, 5);

                var emailValidation = new EmailValidation()
                {
                    Email = user.Email,
                    ExpDate = DateTime.Now.AddMinutes(30),
                    EmailValidationId = Guid.NewGuid(),
                    UserId = user.UserId,
                    ValidationCode = validationcode
                };

                _context.EmailValidations.Add(emailValidation);

                await _context.SaveChangesAsync();

                SendValidateEmailCode(emailValidation);

                return true;
            }

            return false;
        }




        // PUT: api/User/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]

        public async Task<ActionResult<User>> PutUser(User user)
        {

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return user;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //var oldUser = _context.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();

            //if (oldUser != null)
            //{
            //    try
            //    {
            //        _context.Users.Remove(oldUser);

            //        _context.Users.Add(user);
            //        _context.Attach(user.UserLogin);

            //        if (user.Stores.Count > 0)
            //        {
            //            foreach (var item in user.Stores)
            //            {
            //                _context.Attach(item);
            //            }
            //        }

            //        _context.SaveChanges();
            //    }
            //    catch (Exception e)
            //    {

            //        Console.WriteLine(e);
            //    }

            //    return user;
            //}
            //else
            //{
            //    return null;
            //}
        }




        [HttpPost]
        public async Task<ActionResult<bool>> PostUser([FromBody]User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            string validationcode = Guid.NewGuid().ToString().Substring(0, 5);

            var emailValidation = new EmailValidation()
            {
                Email = user.Email,
                ExpDate = DateTime.Now.AddMinutes(30),
                EmailValidationId = Guid.NewGuid(),
                UserId = user.UserId,
                ValidationCode = validationcode
            };

            _context.EmailValidations.Add(emailValidation);

            await _context.SaveChangesAsync();

            SendValidateEmailCode(emailValidation);

            return true;

            //return CreatedAtAction("GetUser", new { id = user.UserId }, user);
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



        Login AuthenticateUser(Login loginCredentials)
        {
            Login user = _context.Logins.SingleOrDefault(x => x.Username == loginCredentials.Username && x.Password == loginCredentials.Password);
            return user;
        }

        //Genera token de seguridad para los distintos action a los que se tendra acceso.
        //===========================================================================================
        string GenerateJWTTokenWithRole(User userInfo, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserId.ToString()),
                new Claim("fullName", userInfo.Name.ToString()),
                new Claim("role",Policies.User),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };


            if (!string.IsNullOrEmpty(role))
            {

                switch (role)
                {
                    case "Admin":
                        {
                            claims.Add(new Claim("role", Policies.Admin));
                            claims.Add(new Claim("role", Policies.StoreControl));
                            break;
                        }
                    case "Employee":
                        {
                            claims.Add(new Claim("role", Policies.Employee));
                            claims.Add(new Claim("role", Policies.StoreControl));
                            break;
                        }
                    default:
                        break;
                }

            }


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserId.ToString()),
                new Claim("fullName", userInfo.Name.ToString()),
                new Claim("role",Policies.User),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };



            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("[action]/{username}/{password}")]
        public User CheckUserCredential(string username, string password)
        {
            var loginOfUser = _context.Logins.Where(c => c.Username == username && c.Password == password).FirstOrDefault();


            if (loginOfUser != null)
            {
                if (_context.Users.Count() > 0)
                {
                    var user = _context.Users.Where(u => u.LoginId == loginOfUser.LoginId).Include(s => s.Stores).Include(c => c.PaymentCards).FirstOrDefault();

                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }


        }

        [HttpGet("[action]/{username}/{password}")]
        public TokenDTO LoginCredential(string username, string password)
        {



            var loginOfUser = _context.Logins.Where(c => c.Username == username && c.Password == password).FirstOrDefault();

            if (loginOfUser != null)
            {

                if (_context.Users.Count() > 0)
                {

                    var user = _context.Users.Where(u => u.LoginId == loginOfUser.LoginId).Include(s => s.Stores).Include(c => c.PaymentCards).FirstOrDefault();

                    var isAdministrator = _context.Stores.Any(s => s.UserId == user.UserId);
                    string tokenString;
                    TokenDTO tokenDTO;

                    if (isAdministrator)
                    {

                        tokenString = GenerateJWTTokenWithRole(user, "Admin");

                        var obj = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
                        tokenDTO = new TokenDTO()
                        {
                            Token = tokenString,
                            UserDetail = user,
                            Exp = DateTime.Now.AddMinutes(30)

                        };

                        return tokenDTO;

                    }

                    var isEmployee = _context.Employees.Any(emp => emp.EmployeeUser.UserId == user.UserId);

                    if (isEmployee)
                    {

                        tokenString = GenerateJWTTokenWithRole(user, "Employee");
                        tokenDTO = new TokenDTO()
                        {
                            Token = tokenString,
                            UserDetail = user,
                            Exp = DateTime.Now.AddMinutes(30)

                        };
                        return tokenDTO;
                    }

                    tokenString = GenerateJWTToken(user);
                    tokenDTO = new TokenDTO()
                    {
                        Token = tokenString,
                        UserDetail = user,
                        Exp = DateTime.Now.AddMinutes(30)
                    };
                    return tokenDTO;

                }
                else
                {
                    return null;
                }
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


                try
                {

                    SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587); //587

                    smtpClient.UseDefaultCredentials = false;

                    smtpClient.Credentials = new System.Net.NetworkCredential("quickorderpr@outlook.com", "jp199494tt");

                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtpClient.EnableSsl = true;

                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("quickorderpr@outlook.com", "Quick Order");

                    mail.To.Add(new MailAddress(result.Email, result.Name));

                    mail.IsBodyHtml = true;

                    mail.Subject = "Quick Order Forgot Password Code";

                    mail.Body = "<b>Forgot Secret Code:</b>" + forgotpassword.Code;


                    smtpClient.Send(mail);

                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    throw;

                }





                //var senderEmail = new MailAddress("est.juanpablotorres@gmail.com", "Quick Order");
                //var receiverEmail = new MailAddress(result.Email, result.Name);

                //var sub = "Quick Order Forgot Password Code";
                //var body = "<b>Forgot Secret Code:</b>" + forgotpassword.Code;
                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential("est.juanpablotorres@gmail.com", "jp84704tt")
                //};
                //using (var mess = new MailMessage(senderEmail, receiverEmail)
                //{
                //    IsBodyHtml = true,
                //    Subject = sub,
                //    Body = body
                //})
                //{
                //    smtp.Send(mess);
                //}

                //return true;

            }
            else
            {
                return false;
            }
        }


        bool SendValidateEmailCode(EmailValidation emailValidation)
        {



            try
            {

                SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587); //587
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("quickorderpr@outlook.com", "jp199494tt");

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("quickorderpr@outlook.com", "Quick Order");
                mail.To.Add(new MailAddress(emailValidation.Email, emailValidation.UserId.ToString()));
                mail.IsBodyHtml = true;
                mail.Subject = "Validation Email Code";
                mail.Body = "<div><b>Code:</b>" + emailValidation.ValidationCode;

                smtpClient.Send(mail);

                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw;

            }


            //var senderEmail = new MailAddress("est.juanpablotorres@gmail.com", "Quick Order");
            //var receiverEmail = new MailAddress(emailValidation.Email, emailValidation.UserId.ToString());

            //var sub = "Validation Email Code";
            //var body = "<div><b>Code:</b>" + emailValidation.ValidationCode;
            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential("est.juanpablotorres@gmail.com", "jp84704tt")
            //};
            //using (var mess = new MailMessage(senderEmail, receiverEmail)
            //{
            //    IsBodyHtml = true,
            //    Subject = sub,
            //    Body = body
            //})
            //{
            //    smtp.Send(mess);
            //    return true;
            //}


        }

        [HttpGet("[action]/{code}")]
        public bool ConfirmCode(string code)
        {
            var result = _context.ForgotPasswords.Where(u => u.Code == code).FirstOrDefault();

            var userInfo = _context.Users.Where(u => u.Email == result.Email).Include(l => l.UserLogin).FirstOrDefault();

            if (result != null)
            {





                try
                {

                    SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587); //587

                    smtpClient.UseDefaultCredentials = false;

                    smtpClient.Credentials = new System.Net.NetworkCredential("quickorderpr@outlook.com", "jp199494tt");

                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtpClient.EnableSsl = true;

                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("quickorderpr@outlook.com", "Quick Order");

                    mail.To.Add(new MailAddress(userInfo.Email, userInfo.UserId.ToString()));

                    mail.IsBodyHtml = true;

                    mail.Subject = "Loging Credials";

                    mail.Body = "<div>Username" + userInfo.UserLogin.Username + "</div>" + "<div>Password" + userInfo.UserLogin.Password + "</div>";

                    smtpClient.Send(mail);

                    _context.ForgotPasswords.Remove(result);

                    _context.SaveChangesAsync();

                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    throw;

                }


                //var senderEmail = new MailAddress("est.juanpablotorres@gmail.com", "Quick Order");
                //var receiverEmail = new MailAddress(userInfo.Email, userInfo.Name);

                //var sub = "Loging Credials";
                //var body = "<div>Username" + userInfo.UserLogin.Username + "</div>" + "<div>Password" + userInfo.UserLogin.Password + "</div>";
                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential("est.juanpablotorres@gmail.com", "jp84704tt")
                //};
                //using (var mess = new MailMessage(senderEmail, receiverEmail)
                //{
                //    IsBodyHtml = true,
                //    Subject = sub,
                //    Body = body
                //})
                //{
                //    smtp.Send(mess);
                //}





            }
            else
            {
                return false;
            }
        }

        [HttpGet("[action]/{username}/{password}")]
        public async Task<bool> CheckIfUsernameAndPasswordExist(string username, string password)
        {
            return await _context.Logins.AnyAsync(l => l.Username == username && l.Password == password);
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        private IActionResult BuildToken(User userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.UserId.ToString()),
                new Claim("miValor", "Lo que yo quiera"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Llave_super_secreta"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            });

        }
    }
}
