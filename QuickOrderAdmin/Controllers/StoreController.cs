 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Library.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickOrderAdmin.Models;
using QuickOrderAdmin.Utilities;

namespace QuickOrderAdmin.Controllers
{
    public class StoreController : Controller
    {
        
        IStoreDataStore StoreDataStore;
        IUserDataStore userDataStore;
        IProductDataStore productDataStore;
        IStoreLicenseDataStore storeLicenseDataStore;
        IOrderDataStore orderDataStore;
        public StoreController(IStoreDataStore storeData,IUserDataStore userData,IProductDataStore productData,IStoreLicenseDataStore storeLicenseData,IOrderDataStore orderData)
        {
            userDataStore = userData;
            StoreDataStore = storeData;
            productDataStore = productData;
            storeLicenseDataStore = storeLicenseData;
            orderDataStore = orderData;
        }
       
        public IActionResult Index()
        {
            return View();
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
                Day= DayOfWeek.Wednesday.ToString(),
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
                    UserId = LogUser.LoginUser.UserId
                };

                var newStoreAddedResult = await StoreDataStore.AddItemAsync(newStore);

                var result = userDataStore.CheckUserCredential(LogUser.LoginUser.UserLogin.Username, LogUser.LoginUser.UserLogin.Password);
                LogUser.LoginUser = result;

            }
                }
                else
                {
                    ViewBag.LicenseError = "License is not valid.";
                    return View();
                }
            }

            return View();
        }

        private byte[] ConvertToBytes(IFormFile image)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)image.Length);
            return CoverImageBytes;
        }

        public  IActionResult UserStores(Guid id)
        {
            //var userStoreData =  StoreDataStore.GetStoresFromUser(LogUser.LoginUser.UserId);
            SelectedStore.CurrentStore = LogUser.LoginUser.Stores.Where(s => s.StoreId == id).FirstOrDefault();
            return RedirectToAction("Index","Home");
        }

        public IActionResult HomeStore(Guid StoreId)
        {
            SelectedStore.CurrentStore = LogUser.LoginUser.Stores.Where(s => s.StoreId == StoreId).FirstOrDefault();
            return View(SelectedStore.CurrentStore);
        }

        public async Task<IActionResult> DeleteStore(Guid id)
        {
            var result = await StoreDataStore.DeleteItemAsync(id.ToString());

            var userStoreData = StoreDataStore.GetStoresFromUser(LogUser.LoginUser.UserId);

            return RedirectToAction("UserStores",userStoreData);
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
            if (productViewModel.File != null )
            {

                var imgbty = ConvertToBytes(productViewModel.File);

                var newProductStore = new Product()
                {
                    InventoryQuantity = productViewModel.InventoryQuantity,
                    Price = productViewModel.ProductPrice,
                    ProductId = Guid.NewGuid(),
                    ProductImage = imgbty,
                    ProductName = productViewModel.ProductName,
                    StoreId = SelectedStore.CurrentStore.StoreId
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
            var orderStoreData =  orderDataStore.GetStoreOrders(SelectedStore.CurrentStore.StoreId);

            return View(orderStoreData);
        }

    }
}