using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Address
    {
        [Display(Name = "Address 1 "), Required]
        public string StreetAddress1 { get; set; }
        [Display(Name = "Address 2 "), Required]
        public string? StreetAddress2 { get; set; }
        
    }
}
