using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int InventoryQuantity { get; set; }
    }
}
