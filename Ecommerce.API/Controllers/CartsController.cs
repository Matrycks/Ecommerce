using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Carts;
using Ecommerce.Application.Dtos;
using Mapster;
using MediatR;
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
        private readonly IMediator _mediator;

        /// <summary>
        /// Manages cart and cart items.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public CartsController(ILogger<CartsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new cart.
        /// </summary>
        /// <param name="createCartRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCart(CreateCartRequest createCartRequest)
        {
            if (createCartRequest.CustomerId <= 0) return BadRequest("Invalid customerId");

            var cart = await _mediator.Send(createCartRequest.Adapt<CreateCartCommand>());

            return Ok(cart.Adapt<CartDto>());
        }


        /// <summary>
        /// Retrieves an existing cart.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(int cartId)
        {
            var cart = await _mediator.Send(new GetCartCommand(cartId));

            return Ok(cart.Adapt<CartDto>());
        }

        /// <summary>
        /// Adds an item to an existing cart.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{cartId}/Item")]
        public async Task<IActionResult> AddCartItem(int cartId, AddCartItemRequest request)
        {
            var cart = await _mediator.Send(request.Adapt<AddCartItemCommand>());

            return Ok(cart.Adapt<CartDto>());
        }

        /// <summary>
        /// Removes an item from existing cart.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        [HttpDelete("{cartId}/Item/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(int cartId, int cartItemId)
        {
            var cart = await _mediator.Send(new RemoveCartItemCommand(cartId, cartItemId));

            return Ok(cart.Adapt<CartDto>());
        }

        /// <summary>
        /// Handles cart checkout.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="checkoutCommand"></param>
        /// <returns></returns>
        [HttpPost("{cartId}/checkout")]
        public async Task<IActionResult> Checkout(int cartId, [FromBody] CheckoutCommand checkoutCommand)
        {
            var order = await _mediator.Send(checkoutCommand);

            return Ok(order.Adapt<OrderDto>());
        }
    }
}