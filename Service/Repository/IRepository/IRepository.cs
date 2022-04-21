using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository.IRepository
{
    public interface IRepository<T> where T : class 
    {
        //T - Class
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll(); 
        void Add(T entity);
        void Delete(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
