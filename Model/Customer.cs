using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Debtors
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name"), StringLength(50), Required]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Last Name"), StringLength(50), Required]
        public string LastName { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Mobile Number")]
        public string MobilePhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Date"), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime CreationDate { get; set; } =  DateTime.Now;

        [Display(Name = "Collection (Yes/No)")]
        public bool isCollected { get; set; }

        [Display(Name = "Select Service"),Required]
        public string Catergory { get; set; } = string.Empty;

        [Display(Name = "Address 1 "), Required]
        public string StreetAddress1 { get; set; } = string.Empty;
        [Display(Name = "Address 2 "), Required]
        public string? StreetAddress2 { get; set; }
      //  public List<Address> Addresses { get; set; }
    }
}
