using Library.DTO;
using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IUserDataStore : IDataStore<User>
    {
        User CheckUserCredential(string username, string password);
        bool ForgotCodeSend(string email);
        bool ConfirmCode(string code);

        TokenDTO LoginCredential(string username, string password);
        Task<IEnumerable<UserDTO>> GetUserWithName(string name);

        Task<bool> CheckIfUsernameAndPasswordExist(string username, string password);
    }
}
