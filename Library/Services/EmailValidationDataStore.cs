using Library.Models;
using Library.Services.Interface;

namespace Library.Services
{
    public class EmailValidationDataStore : DataStoreService<EmailValidation>, IEmailValidationService
    {
    }
}