using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    [Table("Users")]
    public class User
    {
        public string Address { get; set; }

        public string Email { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public Gender Gender { get; set; }

        public bool IsValidUser { get; set; }

        public Guid LoginId { get; set; }

        public string Name { get; set; }

        public ICollection<PaymentCard> PaymentCards { get; set; }

        public string Phone { get; set; }

        public ICollection<Store> Stores { get; set; }

        public string StripeUserId { get; set; }

        [Key]
        public Guid UserId { get; set; }

        [ForeignKey("LoginId")]
        public Login UserLogin { get; set; }

        //public Guid StoreRegisterLicenseId { get; set; }

        //[ForeignKey("StoreRegisterLicenseId")]
        //public StoreLicense UserStoreLicense { get; set; }
    }
}