using Service.Data;
using Service.Repository.IRepository;

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
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            AuditTray = new AuditTrayRepository(_db);
        }
        public ICompanyRepository Company { get; private set; }
        public ICustomerRepository Customer { get; private set; }       
        public IProductCategoryRepository ProductCategory { get; private set; }
        public IProductRepository Product { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IAuditTrayRepository AuditTray { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
