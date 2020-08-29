using Library.DTO;
using Library.Factories;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiQuickOrder.Context;
using WebApiQuickOrder.Models.Email;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmailSender emailSender;
        private readonly QOContext _context;
        private readonly IAuthCodeFactory _authCodeFactory;

        public AuthController(IEmailSender emailSender, QOContext context, IAuthCodeFactory authCodeFactory)
        {
            this.emailSender = emailSender;
            this._context = context;
            this._authCodeFactory = authCodeFactory;
        }

        [HttpPost("[action]")]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmailVerificationCode([FromQuery] string email)
        {
            
            string code = string.Empty;
            bool codeExists = true;
            try
            {
                do
                {
                    code = this.GenerateVerificationCode();
                    codeExists = _context.AuthCodes.FirstOrDefault(ac => ac.Code == code) is object;
                } while (codeExists);
                _context.AuthCodes.Add(_authCodeFactory.CreateAuthCode(code, email));
                _context.SaveChanges();
                await this.emailSender.SendEmailAsync(email, "Quick Order Email Verification",
       $"Please confirm your account by entering this code: " + code);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("[action]")]
        public ActionResult<ResponseDto> VerifyAuthCode([FromQuery]string code, [FromQuery]string email)
        {

            const string successMessage = "Your account has been verrifed succesfulle.";
            const string expiredMessage = "This code has expired...";
            const string notFoundMessage = "Code does not exist.";

            ResponseDto? response;
            ActionResult<ResponseDto> result;
            try
            {
                if (_context.AuthCodes.FirstOrDefault(x => x.Code == code && x.Email == email) is AuthCode authCode)
                {
                    if (authCode.EndAt <= DateTime.Now && authCode.IsAlive)
                    {
                        authCode.IsAlive = false;
                        _context.Entry(authCode).CurrentValues.SetValues(authCode);
                        _context.SaveChanges();
                        result = Ok(response = new ResponseDto { HasErrors = false, TextMessage = successMessage });
                    }
                    else
                    {
                        result = NotFound(response = new ResponseDto { HasErrors = true, TextMessage = expiredMessage });
                    }
                }
                else
                {
                    result = NotFound(response = new ResponseDto { HasErrors = true, TextMessage = notFoundMessage });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random((int)TimeSpan.FromSeconds(DateTime.Now.Millisecond).TotalSeconds);
            return random.Next(100000, 999999).ToString();
        }

        private bool ValidForRegistration(User user)
        {
            return !(string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.UserLogin.Password));
        }
    }
}