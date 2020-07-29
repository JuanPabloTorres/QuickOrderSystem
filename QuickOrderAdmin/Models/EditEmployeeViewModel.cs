using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuickOrderAdmin.Models
{
    public class EditEmployeeViewModel
    {

        public Employee EmployeeInformation { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        [DisplayName("Monday Open Time")]
        public DateTime MOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        [DisplayName("Monday close Time")]
        public DateTime MCloseTime { get; set; }



        [DataType(DataType.Time)]
        [DisplayName("Tuesday open Time")]
        public DateTime TOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Tuesday close Time")]
        public DateTime TCloseTime { get; set; }



        [DataType(DataType.Time)]
        [DisplayName("Wednesday open Time")]
        public DateTime WOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Wednesday close Time")]
        public DateTime WCloseTime { get; set; }



        [DataType(DataType.Time)]
        [DisplayName("Thuersday open Time")]
        public DateTime ThOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Thuersday close Time")]
        public DateTime ThCloseTime { get; set; }


        [DataType(DataType.Time)]
        [DisplayName("Friday open Time")]
        public DateTime FOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Friday close Time")]
        public DateTime FCloseTime { get; set; }


        [DataType(DataType.Time)]
        [DisplayName("Saturday open Time")]
        public DateTime SOpenTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Saturday close Time")]
        public DateTime SCloseTime { get; set; }



        [DataType(DataType.Time)]
        [DisplayName("Sunday open Time")]
        public DateTime SuOpenTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Sunday close Time")]
        public DateTime SuCloseTime { get; set; }



        public string Position { get; set; }

        List<string> EmployeePositions;


        public EditEmployeeViewModel()
        {



            EmployeePositions = new List<string>();
            var positions = Enum.GetValues(typeof(EmployeeType));

            foreach (var item in positions)
            {
                EmployeePositions.Add(item.ToString());
            }



        }

    }
}
