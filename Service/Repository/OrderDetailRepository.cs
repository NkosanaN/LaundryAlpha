using Model;
using Service.Data;
using Service.Repository.IRepository;


namespace Service.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(OrderDetail saleorderdetail)
        {
            _db.OrderDetail.Update(saleorderdetail);
        }
    }
}
