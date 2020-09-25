using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public Gender Gender { get; set; }

        public Guid LoginId { get; set; }

        [ForeignKey("LoginId")]
        public Login UserLogin { get; set; }

        public ICollection<Store> Stores { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<PaymentCard> PaymentCards { get; set; }

        public string StripeUserId { get; set; }

        public bool IsValidUser { get; set; }

        //public Guid StoreRegisterLicenseId { get; set; }

        //[ForeignKey("StoreRegisterLicenseId")]
        //public StoreLicense UserStoreLicense { get; set; }



    }

    public enum Gender
    {
        Male,
        Female
    }
}
