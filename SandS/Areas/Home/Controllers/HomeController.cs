using Microsoft.AspNetCore.Mvc;
using SandS.Helpers;
using SandS.Models;
using Service;
using Service.Repository.IRepository;
using System.Diagnostics;

namespace SandS.Controllers
{
    public class HomeController : BaseClass
    {
        private readonly IUnityOfWork _unityofwork;
        public HomeController(IUnityOfWork unityofwork)
        {
            _unityofwork = unityofwork;
        }

        public IActionResult Index()
        {
            ViewBag.sList = ServicesGet();
            ViewBag.Laundry =  _unityofwork.Product.GetFirstOrDefault(x => x.ProductID == 1);
            ViewBag.HouseHolds = _unityofwork.Product.GetFirstOrDefault(x => x.ProductID == 2);
            ViewBag.Comforters = _unityofwork.Product.GetFirstOrDefault(x => x.ProductID == 1002);
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