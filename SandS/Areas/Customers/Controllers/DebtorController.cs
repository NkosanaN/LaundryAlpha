using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using NToastNotify;
using Service;

namespace SandS.Controllers
{
    [Area("Customer")]
    public class DebtorController : Controller
    {
        private readonly IToastNotification _toastNotification;
        public DebtorController(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostPO(string customerinfo, string selectedlines)
        {
            try
            {
                var customer = JsonConvert.DeserializeObject<Debtors>(customerinfo);
                var service = JsonConvert.DeserializeObject<List<Product>>(selectedlines);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            

            return Ok();
        }
    }
}
