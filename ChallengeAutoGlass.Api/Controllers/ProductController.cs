using ChallengeAutoGlass.Domain.Entities;
using ChallengeAutoGlass.Domain.Model;
using ChallengeAutoGlass.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeAutoGlass.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("{code}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int code, CancellationToken ctx)
        {
            var product = await _productService.GetProductByCodeAsync(code, ctx);

            if(product is null)
            {
                return NotFound("Could not found any products");
            }

            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetList([FromQuery] int ps = 4, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var products = await _productService.GetProductsAsync(ps, page, q);

            if (products is null)
            {
                return NotFound("Could not found any products");
            }

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ProductModel product, CancellationToken ctx)
        {
            var result = await _productService.InsertNewProduct(product, ctx);

            if (result)
            {
                return Created("product:", product);
            }

            return BadRequest();
        }

        [HttpPatch]
        [Route("{code}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Patch(int code, CancellationToken ctx)
        {
            var result = await _productService.DeleteProduct(code, ctx);
            if (!result)
            {
                return BadRequest();
            }
            return Ok("Product Inactivade");

        }

        [HttpPut]
        [Route("{code}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int code, [FromBody] ProductModel product, CancellationToken ctx)
        {
            var result = await _productService.UpdateProduct(code, product, ctx);

            if (!result)
            {
                return BadRequest();
            }

            return Ok("Product Updatede");
        }

    }
}
