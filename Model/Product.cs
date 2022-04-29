using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public  class Product
    {
        public int ProductID { get; set; } 

        [Display(Name ="Name")]
        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Price")]
        public double ListPrice { get; set; }
        public string? ImgUrl { get; set; } = string.Empty;
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        public int ProductCategoryID { get; set; }
        [ForeignKey("ProductCategoryID")]
        public ProductCategory ProductCategory { get; set; } = new ProductCategory();
    }
}
