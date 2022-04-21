using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SandS.Helpers
{
    public class BaseClass : Controller
    {
    
        #region Api
        public SelectList ServicesGet()
        {
            var stypes = new List<string>
            {   "All",
                "Laundry",
                "HouseHolds"
            };
            return new SelectList(stypes);
        }
        #endregion
        //public DataHandler _dataHandler;
        //public BaseClass(DataHandler dataHandler)
        //{
        //    _dataHandler = dataHandler;
        //}
        //#region SelectList
        //public SelectList ServicesGet()
        //{
        //    var stypes = _dataHandler.ProductCategoryListGet();

        //    return new SelectList(stypes);
        //}
    }
}
