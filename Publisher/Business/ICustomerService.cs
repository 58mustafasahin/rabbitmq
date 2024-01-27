using Publisher.Dtos;

namespace Publisher.Business
{
    public interface ICustomerService
    {
        Task<bool> Add(AddCustomerDto addCustomerDto);
    }
}
