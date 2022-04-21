using Service.Data;
using Service.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnityOfWork(ApplicationDbContext db)
        {
            _db = db;
            Company = new CompanyRepository(_db);
            Customer = new DebtorsRepository(_db);
            ProductCategory = new ProductCategoryRepository(_db);
            Product = new ProductRepository(_db);
            SaleOrderHeader = new SaleOrderHeaderRepository(_db);
            SaleOrderDetail = new SaleOrderDetailRepository(_db);
        }

        public ICompanyRepository Company { get; private set; }

        public ICustomerRepository Customer { get; private set; }

        public IProductCategoryRepository ProductCategory { get; private set; }

        public IProductRepository Product { get; private set; }

        public ISaleOrderHeaderRepository SaleOrderHeader { get; private set; }

        public ISaleOrderDetailRepository SaleOrderDetail { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
