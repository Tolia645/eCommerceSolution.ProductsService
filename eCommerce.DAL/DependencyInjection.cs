using eCommerce.DAL.Persistence;
using eCommerce.DAL.Repository.Implementations;
using eCommerce.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eCommerce.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccesLayer(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionStringTemplate = configuration.GetConnectionString("DefaultConnection")!;
        string connectionString = connectionStringTemplate
            .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
            .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"));
        
        Console.WriteLine($"Connection string: {connectionString}");
        
        //add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString!)
            );
        });

        services.AddScoped<IProductsRepository, ProductsRepository>();
        
        return services;
    }
}