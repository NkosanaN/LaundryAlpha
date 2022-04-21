using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Service.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ):base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<ShoppingCart> ShoppingCart { get; set; }
        //public DbSet<Customer> Customer { get; set; }
        public DbSet<SaleOrderHeader> SaleOrderHeader { get; set; }
        public DbSet<SaleOrderDetail> SaleOrderDetail { get; set; }
    }
}
