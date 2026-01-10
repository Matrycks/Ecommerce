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
    /// <summary>
    /// Manages cart and cart items.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ILogger<CartsController> _logger;
        private readonly CreateCart _createCart;
        private readonly GetCart _getCart;
        private readonly AddCartItem _addCartItem;
        private readonly RemoveCartItem _removeCartItem;

        /// <summary>
        /// Manages cart and cart items.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="createCart"></param>
        /// <param name="getCart"></param>
        /// <param name="addCartItem"></param>
        /// <param name="removeCartItem"></param>
        public CartsController(ILogger<CartsController> logger, CreateCart createCart,
            GetCart getCart, AddCartItem addCartItem, RemoveCartItem removeCartItem)
        {
            _logger = logger;
            _createCart = createCart;
            _getCart = getCart;
            _addCartItem = addCartItem;
            _removeCartItem = removeCartItem;
        }

        /// <summary>
        /// Creates a new cart.
        /// </summary>
        /// <param name="createCartRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateCart(CreateCartRequest createCartRequest)
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


        /// <summary>
        /// Retrieves an existing cart.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds an item to an existing cart.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{cartId}/Item")]
        public IActionResult AddCartItem(int cartId, AddCartItemRequest request)
        {
            string correlationId = HttpContext.TraceIdentifier;
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            }))
            {
                try
                {
                    _logger.LogInformation("AddCartItem called {cartId}", cartId);

                    var cart = _addCartItem.Execute(request.Adapt<AddCartItemCommand>());

                    _logger.LogInformation("AddCartItem succeeded");

                    return Ok(cart.Adapt<CartDto>());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);

                    return BadRequest("Error adding cart item");
                }
            }
        }

        /// <summary>
        /// Removes an item from existing cart.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        [HttpDelete("{cartId}/Item/{cartItemId}")]
        public IActionResult RemoveCartItem(int cartId, int cartItemId)
        {
            string correlationId = HttpContext.TraceIdentifier;
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            }))
            {
                try
                {
                    _logger.LogInformation("RemoveCartItem called: Cart: {cartId}, Item: {cartItemId}",
                        cartId, cartItemId);

                    var cart = _removeCartItem.Execute(cartId, cartItemId);

                    _logger.LogInformation("RemoveCartItem succeeded");

                    return Ok(cart.Adapt<CartDto>());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);

                    return BadRequest("Error removing item");
                }
            }
        }
    }
}