using Model;
using System.Data;

namespace Service
{
    public partial class DataHandler
    {
        private Debtors GetCustomerFromDataRow(DataRow row)
        {
            return new Debtors
            {
                Id = (int)row["Id"],
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Email = row["Email"].ToString(),
                MobilePhoneNumber = row["MobilePhoneNumber"].ToString(),
                CreationDate = (DateTime)row["CreationDate"],
                isCollected = (bool)row["isCollected"],
                StreetAddress1 = row["StreetAddress1"].ToString(),
                StreetAddress2 = row["StreetAddress2"].ToString(),
                Catergory = row["Catergory"].ToString()
            };
        }
        public List<Debtors> CustomerListGet()
        {
            var sql = "exec sp_customer_data @Mode=0";

            var dt = Util.Select(sql);
            var list = new List<Debtors>();

           // if(dt.Rows.Count == 0)

            foreach (DataRow row in dt.Rows)
            {
                list.Add(GetCustomerFromDataRow(row));
            }
            return list;
        }
        public Debtors CustomerGetSingle(int Id)
        {
            var sql = $"exec sp_customer_data @Mode=1, @id={Id}";

            var dt = Util.Select(sql);
            if (dt.Rows.Count == 0)
            {
                return new Debtors();
            }
            var r = GetCustomerFromDataRow(dt.Rows[0]);
            return r;
        }
        public bool AddCustomer(Debtors data)
        {
            string sql = "$exec sp_customer_process @Mode=0 ," +
             $"@FirstName='{data.FirstName}'," +
             $"@LastName = '{data.LastName}', " +
             $"@Email = '{data.Email}'," +
             $"@MobilePhoneNumber ='{data.MobilePhoneNumber}'," +
             $"@CreationDate ='{data.CreationDate}'," +
             $"@isCollected ='{data.isCollected}'," +
             $"@Catergory ='{data.Catergory}'," +
             $"@StreetAddress1 ='{data.StreetAddress1}'," +
             $"@StreetAddress2 ='{data.StreetAddress2}'";
            return Util.Execute(sql);
        }
        public async Task<bool> UpdateCustomer(Debtors data)
        {
            string sql = "";
            //var sql = $"exec sp_reservation_process @Mode=1 ," +
            //    $"@Id='{data.Id}'," +
            //    $"@Name='{data.Name}'," +
            //    $"@Surname = '{data.Surname}', " +
            //    $"@TelNumber = '{data.TelNumber}'," +
            //    $"@ReservationType ='{data.ReservationType}' ," +
            //    $"@StartDate ='{data.StartDate}' ," +
            //    $"@EndDate ='{data.EndDate}'";
            return Util.Execute(sql);
        }
    }
}
