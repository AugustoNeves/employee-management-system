namespace Application.FunctionalTests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new SqlTestcontainersTestDatabase();

        await database.InitialiseAsync();

        return database;
    }
}
