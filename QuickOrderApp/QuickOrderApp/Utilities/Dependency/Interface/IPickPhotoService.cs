using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace QuickOrderApp.Utilities.Dependency.Interface
{
    public interface IPickPhotoService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
