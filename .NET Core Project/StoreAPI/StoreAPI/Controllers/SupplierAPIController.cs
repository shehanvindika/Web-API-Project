using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPICore.Common;
using StoreAPICore.Controller;
using StoreAPICore.Controllers;
using StoreAPICore.Domain;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierAPIController : ControllerBase
    {
        [Route("v1/CreateSupplier")]
        [HttpPost]

        public IActionResult CreateSupplier([FromBody] InputSupplier supplier)
        {
            SupplierController supplierController = ControllerFactory.CreateSupplierController();
            var result = supplierController.CreateSupplierAccount(supplier);
            return Ok(new { message = result });
        }
    }
}
