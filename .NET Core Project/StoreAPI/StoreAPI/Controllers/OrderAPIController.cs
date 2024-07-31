using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreAPICore.Common;
using StoreAPICore.Controller;
using StoreAPICore.Domain;
using System.Data;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        [Route("api/v1/InsertOrder")]
        [HttpPost]

        public string InsertOrder([FromBody] InputOrder order)
        {
            OrderController orderController = ControllerFactory.CreateOrderController();
            return orderController.InsertOrderDetails(order);
        }

        [Route("api/v1/ActiveOrdersByCustomer")]
        [HttpGet]

        public string ActiveOrdersByCustomer(Guid customerId)
        {
            OrderController orderController = ControllerFactory.CreateOrderController();
            return JsonConvert.SerializeObject(orderController.GetActiveOrdersByCustomers(customerId));
        }


    }
}
