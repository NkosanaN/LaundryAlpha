using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SandS.Helpers
{
    public class BaseClass : Controller
    {
    
        #region SelectList
        public SelectList ServicesGet()
        {
            var stypes = new List<string>
            { 
                "Laundry",
                "HouseHolds",
                "Comforters"
            };
            return new SelectList(stypes);
        }
        #endregion
    }
}
