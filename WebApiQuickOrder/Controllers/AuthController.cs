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
		readonly UserManager<User> userManager;
		readonly IEmailSender emailSender;
		public AuthController(UserManager<User> userManager, IEmailSender emailSender)
		{
			this.userManager = userManager;
			this.emailSender = emailSender;
		}
		//[HttpPost("[action]")]
		//[ValidateAntiForgeryToken]
		//[AllowAnonymous]
		//public async Task<IActionResult> Registration([FromBody]User user)
		//{
		//	string code = string.Empty;
		//	IdentityResult identityResult;

		//	if (this.ValidForRegistration(user))
		//	{
		//		identityResult = await this.userManager.CreateAsync(user, user.UserLogin.Password);
		//		if (identityResult.Succeeded)
		//		{
		//			code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
		//			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
		//			await this.emailSender.SendEmailAsync(user.Email, "Confirm your email",
		//	   $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode("")}'>clicking here</a>.");
		//		}
		//	}
		//}
		private bool ValidForRegistration(User user)
		{
			return !(string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.UserLogin.Password));
		}
	}
}
