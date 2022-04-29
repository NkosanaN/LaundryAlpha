using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailCode { get; set; } 
        [Display(Name ="Nr")]
        public int Count { get; set; }
        public string? Items { get; set; } = string.Empty;
        public double Price { get; set; }
        public string OrdHeaderCode { get; set; }
        [ForeignKey("OrdHeaderCode")]
        public OrderHeader OrderHeader { get; set; }
        public bool isCompleted { get; set; }
    }
}
