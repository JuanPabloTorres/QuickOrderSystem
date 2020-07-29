using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        public byte[] ProductImage { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public double Price { get; set; }

        public int InventoryQuantity { get; set; }

        public Guid StoreId { get; set; }
    }
}
