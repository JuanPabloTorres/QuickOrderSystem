using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiQuickOrder.Service
{
    public interface ITokenService
    {

        Task<string> GetToken();

    }
}
