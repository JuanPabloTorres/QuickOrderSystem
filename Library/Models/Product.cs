using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [DisplayName("Image")]
        public byte[] ProductImage { get; set; }

        [DisplayName("Product")]
        public string ProductName { get; set; }

        [DisplayName("Description")]
        public string ProductDescription { get; set; }

        [DisplayName("Price")]
        public double Price { get; set; }
        [DisplayName("Inventory")]
        public int InventoryQuantity { get; set; }

        public Guid StoreId { get; set; }

        public ProductType Type { get; set; }
    }


    public enum ProductType
    {
        Carnes,
        Bebidas,
        FrutasYVerduras,
        Limpieza,
        Herramientas,
        Escolar,
        Alimentos,
        Animales,
        Juguetes,
        Otro
    }
}
