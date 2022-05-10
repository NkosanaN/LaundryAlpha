﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = UtilityConstant.Role_User_Admin)]
    public class CategoryController : BaseContoller
    {
        private readonly IUnityOfWork _unityofwork;

        public CategoryController(IUnityOfWork unityofwork,
            IToastNotification toast):base(unityofwork, toast)
        {
            _unityofwork = unityofwork;
        }
        public ActionResult Index()
        {
            IEnumerable<OrderHeader> obj = _unityofwork.OrderHeader.GetAll();
            obj ??= new List<OrderHeader>();
            return View(obj);
        }
        public ActionResult Edit(string ordcode) 
        {
            return View(_unityofwork.OrderDetail.GetAll().Where(x => x.OrdHeaderCode == ordcode));
        }

        public ActionResult Completed(string ordcode)
        {
            //var complete = _unityofwork.OrderHeader.Include("OrderDetail").ToList();
            var model = _unityofwork.OrderHeader.GetFirstOrDefault(u => u.OrdHeaderCode == ordcode);
            model.isCompleted = true;

            //foreach (var item in model.OrderLine)
            //{
            //    _unityofwork.AuditTray.Add(new AuditTray
            //    {
            //        OrdHeaderCode = item.OrdHeaderCode,
            //        Price = item.Price,
            //        Name = model.Name,
            //        Items = item.Items
            //    });
            //}
            _unityofwork.Save();
            return RedirectToAction("Index");

        }

        // GET: HomeController1/Edit/5
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

                Notify("Successful created order", "Successful created order", type: NotificationType.success);
                return View("Index");

                //return View(receipt);
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
        public ActionResult Striple(string customerinfo, string selectedlines)
        {
            GenerateReceiptViewModel receipt = new();
            try
            {
                var customer = JsonConvert.DeserializeObject<Debtors>(customerinfo);
                var products = JsonConvert.DeserializeObject<List<Product>>(selectedlines);
                string domain = "https://localhost:44342/";

                OrderHeader header = new OrderHeader()
                {
                    Name = customer.FirstName,
                    Surname = customer.LastName,
                    Email = customer.Email,
                    ItemNr = products.Count,
                    TotalLine = products.Select(x => x.ListPrice).Sum(),
                };
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = domain + $"admin/category/orderconfirmation?id={header.OrdHeaderCode}",
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

            var product = _unityofwork.Product.GetFirstOrDefault(x => x.ProductID == id);
            var service = new SessionService();
            Session session = service.Get(product.SessionId);

            if (session.PaymentStatus.ToLower().Contains("paid"))
            {
                //update statues
                //save()
            }


            return View(id);
        }
        public ActionResult Details(int id)
        {

            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //_shoppingCart.ApplicationUserId = claim.Value;
            return View();
            //   return View(_dataHandler.CustomerGetSingle(id));
        }



    }
}
