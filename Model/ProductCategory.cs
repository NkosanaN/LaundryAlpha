using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class ProductCategory
    {
        [Key]
        public int ProductCatID { get; set; }
        public string Name { get; set; }  = string.Empty;
        public int? ProductRef { get; set; }


    }
}
