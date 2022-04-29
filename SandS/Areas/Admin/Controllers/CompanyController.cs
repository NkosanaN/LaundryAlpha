using Microsoft.AspNetCore.Mvc;
using Model;
using NToastNotify;
using Service;
using Service.Repository.IRepository;
//using Microsoft.AspNetCore.Hosting.;

namespace SandS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnityOfWork _unityofwork;

        //private readonly ILogger _logger;
        public CompanyController(IUnityOfWork unityofwork/*,IHostingEnvironment hosting,ILogger logger*/)
        {
            _unityofwork = unityofwork;
            //_logger = logger;
        }
        // GET: CompanyController
        public ActionResult Index()
        {
            IEnumerable<Company> obj = _unityofwork.Company.GetAll();
            return View(obj);

        }

        // GET: CompanyController/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var customer = _unityofwork.Customer.GetFirstOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: CompanyController/Create
        public ActionResult Upsert(int? id)
        {
            Company company = new();

            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {
                company = _unityofwork.Company.GetFirstOrDefault(x => x.Id == id.Value);
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


                if (model.Id == 0)
                {
                    _unityofwork.Company.Add(model);
                }
                else
                {
                    _unityofwork.Company.Update(model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //_logger.LogError(ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
