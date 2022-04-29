namespace Service.Repository.IRepository
{
    public interface IUnityOfWork
    {
        ICompanyRepository Company { get; }
        ICustomerRepository Customer { get; }
        IProductCategoryRepository ProductCategory { get; }
        IProductRepository Product { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        IAuditTrayRepository AuditTray { get; }
        void Save();
    }
}
