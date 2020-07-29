using Microsoft.AspNetCore.Http;
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

    }
}
