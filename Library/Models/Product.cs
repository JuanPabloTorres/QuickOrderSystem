using Library.AbstractModels;
using Library.Helpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Products")]
    public class Product : BaseModel
    {
        [DisplayName("Inventory")]
        public int InventoryQuantity { get; set; }

        [DisplayName("Price")]
        public double Price { get; set; }

        [DisplayName("Description")]
        public string ProductDescription { get; set; }

        [DisplayName("Image")]
        public byte[] ProductImage { get; set; }

        [DisplayName("Product")]
        public string ProductName { get; set; }

        public ProductType Type { get; set; }
    }
}