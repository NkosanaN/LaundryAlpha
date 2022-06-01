using Model;
using S_and_S.Hubs;
using TableDependency.SqlClient;

namespace S_and_S.SubscribeTableDependencies
{
    //https://www.youtube.com/watch?v=1ulnU5ppXag&list=WL&index=269
    public class SubscribePendingLaundryDependency
    {
        private IConfiguration _configuration;
        SqlTableDependency<OrderHeader> tbDependency;
        DashboardHub dashboardHub;

        public SubscribePendingLaundryDependency(DashboardHub dashboardHub 
                                                 )
        {
            this.dashboardHub = dashboardHub;
          //  _configuration = configuration;
        }
        public void SubscribeTableDependency()
        {
            string connectionString = "";//_configuration.GetConnectionString("DefaultConnection");
            tbDependency = new SqlTableDependency<OrderHeader>(connectionString);
            tbDependency.OnChanged += TableDependency_OnChanged;
            tbDependency.OnError += TableDependency_OnError;
            tbDependency.Start();
        }
        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<OrderHeader> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendPendingLaundry();
            }
        }
        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(OrderHeader)} SqlTableDependency error {e.Error.Message}");
        }
    }
}
