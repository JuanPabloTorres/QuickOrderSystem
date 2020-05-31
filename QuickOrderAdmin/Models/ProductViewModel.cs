using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOrderAdmin.Models
{
    public class ProductViewModel
    {


        public IFormFile File { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please enter product name"), StringLength(50)]
        public string ProductName { get; set; }

       
        public double ProductPrice { get; set; }

       
        
        public int InventoryQuantity { get; set; }

    }
}
