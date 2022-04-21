using Model;
using Service.Data;
using Service.Repository.IRepository;

namespace Service.Repository
{

    public class SaleOrderHeaderRepository : Repository<SaleOrderHeader>, ISaleOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public SaleOrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SaleOrderHeader saleorderheader)
        {
            _db.SaleOrderHeader.Update(saleorderheader);
        }
    }
}
