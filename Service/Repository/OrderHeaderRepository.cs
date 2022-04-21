using Model;
using Service.Data;
using Service.Repository.IRepository;

namespace Service.Repository
{

    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader saleorderheader)
        {
            _db.OrderHeader.Update(saleorderheader);
        }
    }
}
