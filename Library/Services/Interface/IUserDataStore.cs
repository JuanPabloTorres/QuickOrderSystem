using Library.DTO;
using Library.Models;

namespace Library.Services.Interface
{
    public interface IUserDataStore : IDataStore<User>
    {
        User CheckUserCredential(string username, string password);
        bool ForgotCodeSend(string email);
        bool ConfirmCode(string code);

        TokenDTO LoginCredential(string username, string password);
    }
}
