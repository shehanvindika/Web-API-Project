using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreAPICore.Common;
using StoreAPICore.Controller;
using StoreAPICore.Controllers;
using StoreAPICore.Domain;
using System.Data;



namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAPIController : ControllerBase
    {
        [Route("v1/CreateCustomer")]
        [HttpPost]
        
        public IActionResult CreateCustomer([FromBody] InputCustomer customer)
        {
            CustomerController customerController = ControllerFactory.CreateCustomerController();
            var msg = customerController.CreateCustomer(customer);
            return Ok(new { message = msg });
        }

        [Route("v1/GetAllCustomer")]
        [HttpGet]

        public IActionResult GetAllCustomer()
        {
            CustomerController customerController = ControllerFactory.CreateCustomerController();
            DataTable CustomerList = customerController.GetAllCustomers();

            // Convert DataTable to a List of Dictionary<string, object>
            var ordersList = new List<Dictionary<string, object>>();

            foreach (DataRow row in CustomerList.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in CustomerList.Columns)
                {
                    rowDict[column.ColumnName] = row[column];
                }
                ordersList.Add(rowDict);
            }
            return Ok(ordersList);
        }

        [Route("v1/UpdateCustomer")]
        [HttpPost]

        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            CustomerController customerController = ControllerFactory.CreateCustomerController();
            var result = customerController.UpdateCustomer(customer);
            return Ok(new { message = result });
        }

        [Route("v1/DeleteCustomer")]
        [HttpDelete]

        public IActionResult DeleteCustomer(Guid customerId)
        {
            CustomerController customerController = ControllerFactory.CreateCustomerController();
            var result = customerController.DeleteCustomer(customerId);
            return Ok(new { message = result });
        }

        
    }
}
