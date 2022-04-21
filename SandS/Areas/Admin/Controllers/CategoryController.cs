using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using Newtonsoft.Json;
using NToastNotify;
using Rotativa.AspNetCore;
using SandS.Helpers;
using SandS.Models;
using Service;
using Service.Repository.IRepository;
using Stripe.Checkout;
using System.Security.Claims;

namespace SandS.Controllers
{
    [Area("Admin")]
    [Authorize(Roles= UtilityConstant.Role_User_Admin)]
    public class CategoryController : BaseClass
    {
        private readonly DataHandler _dataHandler;
        private readonly IUnityOfWork _unityofwork;
        private readonly IToastNotification _toastNotification;
         // GET: HomeController1

        public CategoryController(DataHandler dataHandler,IUnityOfWork unityofwork,
            IToastNotification toastNotification)
        {
            _dataHandler = dataHandler;
            _unityofwork = unityofwork;
            _toastNotification = toastNotification;
        }
        public ActionResult Index()
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //ShoppingCart _shoppingCart = new();
            //_shoppingCart.ApplicationUserId = claim.Value;

            //  ViewData["routeInfo"] = ControllerContext.MyDisplayRouteInfo();

            //var model = _dataHandler.SalesHeaderListGet();
            //model ??= new List<SaleOrderHeader>();

            IEnumerable<SaleOrderHeader> obj = _unityofwork.SaleOrderHeader.GetAll();
            obj ??= new List<SaleOrderHeader>();
            return View(obj);

        }

        public ActionResult SaleOrderDetail(int Id) 
        {
            return View();
            //return View(_unityofwork.SaleOrderDetail.GetFirstOrDefault(x => x.Id == id));
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {

            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //_shoppingCart.ApplicationUserId = claim.Value;
            return View();
         //   return View(_dataHandler.CustomerGetSingle(id));
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            ViewBag.sList = ServicesGet();
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Debtors model)
        {
            try
            {
                var customerObj = new Debtors()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    MobilePhoneNumber = model.MobilePhoneNumber,
                    Catergory  = model.Catergory
                };

                if (customerObj.isCollected)
                    customerObj.StreetAddress1 = "Mamelodi";
                else
                    customerObj.StreetAddress1 = model.StreetAddress1;
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: HomeController1/Edit/5
        public ActionResult GenerateReceipt(string customerinfo, string selectedlines)
        {
            GenerateReceiptViewModel receipt = new();
            try
            {
                var customer = JsonConvert.DeserializeObject<Debtors>(customerinfo);
                var products = JsonConvert.DeserializeObject<List<Product>>(selectedlines);

                SaleOrderHeader header = new SaleOrderHeader()
                {
                    Name = customer.FirstName,
                    Surname = customer.LastName,
                    Email = customer.Email,
                    ItemNr = products.Count,
                    TotalLine = products.Select(x => x.ListPrice).Sum(),
                    SaleOrderDetail = new()
                };
                for (int i = 0; i < products.Count; i++)
                {
                    header.SaleOrderDetail.Add(new SaleOrderDetail()
                    {
                        CountId = i,
                        Items = products[i].ProductName,
                        Price = products[i].ListPrice,
                    });
                }
                receipt.customer = customer;
                receipt.product = products;

                var potrait = new ViewAsPdf(receipt)
                {
                    FileName = "receipt.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4
                };
                return potrait;
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex.Message);
                throw;
            }

        }
        public ActionResult Striple(string customerinfo, string selectedlines)
        {
            GenerateReceiptViewModel receipt = new();
            try
            {
                var customer = JsonConvert.DeserializeObject<Debtors>(customerinfo);
                var products = JsonConvert.DeserializeObject<List<Product>>(selectedlines);
                string domain = "https://localhost:44342/";

                SaleOrderHeader header = new SaleOrderHeader()
                {
                    Name = customer.FirstName,
                    Surname = customer.LastName,
                    Email = customer.Email,
                    ItemNr = products.Count,
                    TotalLine = products.Select(x => x.ListPrice).Sum(),
                    SaleOrderDetail = new()
                };
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = domain + $"admin/category/orderconfirmation?id={header.SaleOrdHeaderCode}",
                    CancelUrl = domain + "admin/category/index",
                };
                for (int i = 0; i < products.Count; i++)
                {
                    var sessionLineItem = new SessionLineItemOptions()
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(products[i].ListPrice * 100),//20.00 => 200
                            Currency = "usd", //ZAR
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = products[i].ProductName
                            },
                        },
                        Quantity = i,
                    };
                    options.LineItems.Add(sessionLineItem);
                }


                var service = new SessionService();
                Session session = service.Create(options);
                products.FirstOrDefault().SessionId = session.Id;
                products.FirstOrDefault().PaymentIntentId = session.PaymentIntentId;

                Response.Headers.Add("Location",session.Url);
                return new StatusCodeResult(303);
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex.Message);
                throw;
            }
           //  return View(receipt);
        }

        public ActionResult OrderConfirmation(int id) 
        {

            var product = _dataHandler.ProductGetSingle(id);
            var service = new SessionService();
            Session session = service.Get(product.SessionId);

            if (session.PaymentStatus.ToLower().Contains("paid"))
            {
                //update statues
                //save()
            }


            return View(id);
        }

       
    }
}
