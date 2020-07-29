using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuickOrderAdmin.Controllers
{
    public class StoreHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}