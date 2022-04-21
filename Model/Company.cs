using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? StreetAddress { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
  //      public IFormFile Photo { get; set; }
    }
}
