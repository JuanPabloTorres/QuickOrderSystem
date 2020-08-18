using Library.DTO;
using Library.Models;
using Library.Services;
using Library.Services.Interface;
using Library.SolutionUtilities.ValidatorComponents;
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
        ICardDataStore cardDataStore;
        IStripeServiceDS StripeServiceDS;

        static StripePaymentCardResult StripePaymentCardResult;
        static PaymentCard PaymentCard;
        public LicenseController(IStoreLicenseDataStore storeLicenseData, IUserDataStore userData, IStripeServiceDS stripeServiceDS, ICardDataStore cardDataStore)
        {
            storeLicenseDataStore = storeLicenseData;
            userDataStore = userData;
            this.StripeServiceDS = stripeServiceDS;
            this.cardDataStore = cardDataStore;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetLicencePro(CardViewModel cardViewModel)
        {

            var checkingCard = new PaymentCard()
            {
                CardNumber = cardViewModel.cardNumber,
                Cvc = cardViewModel.CVc,
                HolderName = cardViewModel.Holdername,
                Month = cardViewModel.MM,
                Year = cardViewModel.YY,

            };

            Validator creditcardValidator = new Validator(ValidatorRules.CreditCardCheckerRule(checkingCard));

            if (!creditcardValidator.HasError)
            {

                try
                {
                    //Obtenemos informacion validada de la tarjeta del nuevo usuario administrador.
                    PaymentCard = new PaymentCard()
                    {
                        CardNumber = cardViewModel.cardNumber,
                        Cvc = cardViewModel.CVc,
                        HolderName = cardViewModel.Holdername,
                        Month = cardViewModel.MM,
                        Year = cardViewModel.YY


                    };


                    //Creamos un el token para la tarjeta
                    StripePaymentCardResult = await StripeServiceDS.CreateCardToken(PaymentCard);

                    //Verficamos si hay algun error en la tarjeta
                    if (!StripePaymentCardResult.HasError)
                    {

                        return RedirectToAction("UserRegistration");
                    }
                    else
                    {
                        ViewBag.CardError = "Some error have ocurred." + "\r Check:" + StripePaymentCardResult.ErrorMsg;

                        return View();
                    }
                }
                catch (Exception)
                {

                    throw;
                }


            }
            else
            {
                if (creditcardValidator.ErrorMessage != "Values are empty.")
                {

                    ViewBag.CardError = "Some error have ocurred." + "\r Check:" + creditcardValidator.ErrorMessage;
                }
                return View();
            }
        }



        bool UserInfoNotNullOrEmpty(UserViewModel userVm)
        {
            if (!string.IsNullOrEmpty(userVm.Name) && !string.IsNullOrEmpty(userVm.Email) && !string.IsNullOrEmpty(userVm.Password) && !string.IsNullOrEmpty(userVm.ConfirmPassword)
               && !string.IsNullOrEmpty(userVm.StoreLicence.ToString()) && !string.IsNullOrEmpty(userVm.Phone) && !string.IsNullOrEmpty(userVm.Address))
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

            if (userVm.Password == userVm.ConfirmPassword)
            {
                //Las credenciales de entrada del usuario
                var userLogin = new Login()
                {
                    LoginId = Guid.NewGuid(),
                    Password = userVm.Password,
                    Username = userVm.Username
                };

                //Creamos el usuario
                var newUser = new User()
                {
                    UserId = Guid.NewGuid(),
                    Email = userVm.Email,
                    LoginId = userLogin.LoginId,
                    Name = userVm.Name,
                    UserLogin = userLogin,
                    Address = userVm.Address
                };


                //Obtenemos datos de empleados necesarios para el registro en stripe
                var optionsCustomers = new UserDTO
                {

                    Name = userVm.Name,
                    Email = userVm.Email,
                    Phone = userVm.Phone,
                    Address = userVm.Address,

                };


                //Creamos un nuevo usuario en stripe
                var customerToken = await StripeServiceDS.CreateStripeCustomer(optionsCustomers);

                //Le  insertamos la tarjeta al nuevo usuario de stripe
                var cardservicetokenId = await StripeServiceDS.InsertStripeCardToCustomer(PaymentCard, customerToken);

                //Creamos la subcripcion al usuario

                var subcriptiontoken = await StripeServiceDS.CreateACustomerSubcription(customerToken);

                //Verificamos que la informacion este correcta

                if (!string.IsNullOrEmpty(subcriptiontoken))
                {
                    //Le damos el id del nuevo customer de la cuenta de stripe.
                    newUser.StripeUserId = customerToken;

                    //Lo agregamos a la base de datos.
                    var addedUser = await userDataStore.AddItemAsync(newUser);

                    //Verificamos si el usuario se inserto a nuestra base de datos
                    if (addedUser)
                    {

                    //Verificamos si el token de la tarjeta insertada es correcta.
                    if (!string.IsNullOrEmpty(cardservicetokenId))
                    {
                        //Agregamos la tarjeta a nuestra base de datos.
                        var cardadded = cardDataStore.AddItemAsync(new PaymentCard()
                        {
                            UserId = newUser.UserId,
                            StripeCardId = cardservicetokenId, 
                            CardNumber = PaymentCard.CardNumber, 
                            Cvc= PaymentCard.Cvc,
                            Month = PaymentCard.Month, 
                            Year = PaymentCard.Year, 
                            HolderName = PaymentCard.HolderName, PaymentCardId = Guid.NewGuid() 
                        });
                    }

                    //Creamos el lincense 
                        var newStoreLicense = new StoreLicense()
                        {
                            LicenseId = Guid.NewGuid(),
                            StartDate = DateTime.Today,
                            LicenseHolderUserId = newUser.UserId
                            
                        };

                        //Lo insertamos a nuestra base de datos
                        var storelicenceresult = await storeLicenseDataStore.AddItemAsync(newStoreLicense);

                        //Verificamos el resultado
                        if (storelicenceresult)
                        {
                            //Enviamos el email con el codio de la nueva licensia.
                            SendStoreLicenceEmailCode(newUser.Email, newStoreLicense.LicenseId.ToString());
                        }
                        //Verificamos que los credenciales esten correctos.
                        var resultCredentials = userDataStore.CheckUserCredential(userLogin.Username, userLogin.Password);

                        //Validamos que el resultado no sea vacio
                        if (resultCredentials != null)
                        {
                            //Le damos los credenciales al LogUser
                            LogUser.LoginUser = resultCredentials;
                        }

                        //Luego de todo completado de manera correcta nos vamos a registrar una tienda.
                        return RedirectToAction("RegisterStore", "Store");
                    }
                }
                else
                {
                    return View();
                }


            }
            else
            {
                ViewBag.ErrorMsg = "Las Credenciales de password y confirm password son distintas.";
                return View();
            }
            }


            return View();

        }

        void SendStoreLicenceEmailCode(string email, string licenseId)
        {
            var senderEmail = new MailAddress("est.juanpablotorres@gmail.com", "Quick Order");
            var receiverEmail = new MailAddress(email, "Juan Torres");

            var sub = "Quick Order Lincense Code";
            var body = "<span>License Code:</span>" + licenseId;
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
    }
}