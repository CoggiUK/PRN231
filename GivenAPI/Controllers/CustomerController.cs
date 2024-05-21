using GivenAPI.DTO;
using GivenAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GivenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly PRN_Sum22_B1Context _context;

        public CustomerController(PRN_Sum22_B1Context context)
        {
            _context = context;
        }
        [HttpPost("delete/{CustomerId}")]
        public ActionResult deleteCustomer(string CustomerId)
        {
            var customer = _context.Customers
                .Include(s => s.Orders)
                .ThenInclude(s => s.OrderDetails)
                .FirstOrDefault(s => s.CustomerId == CustomerId);
            int customerDeleteCount = 0;
            int orderDeleteCount = 0;
            int orderDetailDeleteCount = 0;

            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            else
            {
                customerDeleteCount++;
                foreach (var order in customer.Orders)
                {
                    orderDeleteCount++;
                    orderDetailDeleteCount += order.OrderDetails.Count;
                    
                    _context.OrderDetails.RemoveRange(order.OrderDetails);
                }

                _context.Orders.RemoveRange(customer.Orders);
                _context.Customers.Remove(customer);
                _context.SaveChanges();

                var result = new
                {
                    CustomerDeleteCount = customerDeleteCount,
                    OrderDeleteCount = orderDeleteCount,
                    OrderDetailDeleteCount = orderDetailDeleteCount
                };

                return Ok(result);
            }

            
        }
    }
}
