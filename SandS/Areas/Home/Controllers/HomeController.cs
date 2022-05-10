using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SandS.Helpers;
using SandS.Models;
using Service;
using Service.Repository.IRepository;
using System.Diagnostics;

namespace SandS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnityOfWork _unityofwork;
        public HomeController(IUnityOfWork unityofwork)
        {
            _unityofwork = unityofwork;
        }
        public SelectList ServicesGet()
        {
            var stypes = new List<string>
            {
                "Laundry",
                "HouseHolds",
                "Comforters"
            };
            return new SelectList(stypes);
        }
        public IActionResult Index()
        {
            ViewBag.sList = ServicesGet();
            ViewBag.Laundry    = _unityofwork.Product.GetAll().Where(x => x.ProductCategoryID == 1).ToList();
            ViewBag.HouseHolds = _unityofwork.Product.GetAll().Where(x => x.ProductCategoryID == 2).ToList();
            ViewBag.Comforters = _unityofwork.Product.GetAll().Where(x => x.ProductCategoryID == 3).ToList();
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