using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using NToastNotify;
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
        private readonly UserManager<IdentityUser> _signInManager;
        public DebtorController(IUnityOfWork unityofwork, UserManager<IdentityUser> signInManager) :base(unityofwork, signInManager)
        {
            _unityofwork = unityofwork;
            _signInManager = signInManager;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerateReceipt(string customerinfo, string selectedlines)
        {
            GenerateReceiptViewModel receipt = new();
            try
            {
                var customer = JsonConvert.DeserializeObject<Debtors>(customerinfo);
                var products = JsonConvert.DeserializeObject<List<Product>>(selectedlines);
                string code = RandomString.GenerateOrderCode(5);

                OrderHeader header = new OrderHeader()
                {
                    OrdHeaderCode = code,
                    Name = customer.FirstName,
                    Surname = customer.LastName,
                    Email = customer.Email,
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
                //_unityofwork.Save();

                receipt.customer = customer;
                receipt.product = products;
                ViewBag.ReceiptNr = code;

                Notify("Successful created new Order", type: NotificationType.error);
                return PartialView("_GenerateReceipt", receipt);

                //var potrait = new ViewAsPdf(receipt)
                //{
                //    FileName = "receipt.pdf",
                //    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                //    PageSize = Rotativa.AspNetCore.Options.Size.A4
                //};
                //return potrait;
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex.Message);
                throw;
            }

        }

        //public ActionResult PostPO(string customerinfo, string selectedlines)
        //{
        //    try
        //    {
        //        var customer = JsonConvert.DeserializeObject<Debtors>(customerinfo);
        //        var service = JsonConvert.DeserializeObject<List<Product>>(selectedlines);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }


        //    return Ok();
        //}
    }
}
