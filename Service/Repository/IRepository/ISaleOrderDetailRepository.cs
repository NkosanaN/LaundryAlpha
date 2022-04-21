using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository.IRepository
{
    public interface ISaleOrderDetailRepository : IRepository<SaleOrderDetail>
    {
        void Update(SaleOrderDetail saleorderdetail);
    }
}
