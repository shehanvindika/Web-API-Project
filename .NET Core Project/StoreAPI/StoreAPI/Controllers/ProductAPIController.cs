using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPICore.Common;
using StoreAPICore.Controller;
using StoreAPICore.Domain;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        [Route("api/v1/InsertProduct")]
        [HttpPost]

        public IActionResult InsertProduct([FromBody] InputProduct product)
        {
            ProductController productController = ControllerFactory.CreateProductController();
            return Ok(productController.InsertProduct(product));
        }
    }
}
