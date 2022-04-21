using Service.Dbinitializer;

namespace Service.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        public void Initialize()
        {
            //migrations if they are not applied

            //create roles if they are not create

            //if roles are not  created,then we will create admin user as well

            throw new NotImplementedException();
        }
    }
}
