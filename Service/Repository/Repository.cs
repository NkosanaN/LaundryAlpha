using Microsoft.EntityFrameworkCore;
using Service.Data;
using Service.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(predicate);

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
