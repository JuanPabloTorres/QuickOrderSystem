using Library.ApiResponses;
using Library.DTO;
using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IUserDataStore : IDataStore<AppUser>
    {
        Task<Response<Credential>> CheckIfUsernameAndPasswordExist (string username, string password);

        AppUser CheckUserCredential (string username, string password);

        bool ConfirmCode (string code);

        Task<bool> EmailExist (string email);

        bool ForgotCodeSend (string email);
        Task<IEnumerable<UserDTO>> GetUserWithName (string name);

        Task<Response<TokenDTO>> LoginCredential (string username, string password);
        Task<bool> ResendCode (string userId);

        Task<bool> ValidateEmail (string code, string userid);
    }
}