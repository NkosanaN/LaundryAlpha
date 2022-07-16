using Microsoft.AspNetCore.Mvc;
using Model;
using Rotativa.AspNetCore;

namespace S_and_S.ViewComponents
{
    public class GenerateReceiptViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<OrderHeader> model)
        {
            try
            {   
                //ViewBag.oLine = model.FirstOrDefault().OrderLine;

                //return RedirectToAction()

                //return Invoice(model.FirstOrDefault());
                return Invoke(model);
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<ViewAsPdf> CreatePfd(OrderHeader model) 
        {
            return  await Task.FromResult(new ViewAsPdf(model) 
            {
                FileName = "Receipt.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                CustomSwitches =
                          "--footer-center \" Created Date : " +
                          DateTime.Now.Date.ToString("yyyy/MM/dd") + " Page: [page]/[toPage] \"" +
                          " --footer-line --footer-font-size \"12\" --footer-spacing 1 --footer-font-name \" Segoe UI\""
            });
        }
        public async Task<IActionResult> Invoice(OrderHeader model) 
        {
            var pdf = await CreatePfd(model);
            return pdf;
        }

    }
}
