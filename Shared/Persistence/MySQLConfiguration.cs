namespace WebApi.Shared.Persistence
{
    public class MySQLConfiguration
    {


        public string  connectionString { get; set; }

        public MySQLConfiguration(string connectionString)
        {

            this.connectionString = connectionString;
        }
    }
}
