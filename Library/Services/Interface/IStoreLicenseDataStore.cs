using Library.Models;
using System;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IStoreLicenseDataStore : IDataStore<StoreLicense>
    {
        Task<bool> IsLicenseInUsed (string license);

        Task<bool> PostStoreLicense (string email, string username);

        bool StoreLicenseExists (Guid id);

        Task<bool> UpdateLicenceInCode (Guid id);
    }
}