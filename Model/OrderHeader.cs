using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class OrderHeader
    {
        [Key]
        public string OrdHeaderCode { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress),EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Display(Name ="Nr of Items")]
        public int ItemNr { get; set; }

       [Display(Name = "Total")]
        public double TotalLine { get; set; }
        public List<OrderDetail> OrderLine { get; set; } = new List<OrderDetail>();
    }
}
