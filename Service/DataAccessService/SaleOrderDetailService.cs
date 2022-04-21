using Microsoft.Extensions.Caching.Memory;
using Model;
using System.Data;

namespace Service
{
    public partial class DataHandler
    {
        private SaleOrderDetail GetSalesDetailFromDataRow(DataRow row)
        {
            return new SaleOrderDetail
            {
                SaleOrderDetailCode = row["SaleOrderDetailCode"].ToString(),
                Items = row["Items"].ToString(),
                CountId = (int)row["CountId"],
                Price = (decimal)row["Price"],
                SaleOrdHeaderCode = row["SaleOrdHeaderCode"].ToString(),
            };
        }
        public List<SaleOrderDetail> SalesDetailListGet()
        {
            var cacheKey = "salesdetailList";
            string sql = "exec sp_salesorderdetail_data @Mode=0";
            if (memorycache.TryGetValue(cacheKey, out List<SaleOrderDetail> data))
            {
                return data;

                //setting cache
                //var cacheExpiryOpt = new MemoryCacheEntryOptions()
                //{
                //    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                //    Priority = CacheItemPriority.High,
                //    SlidingExpiration = TimeSpan.FromSeconds(20)
                //};
            }
            var salesdetailList = new List<SaleOrderDetail>();
            var dt = Util.Select(sql);
            foreach (DataRow row in dt.Rows)
            {
                salesdetailList.Add(GetSalesDetailFromDataRow(row));
            }
            memorycache.Set(cacheKey, salesdetailList);
            return salesdetailList;
        }
        public List<SaleOrderDetail> SalesDetailListGet(string id )
        {
            var cacheKey = "salesdetailList";

            string sql = $"exec sp_salesorderdetail_data @Mode=1, @id='{id}'";
          
            var salesdetailList = new List<SaleOrderDetail>();
            var dt = Util.Select(sql);
            foreach (DataRow row in dt.Rows)
            {
                salesdetailList.Add(GetSalesDetailFromDataRow(row));
            }
            memorycache.Set(cacheKey, salesdetailList);
            return salesdetailList;
        }
        public SaleOrderDetail SalesDetailGetSingle(string Id)
        {
            var sql = $"exec sp_salesorderdetail_data @Mode=1, @id='{Id}'";

            var dt = Util.Select(sql);
            if (dt.Rows.Count == 0)
            {
                return new SaleOrderDetail();
            }
            var r = GetSalesDetailFromDataRow(dt.Rows[0]);
            return r;
        }
  
    }
}
