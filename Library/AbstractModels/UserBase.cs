using Library.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.AbstractModels
{
    public  abstract class  UserBase:BaseModel
    {
        public string Address { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public string Phone { get; set; }
    }
}
