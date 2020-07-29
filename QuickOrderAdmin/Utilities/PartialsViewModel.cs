using System.Collections.Generic;

namespace QuickOrderAdmin.Utilities
{
    public class PartialsViewModel<T>
    {
        public static string ViewName { get; set; }

        public static List<T> Data { get; set; }

    }
}
