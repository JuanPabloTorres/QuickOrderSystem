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
        UsersConnected UsersConnected;
        ComunicationService ComunicationService;
        #region DataStores
        IStoreDataStore StoreDataStore;
        IUserDataStore userDataStore;
        IProductDataStore productDataStore;
        IStoreLicenseDataStore storeLicenseDataStore;
        IOrderDataStore orderDataStore;
        IEmployeeDataStore employeeDataStore;
        IRequestDataStore RequestDataStore;
        IUserConnectedDataStore userConnectedDataStore;

        static byte[] currentproductimg;

        static Product oldProduct;
        #endregion

        public StoreController(IStoreDataStore storeData, IUserDataStore userData, IProductDataStore productData, IStoreLicenseDataStore storeLicenseData, IOrderDataStore orderData, IEmployeeDataStore employeeData, IRequestDataStore requestData, UsersConnected usersConnected,IUserConnectedDataStore userConnectedDataStore,ComunicationService comunication)
        {
            userDataStore = userData;
            StoreDataStore = storeData;
            productDataStore = productData;
            storeLicenseDataStore = storeLicenseData;
            orderDataStore = orderData;
            employeeDataStore = employeeData;
            RequestDataStore = requestData;
            this.userConnectedDataStore = userConnectedDataStore;
            this.ComunicationService = comunication;

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
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Monday.ToString(),
                            OpenTime = registerStoreViewModel.MOpenTime,
                            CloseTime = registerStoreViewModel.MCloseTime

                        };
                        var TuesdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Tuesday.ToString(),
                            OpenTime = registerStoreViewModel.TOpenTime,
                            CloseTime = registerStoreViewModel.TCloseTime
                        };
                        var WednesdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Wednesday.ToString(),
                            OpenTime = registerStoreViewModel.WOpenTime,
                            CloseTime = registerStoreViewModel.WCloseTime
                        };
                        var ThuerdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Thursday.ToString(),
                            OpenTime = registerStoreViewModel.ThOpenTime,
                            CloseTime = registerStoreViewModel.ThCloseTime
                        };
                        var FridayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Friday.ToString(),
                            OpenTime = registerStoreViewModel.FOpenTime,
                            CloseTime = registerStoreViewModel.FCloseTime
                        };
                        var SaturdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Saturday.ToString(),
                            OpenTime = registerStoreViewModel.SOpenTime,
                            CloseTime = registerStoreViewModel.SCloseTime
                        };
                        var SundayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
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
                            StoreId = StoreId,
                            /////////////////////////CHANGE Dev///////////////////////
                            StoreName = registerStoreViewModel.StoreName,
                            WorkHours = listWorkHour,
                            StoreImage = ImgToBty,
                            StoreRegisterLicenseId = registerStoreViewModel.StoreLicence,
                            UserId = LogUser.LoginUser.UserId,
                            StoreType = registerStoreViewModel.SelectedStoreType,
                            StoreDescription = registerStoreViewModel.StoreDescription
                        };

                        var newStoreAddedResult = await StoreDataStore.AddItemAsync(newStore);

                        var result = userDataStore.CheckUserCredential(LogUser.LoginUser.UserLogin.Username, LogUser.LoginUser.UserLogin.Password);
                        LogUser.LoginUser = result;

                        if (newStoreAddedResult)
                        {
                            return RedirectToAction("HomeStore", new { StoreId = newStore.StoreId });
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

                    if (registerStoreViewModel.File != null)
                    {
                        var ImgToBty = ConvertToBytes(registerStoreViewModel.File);
                        var StoreId = Guid.NewGuid();
                        var listWorkHour = new List<WorkHour>();

                        var MondayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Monday.ToString(),
                            OpenTime = registerStoreViewModel.MOpenTime,
                            CloseTime = registerStoreViewModel.MCloseTime

                        };
                        var TuesdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Tuesday.ToString(),
                            OpenTime = registerStoreViewModel.TOpenTime,
                            CloseTime = registerStoreViewModel.TCloseTime
                        };
                        var WednesdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Wednesday.ToString(),
                            OpenTime = registerStoreViewModel.WOpenTime,
                            CloseTime = registerStoreViewModel.WCloseTime
                        };
                        var ThuerdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Thursday.ToString(),
                            OpenTime = registerStoreViewModel.ThOpenTime,
                            CloseTime = registerStoreViewModel.ThCloseTime
                        };
                        var FridayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Friday.ToString(),
                            OpenTime = registerStoreViewModel.FOpenTime,
                            CloseTime = registerStoreViewModel.FCloseTime
                        };
                        var SaturdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Saturday.ToString(),
                            OpenTime = registerStoreViewModel.SOpenTime,
                            CloseTime = registerStoreViewModel.SCloseTime
                        };
                        var SundayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
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
                            StoreId = StoreId,
                            /////////////////////////CHANGE Dev///////////////////////
                            StoreName = registerStoreViewModel.StoreName,
                            WorkHours = listWorkHour,
                            StoreImage = ImgToBty,
                            StoreRegisterLicenseId = registerStoreViewModel.StoreLicence,
                            UserId = LogUser.LoginUser.UserId,
                            StoreType = registerStoreViewModel.SelectedStoreType,
                            StoreDescription = registerStoreViewModel.StoreDescription
                        };

                        var newStoreAddedResult = await StoreDataStore.AddItemAsync(newStore);

                        var result = userDataStore.CheckUserCredential(LogUser.LoginUser.UserLogin.Username, LogUser.LoginUser.UserLogin.Password);
                        LogUser.LoginUser = result;

                        if (newStoreAddedResult)
                        {
                            return RedirectToAction("Index", "Login", new { loginViewModel = new LoginViewModel() });
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

        public async Task<IActionResult> RegisterStoreAdmin(RegisterStoreViewModel registerStoreViewModel)
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
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Monday.ToString(),
                            OpenTime = registerStoreViewModel.MOpenTime,
                            CloseTime = registerStoreViewModel.MCloseTime

                        };
                        var TuesdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Tuesday.ToString(),
                            OpenTime = registerStoreViewModel.TOpenTime,
                            CloseTime = registerStoreViewModel.TCloseTime
                        };
                        var WednesdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Wednesday.ToString(),
                            OpenTime = registerStoreViewModel.WOpenTime,
                            CloseTime = registerStoreViewModel.WCloseTime
                        };
                        var ThuerdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Thursday.ToString(),
                            OpenTime = registerStoreViewModel.ThOpenTime,
                            CloseTime = registerStoreViewModel.ThCloseTime
                        };
                        var FridayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Friday.ToString(),
                            OpenTime = registerStoreViewModel.FOpenTime,
                            CloseTime = registerStoreViewModel.FCloseTime
                        };
                        var SaturdayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
                            Day = DayOfWeek.Saturday.ToString(),
                            OpenTime = registerStoreViewModel.SOpenTime,
                            CloseTime = registerStoreViewModel.SCloseTime
                        };
                        var SundayWH = new WorkHour()
                        {
                            WorkHourId = Guid.NewGuid(),
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
                            StoreId = StoreId,
                            /////////////////////////CHANGE Dev///////////////////////
                            StoreName = registerStoreViewModel.StoreName,
                            WorkHours = listWorkHour,
                            StoreImage = ImgToBty,
                            StoreRegisterLicenseId = registerStoreViewModel.StoreLicence,
                            UserId = LogUser.LoginUser.UserId,
                            StoreType = registerStoreViewModel.SelectedStoreType,
                            StoreDescription = registerStoreViewModel.StoreDescription
                        };

                        var newStoreAddedResult = await StoreDataStore.AddItemAsync(newStore);

                        if (newStoreAddedResult)
                        {
                            LogUser.LoginUser.Stores = StoreDataStore.GetStoresFromUser(LogUser.LoginUser.UserId).ToList();
                            return RedirectToAction("HomeStore", "Store", new { StoreId = SelectedStore.CurrentStore.StoreId });
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
            //var userStoreData =  StoreDataStore.GetStoresFromUser(LogUser.LoginUser.UserId);
            SelectedStore.CurrentStore = LogUser.LoginUser.Stores.Where(s => s.StoreId == id).FirstOrDefault();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> HomeStore(Guid StoreId)
        {

            SelectedStore.CurrentStore = LogUser.LoginUser.Stores.Where(s => s.StoreId == StoreId).FirstOrDefault();
            var result = productDataStore.GetProductWithLowQuantity(SelectedStore.CurrentStore.StoreId, 5);


            var empresult =await employeeDataStore.GetEmployeesOfStore(SelectedStore.CurrentStore.StoreId);


            //ViewBag.EmpResult = empresult.Where(e=>e.EmployeeWorkHours.Where(wh=>wh.OpenTime.))
            ViewBag.Result = result;
            return View(SelectedStore.CurrentStore);
        }

        public async Task<IActionResult> DeleteStore(Guid id)
        {
            var result = await StoreDataStore.DeleteItemAsync(id.ToString());

            var userStoreData = StoreDataStore.GetStoresFromUser(LogUser.LoginUser.UserId);

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

              productType=(ProductType) Enum.Parse(typeof(ProductType), productViewModel.Type);

                var newProductStore = new Product()
                {
                    InventoryQuantity = productViewModel.InventoryQuantity,
                    Price = productViewModel.ProductPrice,
                    ProductId = Guid.NewGuid(),
                    ProductImage = imgbty,
                    Type = productType,
                    ProductName = productViewModel.ProductName,
                    StoreId = SelectedStore.CurrentStore.StoreId,
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

        public IActionResult ShowMap()
        {
            return View();
        }

        public IActionResult StoreOrders()
        {  

            var orderStoreData = orderDataStore.GetStoreOrders(SelectedStore.CurrentStore.StoreId,LogUser.Token.Token);

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


        //Verificar Error con hub signal r 
        public async Task<IActionResult> SendRequest(Guid userId)
        {
            var jobRequest = new UserRequest()
            {
                RequestId = Guid.NewGuid(),
                FromStore = SelectedStore.CurrentStore.StoreId,
                ToUser = userId,
                Type = RequestType.JobRequest,
                RequestAnswer = Answer.None
            };

            var userhaveOrder = RequestDataStore.UserRequestExists(userId, SelectedStore.CurrentStore.StoreId);

            if (!userhaveOrder)
            {

                var result = await RequestDataStore.AddItemAsync(jobRequest);

                var userhubconnectionResult =await userConnectedDataStore.GetUserConnectedID(jobRequest.ToUser);

                await this.ComunicationService.SendRequestToUser(userhubconnectionResult.HubConnectionID,jobRequest);

               
            }  


            return View("SearchEmployee", new EmployeeViewModel());
        }

        public async Task<IActionResult> ShowStoreProducts()
        {
            var products = productDataStore.GetProductFromStore(SelectedStore.CurrentStore.StoreId);

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

                if (oldProduct.Price != editproduct.Price || oldProduct.InventoryQuantity != editproduct.InventoryQuantity)
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



    }
}