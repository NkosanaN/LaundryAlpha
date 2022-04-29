using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AuditTray
    {
        [Key]
        public string OrdHeaderCode { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Items { get; set; } = string.Empty;
        public double Price { get; set; }

    }
}
