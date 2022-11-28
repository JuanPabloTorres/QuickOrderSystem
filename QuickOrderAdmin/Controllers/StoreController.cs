using Library.Helpers;
using Library.Models;
using Library.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickOrderAdmin.Models;
using QuickOrderAdmin.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOrderAdmin.Controllers
{
    public class StoreController : Controller
    {
       
        #region DataStores
        IStoreDataStore StoreDataStore;
        IUserDataStore userDataStore;
        IProductDataStore productDataStore;
        IStoreLicenseDataStore storeLicenseDataStore;
        IOrderDataStore orderDataStore;
        IEmployeeDataStore employeeDataStore;
        IRequestDataStore RequestDataStore;
        IUserConnectedDataStore userConnectedDataStore;
        ICardDataStore cardDataStore;
        IStripeServiceDS stripeServiceDS;
        ISubcriptionDataStore subcriptionDataStore;

        static byte[] currentproductimg;

        static Product oldProduct;
        #endregion

        public StoreController(IStoreDataStore storeData, IUserDataStore userData, IProductDataStore productData, IStoreLicenseDataStore storeLicenseData, IOrderDataStore orderData, IEmployeeDataStore employeeData, IRequestDataStore requestData, IUserConnectedDataStore userConnectedDataStore ,ICardDataStore cardDataStore, IStripeServiceDS stripeServiceDS, ISubcriptionDataStore subcriptionDataStore)
        {
            userDataStore = userData;
            StoreDataStore = storeData;
            productDataStore = productData;
            storeLicenseDataStore = storeLicenseData;
            orderDataStore = orderData;
            employeeDataStore = employeeData;
            RequestDataStore = requestData;
            this.userConnectedDataStore = userConnectedDataStore;
           
            this.cardDataStore = cardDataStore;
            this.stripeServiceDS = stripeServiceDS;
            this.subcriptionDataStore = subcriptionDataStore;

            //UsersConnected = usersConnected;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FirstRegisterStore(RegisterStoreViewModel registerStoreViewModel)
        {
            if (!(registerStoreViewModel.StoreLicence == Guid.Empty))
            {
                var LicenseValid = storeLicenseDataStore.StoreLicenseExists(registerStoreViewModel.StoreLicence);

                if (LicenseValid)
                {

                    if (registerStoreViewModel.File != null)
                    {
                        var ImgToBty = ConvertToBytes(registerStoreViewModel.File);
                        var StoreId = Guid.NewGuid();
                        var listWorkHour = new List<WorkHour>();

                        var MondayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Monday.ToString(),
                            OpenTime = registerStoreViewModel.MOpenTime,
                            CloseTime = registerStoreViewModel.MCloseTime

                        };
                        var TuesdayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Tuesday.ToString(),
                            OpenTime = registerStoreViewModel.TOpenTime,
                            CloseTime = registerStoreViewModel.TCloseTime
                        };
                        var WednesdayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Wednesday.ToString(),
                            OpenTime = registerStoreViewModel.WOpenTime,
                            CloseTime = registerStoreViewModel.WCloseTime
                        };
                        var ThuerdayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Thursday.ToString(),
                            OpenTime = registerStoreViewModel.ThOpenTime,
                            CloseTime = registerStoreViewModel.ThCloseTime
                        };
                        var FridayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Friday.ToString(),
                            OpenTime = registerStoreViewModel.FOpenTime,
                            CloseTime = registerStoreViewModel.FCloseTime
                        };
                        var SaturdayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Saturday.ToString(),
                            OpenTime = registerStoreViewModel.SOpenTime,
                            CloseTime = registerStoreViewModel.SCloseTime
                        };
                        var SundayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Sunday.ToString(),
                            OpenTime = registerStoreViewModel.SuOpenTime,
                            CloseTime = registerStoreViewModel.SuCloseTime
                        };

                        listWorkHour.Add(MondayWH);
                        listWorkHour.Add(TuesdayWH);
                        listWorkHour.Add(WednesdayWH);
                        listWorkHour.Add(ThuerdayWH);
                        listWorkHour.Add(FridayWH);
                        listWorkHour.Add(SaturdayWH);
                        listWorkHour.Add(SundayWH);

                        var newStore = new Store()
                        {
                            ID = StoreId,
                            /////////////////////////CHANGE Dev///////////////////////
                            StoreName = registerStoreViewModel.StoreName,
                            WorkHours = listWorkHour,
                            StoreImage = ImgToBty,
                            StoreRegisterLicenseId = registerStoreViewModel.StoreLicence,
                            UserId = LogUser.LoginUser.ID,
                            StoreType = registerStoreViewModel.SelectedStoreType,
                            StoreDescription = registerStoreViewModel.StoreDescription,
                            SKKey = registerStoreViewModel.StripeSecretKey,
                            PBKey = registerStoreViewModel.StripePublicKey
                        };

                        var newStoreAddedResult = await StoreDataStore.AddItemAsync(newStore);

                        var result = userDataStore.CheckUserCredential(LogUser.LoginUser.UserLogin.Username, LogUser.LoginUser.UserLogin.Password);
                        LogUser.LoginUser = result;

                        if (true)
                        {
                            return RedirectToAction("HomeStore", new { StoreId = newStore.ID });
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
                else
                {
                    ViewBag.LicenseError = "License is not valid.";
                    return View();
                }
            }
            else
            {
                return View();
            }


        }
        public async Task<IActionResult> RegisterStore(RegisterStoreViewModel registerStoreViewModel)
        {

            if (!(registerStoreViewModel.StoreLicence == Guid.Empty))
            {

                var LicenseValid = storeLicenseDataStore.StoreLicenseExists(registerStoreViewModel.StoreLicence);

                if (LicenseValid)
                {

                    var licenseIsInUsed = await storeLicenseDataStore.IsLicenseInUsed(registerStoreViewModel.StoreLicence.ToString());

                    if (!licenseIsInUsed)
                    {

                        if (registerStoreViewModel.File != null)
                        {
                            var ImgToBty = ConvertToBytes(registerStoreViewModel.File);
                            var StoreId = Guid.NewGuid();
                            var listWorkHour = new List<WorkHour>();

                            var MondayWH = new WorkHour()
                            {
                                ID = Guid.NewGuid(),
                                Day = DayOfWeek.Monday.ToString(),
                                OpenTime = registerStoreViewModel.MOpenTime,
                                CloseTime = registerStoreViewModel.MCloseTime

                            };
                            var TuesdayWH = new WorkHour()
                            {
                                ID = Guid.NewGuid(),
                                Day = DayOfWeek.Tuesday.ToString(),
                                OpenTime = registerStoreViewModel.TOpenTime,
                                CloseTime = registerStoreViewModel.TCloseTime
                            };
                            var WednesdayWH = new WorkHour()
                            {
                                ID = Guid.NewGuid(),
                                Day = DayOfWeek.Wednesday.ToString(),
                                OpenTime = registerStoreViewModel.WOpenTime,
                                CloseTime = registerStoreViewModel.WCloseTime
                            };
                            var ThuerdayWH = new WorkHour()
                            {
                                ID = Guid.NewGuid(),
                                Day = DayOfWeek.Thursday.ToString(),
                                OpenTime = registerStoreViewModel.ThOpenTime,
                                CloseTime = registerStoreViewModel.ThCloseTime
                            };
                            var FridayWH = new WorkHour()
                            {
                                ID = Guid.NewGuid(),
                                Day = DayOfWeek.Friday.ToString(),
                                OpenTime = registerStoreViewModel.FOpenTime,
                                CloseTime = registerStoreViewModel.FCloseTime
                            };
                            var SaturdayWH = new WorkHour()
                            {
                                ID = Guid.NewGuid(),
                                Day = DayOfWeek.Saturday.ToString(),
                                OpenTime = registerStoreViewModel.SOpenTime,
                                CloseTime = registerStoreViewModel.SCloseTime
                            };
                            var SundayWH = new WorkHour()
                            {
                                ID = Guid.NewGuid(),
                                Day = DayOfWeek.Sunday.ToString(),
                                OpenTime = registerStoreViewModel.SuOpenTime,
                                CloseTime = registerStoreViewModel.SuCloseTime
                            };

                            listWorkHour.Add(MondayWH);
                            listWorkHour.Add(TuesdayWH);
                            listWorkHour.Add(WednesdayWH);
                            listWorkHour.Add(ThuerdayWH);
                            listWorkHour.Add(FridayWH);
                            listWorkHour.Add(SaturdayWH);
                            listWorkHour.Add(SundayWH);

                            var newStore = new Store()
                            {
                                ID = StoreId,
                                /////////////////////////CHANGE Dev///////////////////////
                                StoreName = registerStoreViewModel.StoreName,
                                WorkHours = listWorkHour,
                                StoreImage = ImgToBty,
                                StoreRegisterLicenseId = registerStoreViewModel.StoreLicence,
                                UserId = LogUser.LoginUser.ID,
                                StoreType = registerStoreViewModel.SelectedStoreType,
                                StoreDescription = registerStoreViewModel.StoreDescription,
                                SKKey = registerStoreViewModel.StripeSecretKey,
                                PBKey = registerStoreViewModel.StripePublicKey,
                                StoreLicenceId = registerStoreViewModel.StoreLicence
                            };

                            var newStoreAddedResult = await StoreDataStore.AddItemAsync(newStore);

                            var result = userDataStore.CheckUserCredential(LogUser.LoginUser.UserLogin.Username, LogUser.LoginUser.UserLogin.Password);

                            LogUser.LoginUser = result;

                            LogUser.LoginUser.Stores = LogUser.LoginUser.Stores.Where(s => s.IsDisable == false).ToList();

                            if (LogUser.Token == null)
                            {
                                LogUser.UsersConnected = new UsersConnected()
                                {
                                    HubConnectionID = LogUser.ComunicationService.hubConnection.ConnectionId,
                                    UserID = result.ID
                                };

                                var hub_connection_result = await userConnectedDataStore.AddItemAsync(LogUser.UsersConnected);

                                var _apiResponse =await userDataStore.LoginCredential(LogUser.LoginUser.UserLogin.Username, LogUser.LoginUser.UserLogin.Password);

                                if (_apiResponse.IsValid)
                                {
                                    LogUser.Token = _apiResponse.Data;
                                }

                               
                            }

                            if (true)
                            {

                                var licenseUpdated = await storeLicenseDataStore.UpdateLicenceInCode(registerStoreViewModel.StoreLicence);

                                if (licenseUpdated)
                                {

                                return RedirectToAction("HomeStore", new { StoreId = newStore.ID });
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
                        else
                        {
                            return View();

                        }
                    }
                    else
                    {
                        ViewBag.LicenseError = "License is in used.";
                        return View();
                    }

                }
                else
                {
                    ViewBag.LicenseError = "License is not valid.";
                    return View();
                }
            }
            else
            {
                return View();
            }


        }

        public async Task<IActionResult> RegisterStoreAdmin(RegisterStoreViewModel registerStoreViewModel)
        {
            if (!(registerStoreViewModel.StoreLicence == Guid.Empty))
            {
                var IsLicenseValid = storeLicenseDataStore.StoreLicenseExists(registerStoreViewModel.StoreLicence);

                if (IsLicenseValid)
                {

                    if (registerStoreViewModel.File != null)
                    {
                        var ImgToBty = ConvertToBytes(registerStoreViewModel.File);
                        var StoreId = Guid.NewGuid();
                        var listWorkHour = new List<WorkHour>();

                        var MondayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Monday.ToString(),
                            OpenTime = registerStoreViewModel.MOpenTime,
                            CloseTime = registerStoreViewModel.MCloseTime

                        };
                        var TuesdayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Tuesday.ToString(),
                            OpenTime = registerStoreViewModel.TOpenTime,
                            CloseTime = registerStoreViewModel.TCloseTime
                        };
                        var WednesdayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Wednesday.ToString(),
                            OpenTime = registerStoreViewModel.WOpenTime,
                            CloseTime = registerStoreViewModel.WCloseTime
                        };
                        var ThuerdayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Thursday.ToString(),
                            OpenTime = registerStoreViewModel.ThOpenTime,
                            CloseTime = registerStoreViewModel.ThCloseTime
                        };
                        var FridayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Friday.ToString(),
                            OpenTime = registerStoreViewModel.FOpenTime,
                            CloseTime = registerStoreViewModel.FCloseTime
                        };
                        var SaturdayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Saturday.ToString(),
                            OpenTime = registerStoreViewModel.SOpenTime,
                            CloseTime = registerStoreViewModel.SCloseTime
                        };
                        var SundayWH = new WorkHour()
                        {
                            ID = Guid.NewGuid(),
                            Day = DayOfWeek.Sunday.ToString(),
                            OpenTime = registerStoreViewModel.SuOpenTime,
                            CloseTime = registerStoreViewModel.SuCloseTime
                        };

                        listWorkHour.Add(MondayWH);
                        listWorkHour.Add(TuesdayWH);
                        listWorkHour.Add(WednesdayWH);
                        listWorkHour.Add(ThuerdayWH);
                        listWorkHour.Add(FridayWH);
                        listWorkHour.Add(SaturdayWH);
                        listWorkHour.Add(SundayWH);

                        var newStore = new Store()
                        {
                            ID = StoreId,
                            StoreName = registerStoreViewModel.StoreName,
                            WorkHours = listWorkHour,
                            StoreImage = ImgToBty,
                            StoreRegisterLicenseId = registerStoreViewModel.StoreLicence,
                            UserId = LogUser.LoginUser.ID,
                            StoreType = registerStoreViewModel.SelectedStoreType,
                            StoreDescription = registerStoreViewModel.StoreDescription,
                            SKKey = registerStoreViewModel.StripeSecretKey,
                            PBKey = registerStoreViewModel.StripePublicKey,
                            StoreLicenceId = registerStoreViewModel.StoreLicence

                        };

                        var newStoreAddedResult = await StoreDataStore.AddItemAsync(newStore);

                        if (LogUser.Token == null)
                        {
                            //LogUser.Token =await userDataStore.LoginCredential(LogUser.LoginUser.UserLogin.Username, LogUser.LoginUser.UserLogin.Password);

                            LogUser.Token = null;
                        }

                        if (true)
                        {
                            LogUser.LoginUser.Stores = StoreDataStore.GetStoresFromUser(LogUser.LoginUser.ID).ToList();
                            return RedirectToAction("HomeStore", "Store", new { StoreId = SelectedStore.CurrentStore.ID });
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
                else
                {
                    ViewBag.LicenseError = "License is not valid.";
                    return View();
                }
            }
            else
            {
                return View();
            }


        }

        private byte[] ConvertToBytes(IFormFile image)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)image.Length);
            return CoverImageBytes;
        }

        public IActionResult UserStores(Guid id)
        {

            SelectedStore.CurrentStore = LogUser.LoginUser.Stores.Where(s => s.ID == id).FirstOrDefault();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> HomeStore(Guid StoreId)
        {

            SelectedStore.CurrentStore = LogUser.LoginUser.Stores.Where(s => s.ID == StoreId).FirstOrDefault();

            var result = productDataStore.GetProductWithLowQuantity(SelectedStore.CurrentStore.ID, 5);

            var empresult = await employeeDataStore.GetEmployeesOfStore(SelectedStore.CurrentStore.ID);

            ViewBag.Result = result;

            return View(SelectedStore.CurrentStore);
        }

        public async Task<IActionResult> DeleteStore(Guid id)
        {
            var result = await StoreDataStore.DeleteItemAsync(id.ToString());

            var userStoreData = StoreDataStore.GetStoresFromUser(LogUser.LoginUser.ID);

            return RedirectToAction("UserStores", userStoreData);
        }

        public async Task<IActionResult> DetailStore(Guid id)
        {
            var detailStoreData = await StoreDataStore.GetItemAsync(id.ToString());
            ViewBag.View = PartialsViewModel<Product>.ViewName;
            return View(detailStoreData);
        }



        public IActionResult AddProduct()
        {
            return RedirectToAction("AddProductStore");
        }

        public async Task<IActionResult> AddProductStore(ProductViewModel productViewModel)
        {
            if (productViewModel.File != null)
            {

                var imgbty = ConvertToBytes(productViewModel.File);

                ProductType productType;

                productType = (ProductType)Enum.Parse(typeof(ProductType), productViewModel.Type);

                var newProductStore = new Product()
                {
                    InventoryQuantity = productViewModel.InventoryQuantity,
                    Price = productViewModel.ProductPrice,
                    ID = Guid.NewGuid(),
                    ProductImage = imgbty,
                    Type = productType,
                    ProductName = productViewModel.ProductName,
                    StoreID = SelectedStore.CurrentStore.ID,
                    ProductDescription = productViewModel.ProductDescription
                };

                var productAdded = await productDataStore.AddItemAsync(newProductStore);

                return View();
            }
            else
            {
                return View();
            }


        }


        public async Task<IActionResult> StoreOrders()
        {

            var orderStoreData = await orderDataStore.GetStoreOrders(SelectedStore.CurrentStore.ID, LogUser.Token.Token);

            var Orders = orderStoreData.Where(o => o.OrderStatus == Status.Submited).ToList();

            return View(Orders);
        }

        public async Task<IActionResult> OrderDetail(Guid id)
        {
            var orderDetail = await orderDataStore.GetItemAsync(id.ToString());

            return View(orderDetail);
        }

        public async Task<IActionResult> Complete(Guid OrderId)
        {
            var order = await orderDataStore.GetItemAsync(OrderId.ToString());

            order.OrderStatus = Status.Completed;
            order.StoreOrder = SelectedStore.CurrentStore;
            var orderUpdated = await orderDataStore.UpdateItemAsync(order);

            if (orderUpdated)
            {
                await LogUser.ComunicationService.SendCompletedOrderNotification(order.ID, order.BuyerId.ToString());
            }
            return RedirectToAction("StoreOrders");
        }

        public IActionResult Search()
        {
            return View("SearchEmployee", new EmployeeViewModel());
        }

        public async Task<IActionResult> SearchEmployee(EmployeeViewModel employeeViewModel)
        {
            var users = await userDataStore.GetItemsAsync();

            var result = users.Where(u => u.Name == employeeViewModel.Search);

            ViewBag.UserResult = result;

            return View();
        }

        public async Task<IActionResult> SearchItem(string SearchItem)
        {
            var itemResult = await productDataStore.SearchItemOfStore(SelectedStore.CurrentStore.ID.ToString(), SearchItem);

            IList<Product> items = new List<Product>();
            if (itemResult != null)
            {

                items.Add(itemResult);

                return View("ShowStoreProducts", items);
            }
            else
            {

                ViewBag.Error = "Item was not found try with another description name.";
                return View("ShowStoreProducts", items);
            }



        }


        //Verificar Error con hub signal r 
        public async Task<IActionResult> SendRequest(Guid userId)
        {
            var jobRequest = new UserRequest()
            {
                ID = Guid.NewGuid(),
                FromStore = SelectedStore.CurrentStore.ID,
                ToUser = userId,
                Type = RequestType.JobRequest,
                RequestAnswer = Answer.None
            };

            var userhaveOrder = RequestDataStore.UserRequestExists(userId, SelectedStore.CurrentStore.ID);

            if (!userhaveOrder)
            {

                var result = await RequestDataStore.AddItemAsync(jobRequest);

                var userhubconnectionResult = await userConnectedDataStore.GetUserConnectedID(jobRequest.ToUser);

                if (userhubconnectionResult != null)
                {

                    await LogUser.ComunicationService.SendRequestToUser(userhubconnectionResult.HubConnectionID, jobRequest);
                    await LogUser.ComunicationService.SendJobNotification(SelectedStore.CurrentStore, userId.ToString());
                }



            }


            return View("SearchEmployee", new EmployeeViewModel());
        }

        public async Task<IActionResult> ShowStoreProducts()
        {
            var products = productDataStore.GetProductFromStore(SelectedStore.CurrentStore.ID);

            return View(products);
        }

        public async Task<IActionResult> EditProduct(Guid id)
        {

            var product = await productDataStore.GetItemAsync(id.ToString());

            currentproductimg = product.ProductImage;
            oldProduct = product;

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(Product editproduct, IFormFile file)
        {
            if (file == null)
            {
                editproduct.ProductImage = currentproductimg;

                if (oldProduct.Price != editproduct.Price || oldProduct.InventoryQuantity != editproduct.InventoryQuantity || oldProduct.ProductName != editproduct.ProductName)
                {
                    var editproductresult = await productDataStore.UpdateItemAsync(editproduct);

                    if (editproductresult)
                    {
                        return RedirectToAction("ShowStoreProducts");
                    }
                }
            }


            if (file != null)
            {

                var imgbty = ConvertToBytes(file);

                editproduct.ProductImage = imgbty;
                var editproductresult = await productDataStore.UpdateItemAsync(editproduct);

                if (editproductresult)
                {
                    return RedirectToAction("ShowStoreProducts");
                }

            }



            return View(editproduct);




        }

        public async Task<IActionResult> DetailProdut(Guid id)
        {
            var product = await productDataStore.GetItemAsync(id.ToString());

            return View(product);
        }

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await productDataStore.DeleteItemAsync(id.ToString());

            return RedirectToAction("ShowStoreProducts");
        }


        public IActionResult AlreadyHaveLicence()
        {
            return RedirectToAction("RegisterStore", "Store");
        }

        public IActionResult RegisterControl()
        {
            return View();
        }

        public async Task<IActionResult> NewLicense()
        {
            var cardResult = await cardDataStore.GetCardFromUser(LogUser.LoginUser.ID,LogUser.Token.Token);

            if (cardResult.Count() > 0)
            {
                var subcriptionToken = await stripeServiceDS.CreateACustomerSubcription(LogUser.LoginUser.StripeUserId);

                if (!string.IsNullOrEmpty(subcriptionToken))
                {

                    var newStoreLicense = new StoreLicense()
                    {
                        ID = Guid.NewGuid(),
                        StartDate = DateTime.Today,
                        LicenseHolderUserId = LogUser.LoginUser.ID

                    };

                    //Lo insertamos a nuestra base de datos
                    var _apiResponse = await storeLicenseDataStore.AddItemAsync(newStoreLicense);

                    if (_apiResponse.IsValid)
                    {
                        var subcription = new Library.Models.Subcription()
                        {
                            StripeCustomerId = LogUser.LoginUser.StripeUserId,
                            StripeSubcriptionID = subcriptionToken,
                            StoreLicense = newStoreLicense.ID,
                            IsDisable = false

                        };

                        var subcriptionResult = await subcriptionDataStore.AddItemAsync(subcription);

                        if (true)
                        {

                            var licenseReuslt = await storeLicenseDataStore.PostStoreLicense(LogUser.LoginUser.Email, LogUser.LoginUser.Name);

                            return RedirectToAction("RegisterControl");

                        }

                    }


                }
            }

            return RedirectToAction("RegisterControl");
        }



    }
}