namespace ScarletScreen.MongoConnection.Services
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string MoviesDatabaseName { get; set; }
        public string TVDatabaseName { get; set; }
    }

}