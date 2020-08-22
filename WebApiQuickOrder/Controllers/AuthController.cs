using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using WebApiQuickOrder.Models.Email;

namespace WebApiQuickOrder.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		readonly IEmailSender emailSender;
		public AuthController(IEmailSender emailSender)
		{
			this.emailSender = emailSender;
		}
		[HttpPost("[action]")]
		//[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public async Task<IActionResult> Registration(/*[FromBody] User user*/)
		{
			string code = string.Empty;

			try
			{
				code = this.GenerateVerificationCode();
				await this.emailSender.SendEmailAsync("cs.torresricardo@gmail.com", "Quick Order Email Verification",
		   $"Please confirm your account by entering this code: " + code);
				return Ok();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
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
