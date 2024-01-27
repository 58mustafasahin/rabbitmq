using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Publisher.Business;
using Publisher.Dtos;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCustomerDto addCustomerDto)
        {
            var customer = await _customerService.Add(addCustomerDto);
            if (!customer)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
