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

namespace QuickOrderAdmin.Controllers
{
    public class StoreController : Controller
    {

        IStoreDataStore StoreDataStore;
        public StoreController(IStoreDataStore storeData)
        {
            StoreDataStore = storeData;
        }
       
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> RegisterStore(RegisterStoreViewModel registerStoreViewModel)
        {
            if (registerStoreViewModel.File != null)
            {
                var ImgToBty = ConvertToBytes(registerStoreViewModel.File);

                var listWorkHour = new List<WorkHour>();

                var MondayWH = new WorkHour()
                {
                    WorkHourId = Guid.NewGuid(),
                    OpenTime = registerStoreViewModel.MOpenTime,
                    CloseTime = registerStoreViewModel.MCloseTime

                };
                var TuesdayWH = new WorkHour()
                {
                    WorkHourId = Guid.NewGuid(),
                    OpenTime = registerStoreViewModel.TOpenTime,
                    CloseTime = registerStoreViewModel.TCloseTime
                };
                var WednesdayWH = new WorkHour()
                {
                    WorkHourId = Guid.NewGuid(),
                    OpenTime = registerStoreViewModel.WOpenTime,
                    CloseTime = registerStoreViewModel.WCloseTime
                };
                var ThuerdayWH = new WorkHour()
                {
                    WorkHourId = Guid.NewGuid(),
                    OpenTime = registerStoreViewModel.ThOpenTime,
                    CloseTime = registerStoreViewModel.ThCloseTime
                };
                var FridayWH = new WorkHour()
                {
                    WorkHourId = Guid.NewGuid(),
                    OpenTime = registerStoreViewModel.FOpenTime,
                    CloseTime = registerStoreViewModel.FCloseTime
                };
                var SaturdayWH = new WorkHour()
                {
                    WorkHourId = Guid.NewGuid(),
                    OpenTime = registerStoreViewModel.SOpenTime,
                    CloseTime = registerStoreViewModel.SCloseTime
                };
                var SundayWH = new WorkHour()
                {
                    WorkHourId = Guid.NewGuid(),
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
                    StoreId = Guid.NewGuid(),
                    /////////////////////////CHANGE Dev///////////////////////
                    StoreName = registerStoreViewModel.StoreName,
                    WorkHours = listWorkHour,
                    StoreImage = ImgToBty
                };

                var newStoreAddedResult = await StoreDataStore.AddItemAsync(newStore);

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
    }
}