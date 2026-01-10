using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Products;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    /// <summary>
    /// Manages shopping products.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly GetProducts _getProducts;

        /// <summary>
        /// Manages shopping products.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="getProducts"></param>
        public ProductsController(ILogger<ProductsController> logger, GetProducts getProducts)
        {
            _logger = logger;
            _getProducts = getProducts;
        }

        /// <summary>
        /// Retrieves list of products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetProducts()
        {
            var correlationId = HttpContext.TraceIdentifier;
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            }))
            {
                try
                {
                    _logger.LogInformation("GetProducts request received");

                    var products = _getProducts.Execute();

                    _logger.LogInformation("GetProducts succeeded");

                    return Ok(products.Adapt<List<ProductDto>>());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Retrieving products failed");

                    return BadRequest("Retrieving products failed");
                }
            }
        }

    }
}