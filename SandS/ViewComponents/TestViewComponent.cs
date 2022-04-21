using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using Service;
using Stripe.Checkout;

namespace SandS.ViewComponents
{
    public class TestViewComponent : ViewComponent
    {
        private readonly DataHandler _handler;
        public SelectList ServicesGet()
        {
            var stypes = new List<string>
            {   "All",
                "Laundry",
                "HouseHolds"
            };
            return new SelectList(stypes);
        }
        public TestViewComponent(DataHandler handler)
        {
            _handler = handler;
        }
        public IViewComponentResult Invoke(string title)
        {
            ViewBag.Title = title;
            ViewBag.Laundry = _handler.ProductListGet(1);
            ViewBag.HouseHolds = _handler.ProductListGet(2);
            ViewBag.sList = ServicesGet();

            return View(new Debtors());
        }
    }
}
