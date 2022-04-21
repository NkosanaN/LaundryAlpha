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
            {   "All",
                "Laundry",
                "HouseHolds"
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
            ViewBag.Laundry = _unityofwork.Product.GetFirstOrDefault(x => x.ProductID == 1);
            ViewBag.HouseHolds = _unityofwork.Product.GetFirstOrDefault(x => x.ProductID == 2);
            ViewBag.sList = ServicesGet();

            return View(new Debtors());
        }
    }
}
