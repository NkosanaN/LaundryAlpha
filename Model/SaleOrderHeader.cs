using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class SaleOrderHeader
    {
        [Key]
        [Display(Name = "Order Code"),StringLength(10)]
        public string SaleOrdHeaderCode { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [StringLength(50)]
        public string Surname { get; set; } = string.Empty;
        [StringLength(50),DataType(DataType.EmailAddress),EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Display(Name ="Nr of Items")]
        public int ItemNr { get; set; }

       [Display(Name = "Total")]
        public decimal TotalLine { get; set; }
        public List<SaleOrderDetail> SaleOrderDetail { get; set; }
    }
}
