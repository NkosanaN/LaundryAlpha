using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StripeSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Publishablekey { get; set; } = string.Empty;
    }
}
