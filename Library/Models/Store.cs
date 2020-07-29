using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Stores")]
    public class Store
    {
        [Key]
        public Guid StoreId { get; set; }

        public string StoreName { get; set; }

        public Guid? UserId { get; set; }

        public byte[] StoreImage { get; set; }

        public string StoreDescription { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<WorkHour> WorkHours { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Guid StoreRegisterLicenseId { get; set; }

        [ForeignKey("StoreRegisterLicenseId")]
        public StoreLicense UserStoreLicense { get; set; }

        public StoreType StoreType { get; set; }

    }

    public enum StoreType
    {
        None,
        Store,
        Grocery,
        BarberShop,
        Farming,
        Restaurant,
        Service,
        AutorParts,
        ClothingStore,

    }
}
