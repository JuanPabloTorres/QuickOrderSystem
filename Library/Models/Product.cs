using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
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

    [Table("Products")]
    public class Product
    {
        [DisplayName("Inventory")]
        public int InventoryQuantity { get; set; }

        [DisplayName("Price")]
        public double Price { get; set; }

        [DisplayName("Description")]
        public string ProductDescription { get; set; }

        [Key]
        public Guid ProductId { get; set; }

        [DisplayName("Image")]
        public byte[] ProductImage { get; set; }

        [DisplayName("Product")]
        public string ProductName { get; set; }

        public Guid StoreId { get; set; }

        public ProductType Type { get; set; }
    }
}