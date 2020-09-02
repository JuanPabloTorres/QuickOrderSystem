using Library.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace QuickOrderAdmin.Models
{
    public class RegisterStoreViewModel
    {
        [DisplayName("Store Name")]
        [Required(ErrorMessage = "Please enter store name"), StringLength(50)]
        public string StoreName { get; set; }

        [DisplayName("Store Description")]
        [Required(ErrorMessage = "Please enter store description"), StringLength(50)]
        public string StoreDescription { get; set; }

        [DisplayName("Stripe Public Key")]
        [Required(ErrorMessage = "Please enter stripe public key"), StringLength(150)]
        public string StripePublicKey { get; set; }

        [DisplayName("Stripe Secret Key")]
        [Required(ErrorMessage = "Please enter stripe secret key"), StringLength(150)]
        public string StripeSecretKey { get; set; }

       
        [DisplayName("Store Register License")]
        public Guid StoreLicence { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        [DisplayName("Monday Open Time")]
        public DateTime MOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        [DisplayName("Monday close Time")]
        public DateTime MCloseTime { get; set; }

        public readonly string Monday;

        [DataType(DataType.Time)]
        [DisplayName("Tuesday open Time")]
        public DateTime TOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Tuesday close Time")]
        public DateTime TCloseTime { get; set; }

        public readonly string Tuesday;

        [DataType(DataType.Time)]
        [DisplayName("Wednesday open Time")]
        public DateTime WOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Wednesday close Time")]
        public DateTime WCloseTime { get; set; }

        public readonly string Wednesday;

        [DataType(DataType.Time)]
        [DisplayName("Thuersday open Time")]
        public DateTime ThOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Thuersday close Time")]
        public DateTime ThCloseTime { get; set; }

        public readonly string Thuersday;
        [DataType(DataType.Time)]
        [DisplayName("Friday open Time")]
        public DateTime FOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Friday close Time")]
        public DateTime FCloseTime { get; set; }

        public readonly string Friday;
        [DataType(DataType.Time)]
        [DisplayName("Saturday open Time")]
        public DateTime SOpenTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Saturday close Time")]
        public DateTime SCloseTime { get; set; }

        public readonly string Saturday;

        [DataType(DataType.Time)]
        [DisplayName("Sunday open Time")]
        public DateTime SuOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Sunday close Time")]
        public DateTime SuCloseTime { get; set; }

        public readonly string Sunday;

        public StoreType SelectedStoreType { get; set; }

        public Guid StoreId { get; set; }

        public IFormFile File { get; set; }

        public byte[] StoreImage { get; set; }
        public RegisterStoreViewModel()
        {

            
        }

        public RegisterStoreViewModel(Store store)
        {
            this.StoreName = store.StoreName;
            this.StoreLicence = store.StoreLicenceId;
            this.StoreDescription = store.StoreDescription;
            this.StripePublicKey = store.PBKey;
            this.StripeSecretKey = store.SKKey;
            this.StoreId = store.StoreId;
            this.SelectedStoreType = store.StoreType;
            this.StoreImage = store.StoreImage;

            var stream = new MemoryStream(store.StoreImage);

            this.File = new FormFile(stream, 0, store.StoreImage.Length, "name", "fileName");

            this.MOpenTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Monday.ToString()).FirstOrDefault().OpenTime;
            this.MCloseTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Monday.ToString()).FirstOrDefault().CloseTime;


            this.TOpenTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Tuesday.ToString()).FirstOrDefault().OpenTime;
            this.TCloseTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Tuesday.ToString()).FirstOrDefault().CloseTime;

            this.WOpenTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Wednesday.ToString()).FirstOrDefault().OpenTime;
            this.WCloseTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Wednesday.ToString()).FirstOrDefault().CloseTime;

            this.ThOpenTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Thursday.ToString()).FirstOrDefault().OpenTime;
            this.ThOpenTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Thursday.ToString()).FirstOrDefault().CloseTime;

            this.FOpenTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Friday.ToString()).FirstOrDefault().OpenTime;
            this.FCloseTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Friday.ToString()).FirstOrDefault().CloseTime;

            this.SOpenTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Saturday.ToString()).FirstOrDefault().OpenTime;
            this.SCloseTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Saturday.ToString()).FirstOrDefault().CloseTime;

            this.SuOpenTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Sunday.ToString()).FirstOrDefault().OpenTime;
            this.SuCloseTime = store.WorkHours.Where(wh => wh.Day == DayOfWeek.Sunday.ToString()).FirstOrDefault().CloseTime;

           





        }


    }
}
