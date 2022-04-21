using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public class Utils
    {
        private static SqlConnection con;
        //private static SqlCommand cmd;
        private DataTable dt;
        private SqlDataAdapter adp;

        public Utils()
        {
            con = new SqlConnection("data source = NKOSANA-LT; database=Laundry; integrated security =SSPI;");
            con.Open();
        }
        public DataTable Select(string sql)
        {
            adp = new SqlDataAdapter(sql, con);
            dt = new DataTable();
            adp.Fill(dt);

            return dt;
        }
        public bool Execute(string sql)
        {
            bool flag = false;
            adp = new SqlDataAdapter(sql, con);
            dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows[0][0].ToString() == "ok")
                flag = true;

            return flag;
        }
    }
}
