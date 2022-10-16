using Microsoft.EntityFrameworkCore;
using MVCCrudEfApp.Models.Domain;

namespace MVCCrudEfApp.Models.Data
{
    public class MvcDemoDbContext : DbContext
    {
        public MvcDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
