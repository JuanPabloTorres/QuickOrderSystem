using System.Threading.Tasks;

namespace WebApiQuickOrder.Service
{
    public interface ITokenService
    {
        Task<string> GetToken ();
    }
}