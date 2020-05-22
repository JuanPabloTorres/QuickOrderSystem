using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Interface
{
   public interface IStoreLicenseDataStore:IDataStore<StoreLicense>
    {
        bool StoreLicenseExists(Guid id);
    }
}
