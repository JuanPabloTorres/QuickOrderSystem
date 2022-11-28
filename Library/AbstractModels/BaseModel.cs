using System;

using System.ComponentModel.DataAnnotations;

namespace Library.AbstractModels
{
    public abstract class BaseModel
    {
        public DateTime CreatedDateTime { get; set; }

        [Key]
        public Guid ID { get; set; }

        public bool IsDisasble { get; set; }
        public Guid StoreID { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}