using Model;
using System.Data;

namespace Service
{
    public partial class DataHandler
    {
        private Company GetCompanyFromDataRow(DataRow row)
        {
            return new Company
            {
                Id = (int)row["Id"],
                Name = row["Name"].ToString(),
                PhoneNumber = row["PhoneNumber"].ToString(),
                PostalCode = row["PostalCode"].ToString(),
                StreetAddress = row["StreetAddress"].ToString(),
                City = row["City"].ToString(),
            };
        }
        public List<Company> CompanyListGet()
        {
            var sql = "exec sp_company_data @Mode=0  ";

            var dt = Util.Select(sql);
            var list = new List<Company>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(GetCompanyFromDataRow(row));
            }
            return list;
        }
        public Company CompanyGetSingle(int Id)
        {
            var sql = $"exec sp_company_data @Mode=1, @id={Id}";

            var dt = Util.Select(sql);
            if (dt.Rows.Count == 0)
            {
                return new Company();
            }
            var r = GetCompanyFromDataRow(dt.Rows[0]);
            return r;
        }
        public bool AddCompany(Company data)
        {
            string sql = "exec sp_company_process @Mode=0 ," +
             $"@Name='{data.Name}'," +
             $"@PhoneNumber = '{data.PhoneNumber}', " +
             $"@PostalCode = '{data.PostalCode}'," +
             $"@StreetAddress ='{data.StreetAddress}'," +
             $"@City ='{data.City}'";
            return Util.Execute(sql);
        }
        public bool UpdateCustomer(Company data)
        {
            string sql = $"exec sp_company_process @Mode=1 ," +
            $"@Id='{data.Id}'," +
            $"@Name='{data.Name}'," +
            $"@PhoneNumber = '{data.PhoneNumber}', " +
            $"@PostalCode = '{data.PostalCode}'," +
            $"@StreetAddress ='{data.StreetAddress}'," +
            $"@City ='{data.City}'";
            return Util.Execute(sql);
        }
    }
}
