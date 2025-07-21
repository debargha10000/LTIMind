using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApi.DTOs.Output;
using SalesApi.Models;
using SalesApi.Services;

namespace SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _service;
        public CustomersController(CustomerService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAllAsync();
            // return customers;
            var results = customers.Select(
                c => new CustomerOutputDto(
                    c.CustomerId,
                    c.CustomerName,
                    c.City,
                    c.Orders.Select(o => o.OrderId).ToList()
                )
            );

            return Ok(results);
        }


    }
}
