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
    public class SettingController : Controller
    {
        IStripeServiceDS stripeServiceDS;
        IStoreDataStore storeDataStore;
        IWorkHourDataStore WorkHourDataStore;
        public static bool press;
      
        public SettingController(IStripeServiceDS stripeServiceDS,IStoreDataStore storeDataStore,IWorkHourDataStore workHourData)
        {
            this.stripeServiceDS = stripeServiceDS;
            this.storeDataStore = storeDataStore;
            this.WorkHourDataStore = workHourData;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> CancelSubcription(string customerId)
        {
            var _apiResponse = await stripeServiceDS.CancelSubcription(customerId);

            if (_apiResponse.IsValid)
            {
                SelectedStore.CurrentStore.IsDisable = true;

                var disableResult = await storeDataStore.DisableStore(SelectedStore.CurrentStore);

                if (disableResult)
                {
                    LogUser.LoginUser.Stores.Remove(LogUser.LoginUser.Stores.Where(s => s.ID == SelectedStore.CurrentStore.ID).FirstOrDefault());

                    if (LogUser.LoginUser.Stores.Count() > 0)
                    {
                        return RedirectToAction("HomeStore", new { StoreId = LogUser.LoginUser.Stores.FirstOrDefault().ID });

                    }
                    else
                    {

                        return RedirectToAction("RegisterControl","Store");
                    }
                }
                else
                {
                    ViewBag.ErroMsg = "Bad Function Try Again...!";
                    return View("Index");
                }
            }
            else
            {
                ViewBag.ErroMsg = "Bad Function Try Again...!";
                return View("Index");
            }
        }

        public async Task<IActionResult> GetLicense(bool answer)
        {
            press = true;

            if (answer != false)
            {
                press = false;
            }

            if (press)
            {
                ViewBag.Msg = "Show";
            }

            if (answer)
            {
                return RedirectToAction("RegisterControl","Store");
            }
            else
            {
                return View("Index");
            }
        }

        public async Task<IActionResult> GoUpdateStoreInformation(string storeId)
        {
            Guid storeid = Guid.Parse(storeId);

            var storeWorkHours = await WorkHourDataStore.GetStoreWorkHours(storeId);

            SelectedStore.CurrentStore.WorkHours = storeWorkHours.ToList();
            var registerStoreViewModel = new RegisterStoreViewModel(SelectedStore.CurrentStore);
            
            return View(registerStoreViewModel);
        }

        private byte[] ConvertToBytes(IFormFile image)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)image.Length);
            return CoverImageBytes;
        }

        public async Task<IActionResult> UpdateStore(RegisterStoreViewModel store)
        {
            var storeToUpdate = SelectedStore.CurrentStore;

            if (store.File != null)
            {
                storeToUpdate.StoreImage = ConvertToBytes(store.File);
            }

            if (SelectedStore.CurrentStore.StoreName != store.StoreName)
            {
                storeToUpdate.StoreName = store.StoreName;
            }

            if (SelectedStore.CurrentStore.StoreDescription != store.StoreDescription)
            {
                storeToUpdate.StoreName = store.StoreName;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh=>wh.Day == DayOfWeek.Monday.ToString()).FirstOrDefault().OpenTime != store.MOpenTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Monday.ToString()).FirstOrDefault().OpenTime = store.MOpenTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Monday.ToString()).FirstOrDefault().CloseTime != store.MCloseTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Monday.ToString()).FirstOrDefault().OpenTime = store.MCloseTime;
            }


            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Tuesday.ToString()).FirstOrDefault().OpenTime != store.TOpenTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Tuesday.ToString()).FirstOrDefault().OpenTime = store.TOpenTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Tuesday.ToString()).FirstOrDefault().OpenTime != store.TCloseTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Tuesday.ToString()).FirstOrDefault().OpenTime = store.TCloseTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Wednesday.ToString()).FirstOrDefault().OpenTime != store.WOpenTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Wednesday.ToString()).FirstOrDefault().OpenTime = store.WOpenTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Wednesday.ToString()).FirstOrDefault().OpenTime != store.WCloseTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Wednesday.ToString()).FirstOrDefault().OpenTime = store.WCloseTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Thursday.ToString()).FirstOrDefault().OpenTime != store.ThOpenTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Thursday.ToString()).FirstOrDefault().OpenTime = store.ThOpenTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Thursday.ToString()).FirstOrDefault().OpenTime != store.ThCloseTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Thursday.ToString()).FirstOrDefault().OpenTime = store.ThCloseTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Friday.ToString()).FirstOrDefault().OpenTime != store.FOpenTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Friday.ToString()).FirstOrDefault().OpenTime = store.FOpenTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Friday.ToString()).FirstOrDefault().OpenTime != store.FCloseTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Friday.ToString()).FirstOrDefault().OpenTime = store.FCloseTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Saturday.ToString()).FirstOrDefault().OpenTime != store.SOpenTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Saturday.ToString()).FirstOrDefault().OpenTime = store.SOpenTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Saturday.ToString()).FirstOrDefault().OpenTime != store.SCloseTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Saturday.ToString()).FirstOrDefault().OpenTime = store.SCloseTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Sunday.ToString()).FirstOrDefault().OpenTime != store.SuOpenTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Sunday.ToString()).FirstOrDefault().OpenTime = store.SuOpenTime;
            }

            if (SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Sunday.ToString()).FirstOrDefault().OpenTime != store.SuCloseTime)
            {
                SelectedStore.CurrentStore.WorkHours.Where(wh => wh.Day == DayOfWeek.Sunday.ToString()).FirstOrDefault().OpenTime = store.SuCloseTime;
            }

            if (SelectedStore.CurrentStore.StoreType != store.SelectedStoreType)
            {
                SelectedStore.CurrentStore.StoreType = store.SelectedStoreType;
            }

            var storeUpdatedResult =await storeDataStore.UpdateItemAsync(SelectedStore.CurrentStore);


            if (storeUpdatedResult)
            {
                return RedirectToAction("HomeStore", "Store", new { StoreId = SelectedStore.CurrentStore.ID });
            }

            return RedirectToAction("GoUpdateStoreInformation",new {storeId = SelectedStore.CurrentStore.ID });

            ////Logid to update store

            //var storedUpdated = storeDataStore.UpdateItemAsync()

            //return View();
        }



    }
}