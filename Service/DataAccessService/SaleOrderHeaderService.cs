using Microsoft.Extensions.Caching.Memory;
using Model;
using System.Data;

namespace Service
{
    public partial class DataHandler
    {
        private SaleOrderHeader GetSalesHeaderFromDataRow(DataRow row)
        {
            return new SaleOrderHeader
            {
                SaleOrdHeaderCode = row["SaleOrdHeaderCode"].ToString(),
                Name = row["Name"].ToString(),
                Surname = row["Surname"].ToString(),
                Email = row["Email"].ToString(),
                ItemNr = (int)row["ItemNr"],
                TotalLine = Convert.ToInt16((decimal)row["TotalLine"])
            };
        }
        public List<SaleOrderHeader> SalesHeaderListGet()
        {
            var cacheKey = "salesheaderList";
            if (memorycache.TryGetValue(cacheKey, out List<SaleOrderHeader> data))
            {
                return data;
                //setting cache https://www.c-sharpcorner.com/article/caching-mechanism-in-asp-net-core/
                //var cacheExpiryOpt = new MemoryCacheEntryOptions()
                //{
                //    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                //    Priority = CacheItemPriority.High,
                //    SlidingExpiration = TimeSpan.FromSeconds(20)
                //};
            }
            var sql = "exec sp_salesorderheader_data @Mode=0";
            var dt = Util.Select(sql);
            var salesheaderList = new List<SaleOrderHeader>();
            foreach (DataRow row in dt.Rows)
            {
                salesheaderList.Add(GetSalesHeaderFromDataRow(row));
            }
            memorycache.Set(cacheKey, salesheaderList);
            return salesheaderList;
        }
        public SaleOrderHeader SalesHeaderGetSingle(string Id)
        {
            var sql = $"exec sp_salesorderheader_data @Mode=1, @SaleOrdHeaderCode={Id}";

            var dt = Util.Select(sql);
            if (dt.Rows.Count == 0)
            {
                return new SaleOrderHeader();
            }
            var r = GetSalesHeaderFromDataRow(dt.Rows[0]);
            return r;
        }
        public bool AddSaleOrderHeader(SaleOrderHeader data)
        {
            bool results = false;
            try
            {
                string sql = "exec sp_salesorderheader_process @Mode = 0";
                var dt = Util.Select(sql);
                if (dt.Rows.Count == 0)
                {
                    return results;
                }
                sql = dt.Rows[0]["Result"].ToString();

                sql += "insert #SaleOrderDetail values";

                for (int i = 0; i < data.SaleOrderDetail.Count; i++)
                {
                    int countid = i + 1;
                    string itemcode = data.SaleOrderDetail[i].Items;
                    decimal price = data.SaleOrderDetail[i].Price;

                    if (i > 0)
                    {
                        sql += ",";
                    }
                    sql += "(";
                    sql += $"{countid},'{itemcode}', {price}";
                    sql += ")";
                }

                sql += ";exec sp_salesorderheader_process @Mode=1 ," +
                 $"@Name='{data.Name}'," +
                 $"@Surname = '{data.Surname}', " +
                 $"@Email = '{data.Email}'," +
                 $"@ItemNr ={data.ItemNr}," +
                 $"@TotalLine ={data.TotalLine}";

                results = Util.Execute(sql);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return results;
        }
    }
}
