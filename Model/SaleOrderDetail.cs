using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class SaleOrderDetail
    {
        [Key]
        [StringLength(10)]
        public string SaleOrderDetailCode { get; set; } =string.Empty;
        [Display(Name ="Nr")]
        public int CountId { get; set; }

        [StringLength(50)]
        public string? Items { get; set; } = string.Empty;

       [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }

        public string SaleOrdHeaderCode { get; set; }

        //public string SaleOrdHeaderCode { get; set; } 
        //[ForeignKey("SaleOrdHeaderCode")]
        //[ValidateNever]
        //public SaleOrderHeader SaleOrderHeader { get; set; }

    }
}
