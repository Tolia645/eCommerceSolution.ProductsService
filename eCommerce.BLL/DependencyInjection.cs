using eCommerce.BLL.Mappers;
using eCommerce.BLL.Mappers;
using eCommerce.BLL.Services.Implementations;
using eCommerce.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinnesLayerServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapProductResponse).Assembly);
        
        services.AddScoped<IProductService, eCommerce.BLL.Services.Implementations.ProductService>();
        
        return services;
    }
}