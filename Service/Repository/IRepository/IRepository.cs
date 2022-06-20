using System.Linq.Expressions;

namespace Service.Repository.IRepository
{
    public interface IRepository<T> where T : class 
    {
        //T - Class
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        IEnumerable<T> GetAll(string? includeProperties = null); 
        void Add(T entity);
        void Delete(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
