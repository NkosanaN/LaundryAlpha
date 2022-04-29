using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using Service;
using Service.Repository.IRepository;
using Stripe.Checkout;

namespace SandS.ViewComponents
{
    public class TestViewComponent : ViewComponent
    {
        private readonly IUnityOfWork _unityofwork;
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
        public TestViewComponent(IUnityOfWork unityofwork)
        {
            _unityofwork = unityofwork;
        }
        public IViewComponentResult Invoke(string title)
        {
            ViewBag.Title = title;
            ViewBag.Laundry = _unityofwork.Product.GetAll().Where(x => x.ProductCategoryID == 1).ToList();
            ViewBag.HouseHolds = _unityofwork.Product.GetAll().Where(x => x.ProductCategoryID == 2).ToList();
            ViewBag.Comforters = _unityofwork.Product.GetAll().Where(x => x.ProductCategoryID == 3).ToList();

            ViewBag.sList = ServicesGet();

            return View(new Debtors());
        }
    }
}
