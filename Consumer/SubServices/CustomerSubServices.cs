using Consumer.Contexts;
using Consumer.Entities;
using DotNetCore.CAP;

namespace Consumer.SubServices
{
    public class CustomerSubServices : ICapSubscribe
    {
        private readonly ConsumerDbContext _dbContext;

        public CustomerSubServices(ConsumerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [CapSubscribe("Customer_Added")]
        public async Task Added(Customer entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
