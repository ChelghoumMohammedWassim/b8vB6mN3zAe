using b8vB6mN3zAe.Models;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

        public DbSet<User> Users { get; set; }
    }
}