using Library.Models;
using Library.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickOrderAdmin.Models;
using QuickOrderAdmin.Utilities;
using System.Diagnostics;

namespace QuickOrderAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IProductDataStore productDataStore;

      

        public HomeController(ILogger<HomeController> logger, IProductDataStore productData)
        {
            _logger = logger;
            productDataStore = productData;
           
        }

        public IActionResult Index()
        {



            return View(SelectedStore.CurrentStore);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
