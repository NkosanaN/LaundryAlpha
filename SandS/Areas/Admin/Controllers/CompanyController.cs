using Microsoft.AspNetCore.Mvc;
using Model;
using NToastNotify;
using Service;
//using Microsoft.AspNetCore.Hosting.;

namespace SandS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly DataHandler _dataHandler;
        private readonly IToastNotification _toastNotification;
        //private readonly ILogger _logger;
        public CompanyController(DataHandler handler,
            IToastNotification toastNotification /*,IHostingEnvironment hosting,ILogger logger*/)
        {
            _dataHandler = handler;
            _toastNotification = toastNotification;
            //_logger = logger;
        }
        // GET: CompanyController
        public ActionResult Index()
        {
            return View(_dataHandler.CompanyListGet());
        }

        // GET: CompanyController/Details/5
        public ActionResult Details(int id)
        {
            return View(_dataHandler.CustomerGetSingle(id));
        }

        // GET: CompanyController/Create
        public ActionResult Upsert(int ?id)
        {
            Company company = new();

            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {
                company = _dataHandler.CompanyGetSingle(id.Value);
                return View(company);
            }
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(Company model)
        {
            try
            {
                string uniqueFileName = string.Empty;

                //if(model.Photo != null) 
                //{

                //}


                //if (model.Id==0)
                //{
                //    if (_dataHandler.AddCompany (model)) 
                //    {
                //        return RedirectToAction("Index");
                //    }
                //}
                //else
                //{
                //    if (_dataHandler.UpdateCustomer(model))
                //    { return RedirectToAction("Index");}
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //_logger.LogError(ex.Message);
                return View();
            }
            return View(model);
        }

        // GET: CompanyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompanyController/Edit/5
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

        // GET: CompanyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompanyController/Delete/5
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
