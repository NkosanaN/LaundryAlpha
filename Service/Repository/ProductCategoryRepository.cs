using Model;
using Service.Data;
using Service.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ProductCategory productcategory)
        {
            _db.ProductCategory.Update(productcategory);
        }
    }
}
