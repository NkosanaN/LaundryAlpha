using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository.IRepository
{
    public interface IUnityOfWork
    {
        ICompanyRepository Company { get; }
        ICustomerRepository Customer { get; }
        IProductCategoryRepository ProductCategory { get; }
        IProductRepository Product { get; }
        ISaleOrderHeaderRepository SaleOrderHeader { get; }
        ISaleOrderDetailRepository SaleOrderDetail { get; }
        void Save();
    }
}
