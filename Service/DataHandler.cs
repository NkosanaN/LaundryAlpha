using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public partial class DataHandler
    {
        public Utils Util { get; set; }
        public IMemoryCache memorycache { get; set; }
        public DataHandler(Utils util, IMemoryCache _memorycache)
        {
            Util = util;
            memorycache = _memorycache;
        }
    }
}
