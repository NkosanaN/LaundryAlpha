using Model;
using Service.Data;
using Service.Repository.IRepository;


namespace Service.Repository
{
    public class SaleOrderDetailRepository : Repository<SaleOrderDetail>, ISaleOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public SaleOrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(SaleOrderDetail saleorderdetail)
        {
            _db.SaleOrderDetail.Update(saleorderdetail);
        }
    }
}
