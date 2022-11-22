using System.IO;
using System.Threading.Tasks;

namespace QuickOrderApp.Utilities.Dependency.Interface
{
    public interface IPickPhotoService
    {
        Task<Stream> GetImageStreamAsync ();
    }
}