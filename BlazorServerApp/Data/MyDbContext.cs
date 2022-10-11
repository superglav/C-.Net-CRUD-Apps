using Microsoft.EntityFrameworkCore;

namespace BlazorServerApp.Data

{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<TodoItem>? TodoItems { get; set; }
    }
}
