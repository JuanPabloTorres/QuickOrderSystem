using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOrderAdmin.Utilities
{
    public class PartialsViewModel<T>
    {
        public static  string ViewName { get; set; }

        public static List<T> Data { get; set; }

    }
}
