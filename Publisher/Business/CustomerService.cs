using DotNetCore.CAP;
using Publisher.Contexts;
using Publisher.Dtos;
using Publisher.Entities;

namespace Publisher.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly ServiceDbContext _dbContext;
        private readonly ICapPublisher _capPublisher;

        public CustomerService(ServiceDbContext dbContext, ICapPublisher capPublisher)
        {
            _dbContext = dbContext;
            _capPublisher = capPublisher;
        }

        public async Task<bool> Add(AddCustomerDto addCustomerDto)
        {
            var customer = new Customer
            {
                Name = addCustomerDto.Name,
                Surname = addCustomerDto.Surname,
            };

            await _dbContext.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            await _capPublisher.PublishAsync("Customer_Added", customer);
            return true;
        }
    }
}
