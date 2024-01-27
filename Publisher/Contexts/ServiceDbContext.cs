using Microsoft.EntityFrameworkCore;
using Publisher.Entities;

namespace Publisher.Contexts
{
    public class ServiceDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DbConnection")));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasKey(p => p.Id);
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
