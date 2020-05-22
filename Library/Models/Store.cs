using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Models
{
    [Table("Stores")]
    public class Store
    {
        [Key]
        public Guid StoreId { get; set; }

        public string StoreName { get; set; }

        public byte[] StoreImage { get; set; }

       public ICollection<Product> Products { get; set; }

        public ICollection<WorkHour> WorkHours { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
