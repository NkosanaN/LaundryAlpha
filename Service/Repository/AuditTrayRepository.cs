using Model;
using Service.Data;
using Service.Repository.IRepository;

namespace Service.Repository
{
    public class AuditTrayRepository : Repository<AuditTray>, IAuditTrayRepository
    {
        private readonly ApplicationDbContext _db;
        public AuditTrayRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(AuditTray audittray)
        {
            _db.AuditTray.Update(audittray);
        }
    }
}
