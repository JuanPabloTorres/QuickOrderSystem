using Library.Models;
using System;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IStoreLicenseDataStore : IDataStore<StoreLicense>
    {
        bool StoreLicenseExists(Guid id);
        Task<bool> PostStoreLicense(string email, string username);

        Task<bool> IsLicenseInUsed(string license);

        Task<bool> UpdateLicenceInCode(Guid id);
    }
}
