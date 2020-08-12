using Library.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuickOrderAdmin.Models
{
    public class ProductViewModel
    {


        public IFormFile File { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please enter product name."), StringLength(50)]
        public string ProductName { get; set; }


        public double ProductPrice { get; set; }

        [DisplayName("Product Description")]
        [Required(ErrorMessage = "Please enter description."), StringLength(100)]
        public string ProductDescription { get; set; }

        public int InventoryQuantity { get; set; }


        public string Type { get; set; }

        List<string> ProductTypes;

        public ProductViewModel()
        {
            ProductTypes = new List<string>();
            var types = Enum.GetValues(typeof(ProductType));
            foreach (var item in types)
            {
                ProductTypes.Add(item.ToString());
            }

        }



    }
}
