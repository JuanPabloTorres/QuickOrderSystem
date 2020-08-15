using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {


        public StripeController()
        {

        }


        //[HttpGet("[action]")]
        //public Task<bool> CreateStripeCustomer()
        //{

        //}
    }
}