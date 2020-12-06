using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DTO
{
    public class StoreDTO
    {
        public string StoreName { get; set; }


        public Guid? UserId { get; set; }

        public byte[] StoreImage { get; set; }

        public string StoreDescription { get; set; }

        public Guid StoreId { get; set; }
    }
}
