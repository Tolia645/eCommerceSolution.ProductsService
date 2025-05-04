using eCommerce.DAL.Persistence;
using eCommerce.DAL.Repository.Implementations;
using eCommerce.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccesLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IProductsRepository, ProductsRepository>();
        
        return services;
    }
}