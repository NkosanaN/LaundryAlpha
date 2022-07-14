using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using NToastNotify;
using Rotativa.AspNetCore;
using SandS.Helpers;
using SandS.Models;
using Service;

using Service.Repository.IRepository;

namespace SandS.Controllers
{
   [Area("Customers")]
    public class DebtorController : BaseContoller
    {
        private readonly IUnityOfWork _unityofwork;
        private readonly UserManager<ApplicationUser> _signInManager;
        public DebtorController(IUnityOfWork unityofwork, UserManager<ApplicationUser> signInManager) :base(unityofwork, signInManager)
        {
            _unityofwork = unityofwork;
            _signInManager = signInManager;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GenerateReceipt(string selectedlines)
        {
            GenerateReceiptViewModel receipt = new();

            try
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(selectedlines);
                string code = RandomString.GenerateOrderCode(5);

                OrderHeader header = new OrderHeader()
                {
                    OrdHeaderCode = code,
                    FirstName = FirstName,
                    Surname = LastName,
                    Email = Email,
                    ItemNr = products.Count,
                    TotalLine = products.Select(x => x.ListPrice).Sum(),
                    OrderLine = new()
                };
                _unityofwork.OrderHeader.Add(header);

                for (int i = 0; i < products.Count; i++)
                {
                    _unityofwork.OrderDetail.Add(new OrderDetail()
                    {
                        OrdHeaderCode = code,
                        Count = i + 1,
                        Items = products[i].ProductName,
                        Price = products[i].ListPrice,
                    });
                }
                _unityofwork.Save();

                //receipt.customer = customer;
                //receipt.product = products;
                //ViewBag.ReceiptNr = code;

                Notify("Successful created new Order","Successful", type: NotificationType.success);
                return RedirectToAction("UserDashboard", "Home", new { area = "" });
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex.Message);
                throw;
            }

        }

        public ActionResult InvoicePdf()
        {
            GenerateReceiptViewModel receipt = new();
            try
            {
               
                string code = RandomString.GenerateOrderCode(5);

                //OrderHeader header = new OrderHeader()
                //{
                //    OrdHeaderCode = code,
                //    FirstName = customer.FirstName,
                //    Surname = customer.LastName,
                //    Email = Email,
                //    ItemNr = products.Count,
                //    TotalLine = products.Select(x => x.ListPrice).Sum(),
                //    OrderLine = new()
                //};
                //_unityofwork.OrderHeader.Add(header);

                //for (int i = 0; i < products.Count; i++)
                //{
                //    _unityofwork.OrderDetail.Add(new OrderDetail()
                //    {
                //        OrdHeaderCode = code,
                //        Count = i + 1,
                //        Items = products[i].ProductName,
                //        Price = products[i].ListPrice,
                //    });
                //}
                ////_unityofwork.Save();
                //receipt.customer = customer;
                //receipt.product = products;
                //ViewBag.ReceiptNr = code;

                //Notify("Successful created order", "Successful created order", type: NotificationType.success);
                //return View("Index");

            
                var potrait = new ViewAsPdf()
                {
                    FileName = "Receipt.pdf",
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
    }
}
