namespace ScarletScreen.MongoConnection.Services
{
public interface IDatabaseSettings
{
    string ConnectionString { get; set; }
    string MoviesDatabaseName { get; set; }
    string TVDatabaseName { get; set; }
}

}