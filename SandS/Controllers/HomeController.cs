using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using SandS.Helpers;
using SandS.Models;
using Service;
using Service.Repository.IRepository;
using System.Diagnostics;

namespace S_and_S.Controllers
{
    public class HomeController : BaseContoller
    {
        private readonly IUnityOfWork _unityofwork;
        private readonly UserManager<ApplicationUser> _signInManager;

        public HomeController(IUnityOfWork unityOfWork, UserManager<ApplicationUser> signInManager) :base(unityOfWork, signInManager)
        {
            _unityofwork = unityOfWork;
            _signInManager = signInManager;
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
            ViewBag.Laundry = _unityofwork.Product.GetAll().Where(x => x.ProductCategoryID == 1).ToList();
            ViewBag.HouseHolds = _unityofwork.Product.GetAll().Where(x => x.ProductCategoryID == 2).ToList();
            ViewBag.Comforters = _unityofwork.Product.GetAll().Where(x => x.ProductCategoryID == 3).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult UserDashboard() 
        {
            ViewData["isExternalUser"] = true;
            getOrderList();
            return View();
        }
        public IActionResult AdminDashboard()
        {
            //return RedirectToAction("Index", "Category", new { area = "Admin" });
            return View();
        }
        public IEnumerable<OrderHeader> getOrderList() 
        {
            var getUserOrders = _unityofwork.OrderHeader.GetAll("OrderLine")
                               .Where(x => x.Email.ToLower() == Email.ToLower());
            ViewBag.NumberOfOrder = getUserOrders.Count();
            return getUserOrders;
        }

        public IActionResult getUserData() 
        {
            return Json(getOrderList());
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