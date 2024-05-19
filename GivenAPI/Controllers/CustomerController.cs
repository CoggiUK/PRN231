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
            var ListoderId = _context.Customers.Include(s => s.Orders).ThenInclude(s => s.OrderDetails).FirstOrDefault(s => s.CustomerId == CustomerId);
            int OrderdeleteCount = 0;
            int OrderDetaildelete = 0;

            if (ListoderId == null)
            {
                return NotFound("Error Not found");
            }
            if (ListoderId != null)
            {
                foreach (var orderId in ListoderId.Orders)
                {
                    
                    OrderdeleteCount++;
                    OrderDetaildelete += orderId.OrderDetails.Count;
                    _context.OrderDetails.RemoveRange(orderId.OrderDetails);
                }
                _context.Orders.RemoveRange(ListoderId.Orders);
                _context.Customers.Remove(ListoderId);

                return Ok(OrderDetaildelete);
            }
            return Ok(CustomerId);
        }
    }
}
