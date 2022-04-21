using Microsoft.AspNetCore.Mvc;
using SandS.Helpers;
using SandS.Models;
using Service;
using System.Diagnostics;

namespace SandS.Controllers
{
    public class HomeController : BaseClass
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataHandler _dataHandler;

        public HomeController(ILogger<HomeController> logger , DataHandler dataHandler)
        {
            _logger = logger;
            _dataHandler = dataHandler;
        }

        public IActionResult Index()
        {
            ViewBag.sList = ServicesGet();
            ViewBag.Laundry = _dataHandler.ProductListGet(1);
            ViewBag.HouseHolds = _dataHandler.ProductListGet(2);
            ViewBag.Comforters = _dataHandler.ProductListGet(1002);
            return View();
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