using Langchips.Services;
using Microsoft.Extensions.Configuration;
namespace Langchips.Tests;

public class ConfigurationServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetConnectionString_ReturnsExpectedConnectionString()
    {
        var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:DefaultConnection", "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"}
            };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var configurationService = new ConfigurationService(configuration);

        var connectionString = configurationService.GetConnectionString("DefaultConnection");

        Assert.AreEqual("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;", connectionString);
    }

    [Test]
    public void GetConnectionString_ThrowsException_WhenConnectionStringIsMissing()
    {
        IConfiguration configuration = new ConfigurationBuilder().Build();
        var configurationService = new ConfigurationService(configuration);

        Assert.Throws<InvalidOperationException>(() => configurationService.GetConnectionString("DefaultConnection"));
    }
}
