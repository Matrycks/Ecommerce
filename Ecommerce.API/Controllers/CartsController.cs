using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Carts;
using Ecommerce.Application.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ILogger<CartsController> _logger;
        private readonly CreateCart _createCart;
        private readonly GetCart _getCart;

        public CartsController(ILogger<CartsController> logger, CreateCart createCart, GetCart getCart)
        {
            _logger = logger;
            _createCart = createCart;
            _getCart = getCart;
        }

        [HttpPost]
        public IActionResult Create(CreateCartRequest createCartRequest)
        {
            string correlationId = HttpContext.TraceIdentifier;
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            }))
            {
                try
                {
                    _logger.LogInformation("Create cart called");

                    if (createCartRequest.CustomerId <= 0) return BadRequest("Invalid customerId");

                    var cart = _createCart.Execute(createCartRequest.Adapt<CreateCartCommand>());

                    _logger.LogInformation("Create cart succeeded");

                    return Ok(cart.Adapt<CartDto>());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);

                    return BadRequest("Error creating cart");
                }
            }
        }

        [HttpGet("{cartId}")]
        public IActionResult GetCart(int cartId)
        {
            string correlationId = HttpContext.TraceIdentifier;
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            }))
            {
                try
                {
                    _logger.LogInformation("GetCart called {cartId}", cartId);

                    var cart = _getCart.Execute(cartId);

                    _logger.LogInformation("GetCart succeeded");

                    return Ok(cart.Adapt<CartDto>());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);

                    return BadRequest("Error retrieving cart");
                }
            }
        }
    }
}