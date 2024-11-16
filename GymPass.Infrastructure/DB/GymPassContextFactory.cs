using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GymPass.Infrastructure.DB;

public class GymPassContextFactory : IDesignTimeDbContextFactory<GymPassContext> 
{
    public GymPassContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();
        
        var optionsBuilder = new DbContextOptionsBuilder<GymPassContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Connection"));
        
        return new GymPassContext(optionsBuilder.Options);
    }
}