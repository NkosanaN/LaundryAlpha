using Model;
using System.Data;

namespace Service
{
    public partial class DataHandler
    {
        private ProductCategory GetProductCategoryFromDataRow(DataRow row)
        {
            return new ProductCategory
            {
                ProductCategoryID = (int)row["ProductCategoryID"],
            
                Name = row["Name"].ToString(),
            };
        }
        public List<ProductCategory> ProductCategoryListGet()
        {
            var sql = "select * from ProductCategory";

            var dt = Util.Select(sql);
            var list = new List<ProductCategory>();

            // if(dt.Rows.Count == 0)

            foreach (DataRow row in dt.Rows)
            {
                list.Add(GetProductCategoryFromDataRow(row));
            }
            return list;
        }
        public ProductCategory ProductCategoryGetSingle(int Id)
        {
            var sql = $"select * from ProductCategory where id ={Id} ";

            var dt = Util.Select(sql);
            if (dt.Rows.Count == 0)
            {
                return new ProductCategory();
            }
            var r = GetProductCategoryFromDataRow(dt.Rows[0]);
            return r;
        }
    }
}
