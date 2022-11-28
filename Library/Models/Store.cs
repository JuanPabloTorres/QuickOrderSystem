using Library.AbstractModels;
using Library.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Stores")]
    public class Store : BaseModel
    {
        public ICollection<Employee> Employees { get; set; }

        public bool IsDisable { get; set; }

        public ICollection<Order> Orders { get; set; }

        public string PBKey { get; set; }

        public ICollection<Product> Products { get; set; }

        public string SKKey { get; set; }

        public string StoreDescription { get; set; }

        public byte[] StoreImage { get; set; }

        public Guid StoreLicenceId { get; set; }

        public string StoreName { get; set; }

        public Guid StoreRegisterLicenseId { get; set; }

        public StoreType StoreType { get; set; }

        public Guid? UserId { get; set; }

        [ForeignKey("StoreRegisterLicenseId")]
        public StoreLicense UserStoreLicense { get; set; }

        public ICollection<WorkHour> WorkHours { get; set; }
    }
}