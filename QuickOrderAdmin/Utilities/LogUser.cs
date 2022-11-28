using Library.DTO;
using Library.Models;

namespace QuickOrderAdmin.Utilities
{
    public class LogUser
    {


        public static AppUser LoginUser { get; set; }

        public static TokenDTO Token { get; set; }

        public static UsersConnected UsersConnected { get; set; }

        public static ComunicationService ComunicationService { get; set; }

    }
}
