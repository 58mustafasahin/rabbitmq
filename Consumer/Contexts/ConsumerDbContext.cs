using Microsoft.EntityFrameworkCore;
using Consumer.Entities;

namespace Consumer.Contexts
{
    public class ConsumerDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ConsumerDbContext(DbContextOptions<ConsumerDbContext> options,IConfiguration configuration)
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
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
