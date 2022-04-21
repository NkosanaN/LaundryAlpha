using Model;

namespace Service.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Debtors>
    {
        void Update(Debtors debtors);

    }
}
