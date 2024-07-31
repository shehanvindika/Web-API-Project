using Microsoft.AspNetCore.Mvc;
using StoreAPICore.Common;
using StoreAPICore.Controller;
using StoreAPICore.Domain;
using System.Data;
using System.Numerics;
using System.Web.Http.Results;


namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAPIController : ControllerBase
    {
        [Route("api/v1/CreateCustomer")]
        [HttpPost]
        
        public IActionResult CreateCustomer([FromBody] InputCustomer customer)
        {
            CustomerController customerController = ControllerFactory.CreateCustomerController();
            return Ok(customerController.CreateCustomer(customer));
        }

        [Route("api/v1/GetAllCustomer")]
        [HttpGet]

        public List<Customer> GetAllCustomer()
        {
            CustomerController customerController = ControllerFactory.CreateCustomerController();
            return customerController.GetAllCustomers();
        }

        [Route("api/v1/UpdateCustomer")]
        [HttpPost]

        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            CustomerController customerController = ControllerFactory.CreateCustomerController();
            return Ok(customerController.UpdateCustomer(customer));
        }

        [Route("api/v1/DeleteCustomer")]
        [HttpDelete]

        public IActionResult DeleteCustomer(Guid customerId)
        {
            CustomerController customerController = ControllerFactory.CreateCustomerController();
            return Ok(customerController.DeleteCustomer(customerId));
        }

        
    }
}
