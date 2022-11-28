using Library.AbstractModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Users")]
    public class AppUser : UserBase
    {
        public ICollection<Employee> Employees { get; set; }

        public bool IsValidUser { get; set; }

        public Guid LoginId { get; set; }

        public ICollection<PaymentCard> PaymentCards { get; set; }

        public ICollection<Store> Stores { get; set; }

        public string StripeUserId { get; set; }

        [ForeignKey("LoginId")]
        public Credential UserLogin { get; set; }

        //public Guid StoreRegisterLicenseId { get; set; }

        //[ForeignKey("StoreRegisterLicenseId")]
        //public StoreLicense UserStoreLicense { get; set; }
    }
}