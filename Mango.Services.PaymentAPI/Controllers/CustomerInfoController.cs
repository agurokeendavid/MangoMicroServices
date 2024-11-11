using Mango.Services.PaymentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.PaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerInfoController : ControllerBase
    {
        private readonly ILogger<CustomerInfoController> _logger;

        public CustomerInfoController(ILogger<CustomerInfoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Get()
        {
            return Ok(new CustomerInfo() {});
        }
    }
}
