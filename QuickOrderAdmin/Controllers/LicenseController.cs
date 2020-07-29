using Library.Models;
using Library.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using QuickOrderAdmin.Models;
using QuickOrderAdmin.Utilities;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QuickOrderAdmin.Controllers
{
    public class LicenseController : Controller
    {
        IStoreLicenseDataStore storeLicenseDataStore;
        IUserDataStore userDataStore;
        public LicenseController(IStoreLicenseDataStore storeLicenseData, IUserDataStore userData)
        {
            storeLicenseDataStore = storeLicenseData;
            userDataStore = userData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetLicencePro(string username, string cardNumber, int MM, int YY, int CVV, string email)
        {
            if (CreditCardNotNullOrEmpty(username, cardNumber, MM, YY, CVV))
            {
                bool cardconfirm = true;

                if (cardconfirm)
                {

                    var newStoreLicense = new StoreLicense()
                    {
                        LicenseId = Guid.NewGuid(),
                        StartDate = DateTime.Today
                    };

                    var result = await storeLicenseDataStore.AddItemAsync(newStoreLicense);

                    if (result)
                    {
                        var senderEmail = new MailAddress("est.juanpablotorres@gmail.com", "Quick Order");
                        var receiverEmail = new MailAddress(email, "Juan Torres");

                        var sub = "Quick Order Lincense Code";
                        var body = "<span>License Code:</span>" + newStoreLicense.LicenseId;
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
                    }


                    return RedirectToAction("UserRegistration");
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

        bool CreditCardNotNullOrEmpty(string username, string cardNumber, int MM, int YY, int CVV)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(cardNumber) && !string.IsNullOrEmpty(MM.ToString()) && !string.IsNullOrEmpty(YY.ToString())
               && !string.IsNullOrEmpty(CVV.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool UserInfoNotNullOrEmpty(UserViewModel userVm)
        {
            if (!string.IsNullOrEmpty(userVm.Name) && !string.IsNullOrEmpty(userVm.Email) && !string.IsNullOrEmpty(userVm.Password) && !string.IsNullOrEmpty(userVm.ConfirmPassword)
               && !string.IsNullOrEmpty(userVm.StoreLicence.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<IActionResult> UserRegistration(UserViewModel userVm)
        {
            if (UserInfoNotNullOrEmpty(userVm))
            {
                //var LicenseValid =  storeLicenseDataStore.StoreLicenseExists(userVm.StoreLicence);


                var userLogin = new Login()
                {
                    LoginId = Guid.NewGuid(),
                    Password = userVm.Password,
                    Username = userVm.Username
                };

                var newUser = new User()
                {
                    UserId = Guid.NewGuid(),
                    Email = userVm.Email,
                    LoginId = userLogin.LoginId,
                    Name = userVm.Name,
                    UserLogin = userLogin,
                };

                var addedUser = await userDataStore.AddItemAsync(newUser);

                if (addedUser)
                {

                    var result = userDataStore.CheckUserCredential(userLogin.Username, userLogin.Password);

                    if (result != null)
                    {
                        LogUser.LoginUser = result;
                    }

                    return RedirectToAction("RegisterStore", "Store");
                }

                //else
                //{
                //    ViewBag.LicenseError = "License is not valid.";
                //    return View();
                //}
            }

            return View();
        }
    }
}