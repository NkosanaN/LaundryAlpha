using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class OrderDetail
    {
        [Key]
        public string OrderDetailCode { get; set; } 
        [Display(Name ="Nr")]
        public int Count { get; set; }
        public string? Items { get; set; } = string.Empty;
        public double Price { get; set; }
        [ForeignKey("OrdHeaderCode")]
        public string OrdHeaderCode { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
