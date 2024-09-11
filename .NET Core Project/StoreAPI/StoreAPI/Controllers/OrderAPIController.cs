using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreAPICore.Common;
using StoreAPICore.Controller;
using StoreAPICore.Domain;
using System.Data;
using System.Web.Http.Results;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        [Route("v1/InsertOrder")]
        [HttpPost]

        public IActionResult InsertOrder([FromBody] InputOrder order)
        {
            OrderController orderController = ControllerFactory.CreateOrderController();
            var result = orderController.InsertOrderDetails(order);
            return Ok(new { message = result });
        }

        [Route("v1/ActiveOrdersByCustomer")]
        [HttpGet]
        public IActionResult ActiveOrdersByCustomer(Guid customerId)
        {
            OrderController orderController = ControllerFactory.CreateOrderController();

            DataTable activeOrders = orderController.GetActiveOrdersByCustomers(customerId);

            // Convert DataTable to a List of Dictionary<string, object>
            var ordersList = new List<Dictionary<string, object>>();

            foreach (DataRow row in activeOrders.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in activeOrders.Columns)
                {
                    rowDict[column.ColumnName] = row[column];
                }
                ordersList.Add(rowDict);
            }

            return Ok(ordersList);
        }


    }
}
