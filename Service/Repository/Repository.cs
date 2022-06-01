using Microsoft.EntityFrameworkCore;
using Service.Data;
using Service.Repository.IRepository;
using System.Linq.Expressions;

namespace Service.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly  ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
           //_db.OrderHeader.Include(u=>u.OrderLine).Include() 
           //include navigation of FK only in applicationDb_Context you can include morethan one property
            this.dbSet = _db.Set<T>();
        }

        //includeProp => "OrderDetails,Category"
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(includeProperties != null) 
            {
                foreach (var includProp in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includProp);
                }
            }
            return query.ToList();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate , string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(predicate);
            if (includeProperties != null)
            {
                foreach (var includProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includProp);
                }
            }
            return query.FirstOrDefault();
        }
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
