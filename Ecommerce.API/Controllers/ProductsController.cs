using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Products;
using Mapster;
using MediatR;
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
        private readonly IMediator _mediator;

        /// <summary>
        /// Manages shopping products.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves list of products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
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

                    var products = await _mediator.Send(new GetProductsCommand());

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