using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SandS.Helpers;
using Service;

namespace SandS.Controllers
{
    [Area("Admin")]
    public class LandingPageController : BaseClass
    {
        private readonly DataHandler _dataHandler;
        public LandingPageController(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        // GET: LandingPageController
        public ActionResult LangingView()
        {

            ViewBag.sList = ServicesGet();

            ViewBag.Laundry = _dataHandler.ProductListGet(1);
            ViewBag.HouseHolds = _dataHandler.ProductListGet(2);
            ViewBag.Comforters = _dataHandler.ProductListGet(1002);
            ViewBag.sList = ServicesGet();

            return View();
        }

        // GET: LandingPageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LandingPageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LandingPageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LandingPageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LandingPageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LandingPageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LandingPageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
