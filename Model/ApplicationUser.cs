using Microsoft.AspNetCore.Identity;

namespace Model
{
    public class ApplicationUser : IdentityUser
    {
        //public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? StreetAddress { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        //public string? State { get; set; } = string.Empty;
        //public string? PostalCode { get; set; } = string.Empty;
        //public int? CompanyId { get; set; }
        //[ForeignKey("CompanyId")]
        //[ValidateNever]
        //public Company Company { get; set; }
    }
}
