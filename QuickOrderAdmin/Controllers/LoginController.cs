using Library.Models;
using Library.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using QuickOrderAdmin.Models;
using QuickOrderAdmin.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOrderAdmin.Controllers
{
    public class LoginController : Controller
    {
        IUserDataStore userDataStore;
        IUserConnectedDataStore userConnectedDataStore;

      
       
        public LoginController(IUserDataStore userData,IUserConnectedDataStore userConnectedData)
        {
            userDataStore = userData;
            userConnectedDataStore = userConnectedData;
            //UsersConnected = usersConnected;
            //ComunicationService = comunication;
            //ComunicationService = new ComunicationService();

            LogUser.ComunicationService = new ComunicationService();
        }

        //Sign In Action
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (LoginInfoNotNullOrEmpty(loginViewModel))
            {
                var result = userDataStore.CheckUserCredential(loginViewModel.Username, loginViewModel.Password);

                LogUser.Token = userDataStore.LoginCredential(loginViewModel.Username, loginViewModel.Password);

                if (result != null)
                {

                    LogUser.ComunicationService = new ComunicationService();


                    Task.Run(async() =>
                    {
                    
                    await LogUser.ComunicationService.Connect();
                    
                    }).Wait();

                    LogUser.LoginUser = result;


                    LogUser.UsersConnected = new UsersConnected()
                    {
                        HubConnectionID = LogUser.ComunicationService.hubConnection.ConnectionId,
                        UserID = result.UserId,
                        IsDisable = false,
                        ConnecteDate = DateTime.Now
                    };


                    bool isConnectionWasModify = await userConnectedDataStore.ModifyOldConnections(LogUser.UsersConnected);

                    var hub_connection_result = await userConnectedDataStore.AddItemAsync(LogUser.UsersConnected);
                   
                     
                    var stores = new List<Store>(result.Stores.Where(s=>s.IsDisable == false));


                    LogUser.LoginUser.Stores = stores;
                    if (stores.Count() > 0)
                    {

                        SelectedStore.CurrentStore = stores[0];

                    }
                    else
                    {
                        return RedirectToAction("RegisterControl","Store");
                    }

                    return RedirectToAction("HomeStore", "Store", new { StoreId = SelectedStore.CurrentStore.StoreId });
                   
                }
                else
                {
                    ViewBag.ErrorMsg = "Error";
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

        public async Task<IActionResult> SignOut()
        {

            string hubconnectionId = LogUser.ComunicationService.hubConnection.ConnectionId;
            await LogUser.ComunicationService.Disconnect();


            LogUser.UsersConnected.IsDisable = true;
            bool result = await userConnectedDataStore.UpdateItemAsync(LogUser.UsersConnected);

            if (result)
            {

            return RedirectToAction("Index", new { loginViewModel = new LoginViewModel() });
            }
            else
            {
                return RedirectToAction("HomeStore", "Store", new { StoreId = SelectedStore.CurrentStore.StoreId });
            }

        }
    }
}