using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;
using Service.Data;
using Service.Dbinitializer;
using Service.Repository.IRepository;

namespace Service.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnityOfWork _unityOfWork;
        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db,
            IUnityOfWork unityOfWork)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _unityOfWork = unityOfWork;
        }
        public void Initialize()
        {
            //migrations if they're are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            //create roles if they're are not create
            if (!_roleManager.RoleExistsAsync(UtilityConstant.Role_User_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(UtilityConstant.Role_User_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UtilityConstant.Role_User_Ind)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UtilityConstant.Role_User_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UtilityConstant.Role_User_Comp)).GetAwaiter().GetResult();

                //if roles are not  created,then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@dotnet.com",
                    Email = "admin@dotnet.com",
                    Name = "Nkosana Ndlela",
                    PhoneNumber = "0733109576",
                    StreetAddress = "2437 Ave",
                    State = "PTA",
                    PostalCode = "0001",
                    City = "PTA"
                }, "Admin123*").GetAwaiter().GetResult();

                //We need to retrieve created Admin
              //  ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@dotnet.com");

                //Assigned user to role
                //_userManager.AddToRoleAsync(user, UtilityConstant.Role_User_Admin).GetAwaiter().GetResult();
                PopulateMissingV();
            }
            return;
        
        }
        public void PopulateMissingV()
        {
            var pCategory = _unityOfWork.ProductCategory.GetAll();
            var Product = _unityOfWork.Product.GetAll();

            if (pCategory.Count() == 0)
            {
                _unityOfWork.ProductCategory.Add(new ProductCategory { Name = "Laundry"  });
                _unityOfWork.ProductCategory.Add(new ProductCategory { Name = "HouseHolds"  });
                _unityOfWork.ProductCategory.Add(new ProductCategory { Name = "Comforters" });
            }

            if (Product.Count() == 0)
            {
                _unityOfWork.Product.Add(new Product { ProductName = "Wash Only", ListPrice = 180.00, ImgUrl = string.Empty, ProductCategoryID = 1 });
                _unityOfWork.Product.Add(new Product { ProductName = "Wash Dry and Fold", ListPrice = 200.00, ImgUrl = string.Empty, ProductCategoryID = 1 }); //Wash, Dry & Fold => & cause issue
                _unityOfWork.Product.Add(new Product { ProductName = "Iron Only", ListPrice = 15.00, ImgUrl = string.Empty, ProductCategoryID = 1 });
                _unityOfWork.Product.Add(new Product { ProductName = "Wash Iron and Fold", ListPrice = 190.00, ImgUrl = string.Empty, ProductCategoryID = 1 });
                _unityOfWork.Product.Add(new Product { ProductName = "Blankets (Single Ply)", ListPrice = 180.00, ImgUrl = string.Empty, ProductCategoryID = 2 });
                _unityOfWork.Product.Add(new Product { ProductName = "Blanket (Double Ply)", ListPrice = 200.00, ImgUrl = string.Empty, ProductCategoryID = 2 });
                _unityOfWork.Product.Add(new Product { ProductName = "Double/Queen", ListPrice = 200.00, ImgUrl = string.Empty, ProductCategoryID = 3 });
                _unityOfWork.Product.Add(new Product { ProductName = "Single", ListPrice = 190.00, ImgUrl = string.Empty, ProductCategoryID = 3 });
                
            }
            _unityOfWork.Save();
        }
    }
}
