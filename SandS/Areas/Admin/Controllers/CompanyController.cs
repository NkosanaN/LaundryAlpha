using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using NToastNotify;
using SandS.Helpers;
using Service;
using Service.Repository.IRepository;
//using Microsoft.AspNetCore.Hosting.;

namespace SandS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : BaseContoller
    {
        private readonly IUnityOfWork _unityofwork;
        private readonly UserManager<ApplicationUser> _signInManager;
        //private readonly ILogger _logger;
        public CompanyController(IUnityOfWork unityofwork, UserManager<ApplicationUser> signInManager
            /*,IHostingEnvironment hosting,ILogger logger*/)
            : base(unityofwork, signInManager)
        {
            _unityofwork = unityofwork;
            _signInManager = signInManager;
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
            if (ModelState.IsValid)
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
                    _unityofwork.Save();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //_logger.LogError(ex.Message);
                    return View();
                }

            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult GetAll() 
        {
            var model = _unityofwork.Company.GetAll();

            return Ok(model);
        }
    }
}
