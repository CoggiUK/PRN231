using GivenAPI.DTO;
using GivenAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GivenAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PRN_Sum22_B1Context _context;

        public OrderController(PRN_Sum22_B1Context context)
        {
            _context = context;
        }
        [HttpGet("GetallOrder")]
        public List<OrderDTO> GetAllOrder()
        {
            var listOrder = _context.Orders.Include(o => o.Employee).Select(
                s => new OrderDTO
                {
                    OrderId = s.OrderId,
                    CustomerId = s.CustomerId,
                    CustomerName = s.Customer.CompanyName,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.Employee.FirstName+" " + s.Employee.LastName,
                    EmployeeDepartmentId = s.Employee.DepartmentId,
                    EmployeeDepartmentName = s.Employee.Department.DepartmentName,
                    OrderDate = s.OrderDate,
                    RequiredDate = s.RequiredDate,
                    ShippedDate = s.ShippedDate,
                    Freight = s.Freight,
                    ShipName = s.ShipName,
                    ShipAddress = s.ShipAddress,
                    ShipCity = s.ShipCity,
                    ShipRegion  = s.ShipRegion,
                    ShipPostalCode = s.ShipPostalCode,
                    ShipCountry = s.ShipCountry
                }).ToList();
            return listOrder;
        }

        [HttpGet("getorderbydate/{From}/{To}")]
        public ActionResult GetOrderByDate(DateTime From, DateTime To)
        {
            var listOrder = _context.Orders.Include(o => o.Employee).Where(s => s.OrderDate >= From && s.OrderDate <= To).Select(
                s => new OrderDTO
                {
                    OrderId = s.OrderId,
                    CustomerId = s.CustomerId,
                    CustomerName = s.Customer.CompanyName,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.Employee.FirstName + " " + s.Employee.LastName,
                    EmployeeDepartmentId = s.Employee.DepartmentId,
                    EmployeeDepartmentName = s.Employee.Department.DepartmentName,
                    OrderDate = s.OrderDate,
                    RequiredDate = s.RequiredDate,
                    ShippedDate = s.ShippedDate,
                    Freight = s.Freight,
                    ShipName = s.ShipName,
                    ShipAddress = s.ShipAddress,
                    ShipCity = s.ShipCity,
                    ShipRegion = s.ShipRegion,
                    ShipPostalCode = s.ShipPostalCode,
                    ShipCountry = s.ShipCountry
                }).ToList();
            return Ok(listOrder);
        }


    }
}
