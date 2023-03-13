using Microsoft.EntityFrameworkCore;
using RealEstateListingApp.Models.Domain;

namespace RealEstateListingApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
    }
}
