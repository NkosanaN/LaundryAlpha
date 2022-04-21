using Model;
using Service.Data;
using Service.Repository.IRepository;

namespace Service.Repository
{
    public class DebtorsRepository : Repository<Debtors>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public DebtorsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Debtors debtors)
        {
            _db.Customer.Update(debtors);
        }
    }
}
