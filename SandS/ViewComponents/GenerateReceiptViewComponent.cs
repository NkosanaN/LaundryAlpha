using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using SandS.Models;
using Service;
using Service.Repository.IRepository;

namespace S_and_S.ViewComponents
{
    public class GenerateReceiptViewComponent : ViewComponent
    {
        private readonly IUnityOfWork _unityofwork;
        public GenerateReceiptViewComponent(IUnityOfWork unityofwork)
        {
            _unityofwork = unityofwork;
        }
        public IViewComponentResult Invoke(string customerinfo, string selectedlines)
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

                return View(receipt);

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
    }
}
