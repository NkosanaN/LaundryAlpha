using Model;
using Service.Data;
using Service.Repository.IRepository;

namespace Service.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Company company)
        {
            _db.Company.Update(company);
        }
    }
}
