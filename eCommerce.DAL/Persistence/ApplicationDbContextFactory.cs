
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eCommerce.DAL.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=eCommerceProducts;User Id=sa;Password=Pa@@w0rd;MultipleActiveResultSets=true;TrustServerCertificate=True"); // або Configuration

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
