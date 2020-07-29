using Library.Models;
using Library.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using QuickOrderAdmin.Models;
using QuickOrderAdmin.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace QuickOrderAdmin.Controllers
{
    public class LoginController : Controller
    {
        IUserDataStore userDataStore;
        public LoginController(IUserDataStore userData)
        {
            userDataStore = userData;
        }

        //Sign In Action
        public IActionResult Index(LoginViewModel loginViewModel)
        {
            if (LoginInfoNotNullOrEmpty(loginViewModel))
            {
                var result = userDataStore.CheckUserCredential(loginViewModel.Username, loginViewModel.Password);

                if (result != null)
                {
                    LogUser.LoginUser = result;

                    var stores = new List<Store>(result.Stores);

                    if (stores.Count() > 0)
                    {

                        SelectedStore.CurrentStore = stores[0];

                    }
                    else
                    {
                        return RedirectToAction("RegisterStore", "Store");
                    }

                    return RedirectToAction("HomeStore", "Store", new { StoreId = SelectedStore.CurrentStore.StoreId });
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }


            }
            else
            {
                return View();
            }


        }

        bool LoginInfoNotNullOrEmpty(LoginViewModel loginViewModel)
        {
            if (!string.IsNullOrEmpty(loginViewModel.Username) && !string.IsNullOrEmpty(loginViewModel.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IActionResult ForgotPassword()
        {

            return View();
        }

        public IActionResult SendCode(ForgotPassword forgotPassword)
        {
            if (!string.IsNullOrEmpty(forgotPassword.Email))
            {
                var result = userDataStore.ForgotCodeSend(forgotPassword.Email);

                ViewBag.CodeSended = "Code was sended to user email check the email.";
                return RedirectToAction("ForgotPassword", "Login");
            }
            else
            {

                return RedirectToAction("ForgotPassword", "Login");
            }

        }

        public IActionResult ConfirmCode(ForgotPassword forgotPassword)
        {
            if (!string.IsNullOrEmpty(forgotPassword.Code))
            {
                var result = userDataStore.ConfirmCode(forgotPassword.Code);

                if (result)
                {

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewBag.CodeError = "Code is incorrect try agin.";
                    return RedirectToAction("ForgotPassword", "Login");
                }
            }
            else
            {

                return RedirectToAction("ForgotPassword", "Login");
            }

        }

        public IActionResult SignOut()
        {
            return RedirectToAction("Index", new { loginViewModel = new LoginViewModel() });
        }
    }
}