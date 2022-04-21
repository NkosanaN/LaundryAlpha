using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Service
{
    public partial class DataHandler
    {
        private Product GetProductFromDataRow(DataRow row)
        {
            return new Product
            {
                ProductName = row["ProductName"].ToString(),
                ProductCategoryID = (int)row["ProductCategoryID"],
                ProductID = (int)row["ProductCategoryID"],
                ListPrice = (decimal)row["ListPrice"],
            };
        }
        public List<Product> ProductListGet()
        {
            var sql = "exec sp_product_data @Mode=0";

            var dt = Util.Select(sql);
            var list = new List<Product>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(GetProductFromDataRow(row));
            }
            return list;
        }
        public List<Product> ProductListGet(int Id)
        {
            var sql = $"exec sp_product_data @Mode=2, @id={Id}";

            var dt = Util.Select(sql);
            var list = new List<Product>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(GetProductFromDataRow(row));
            }
            return list;
        }
        public Product ProductGetSingle(int Id)
        {
            var sql = $"exec sp_product_data @Mode=1, @id={Id}";

            var dt = Util.Select(sql);
            if (dt.Rows.Count == 0)
            {
                return new Product();
            }
            var r = GetProductFromDataRow(dt.Rows[0]);
            return r;
        }
    }
}
