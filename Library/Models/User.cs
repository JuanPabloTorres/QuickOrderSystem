using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Guid LoginId { get; set; }

        [ForeignKey("LoginId")]
        public Login UserLogin { get; set; }

        public ICollection<Store> Stores { get; set; }

        //public Guid StoreRegisterLicenseId { get; set; }

        //[ForeignKey("StoreRegisterLicenseId")]
        //public StoreLicense UserStoreLicense { get; set; }



    }
}
