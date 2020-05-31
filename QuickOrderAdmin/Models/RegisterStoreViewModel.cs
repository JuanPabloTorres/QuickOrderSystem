using Library.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOrderAdmin.Models
{
    public class RegisterStoreViewModel
    {
        [DisplayName("Store Name")]
        [Required(ErrorMessage = "Please enter store name"), StringLength(50)]
        public string StoreName { get; set; }

        [Required]
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
        [DisplayName("Thuersday close Time")]
        public DateTime SCloseTime { get; set; }

        public  readonly string Saturday;

        [DataType(DataType.Time)]
        [DisplayName("Sunday open Time")]
        public DateTime SuOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Sunday close Time")]
        public DateTime SuCloseTime { get; set; }

        public readonly string Sunday;

        public IFormFile File { get; set; }

       
    }
}
