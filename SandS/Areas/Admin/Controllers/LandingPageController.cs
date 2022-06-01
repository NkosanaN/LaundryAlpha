using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using NToastNotify;
using SandS.Helpers;
using Service.Repository.IRepository;

namespace SandS.Controllers
{
    [Area("Admin")]
    public class LandingPageController : BaseContoller
    {
        private readonly IUnityOfWork _unityofwork;
        private readonly UserManager<IdentityUser> _signInManager;

        public LandingPageController(IUnityOfWork unityofwork, UserManager<IdentityUser> signInManager)
            : base(unityofwork, signInManager)
        {
            _unityofwork = unityofwork;
            _signInManager = signInManager;
        }

        // GET: LandingPageController
        public ActionResult LangingView()
        {
            ViewBag.sList = ServicesGet();
            ViewBag.Laundry = _unityofwork.Product.GetFirstOrDefault(x => x.ProductID == 1);
            ViewBag.HouseHolds = _unityofwork.Product.GetFirstOrDefault(x => x.ProductID == 2);
            ViewBag.Comforters = _unityofwork.Product.GetFirstOrDefault(x => x.ProductID == 3);
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
