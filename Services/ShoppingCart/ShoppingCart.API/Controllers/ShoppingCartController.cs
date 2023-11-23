using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Models.ShoppingCart;
using System.Security.Claims;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        public ShoppingCartController(IShoppingCartRepository repository)
        {
            this._repository=repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Models.ShoppingCart.ShoppingCart>> GetBasketByIdAsync(string id)
        {
            var basket = await _repository.GetAsync(id);

            return Ok(basket ?? new Domain.Models.ShoppingCart.ShoppingCart(id));
        }

        [HttpPost]
        public async Task<ActionResult<Domain.Models.ShoppingCart.ShoppingCart>> UpdateBasketAsync([FromBody] Domain.Models.ShoppingCart.ShoppingCart value)
        {
            return Ok(await _repository.UpdateAsync(value));
        }   

        //[Route("checkout")]
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status202Accepted)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult> CheckoutAsync([FromBody] BasketCheckout basketCheckout, [FromHeader(Name = "x-requestid")] string requestId)
        //{
        //    var userId = _identityService.GetUserIdentity();

        //    basketCheckout.RequestId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ?
        //        guid : basketCheckout.RequestId;

        //    var basket = await _repository.GetBasketAsync(userId);

        //    if (basket == null)
        //    {
        //        return BadRequest();
        //    }

        //    var userName = User.FindFirst(x => x.Type == ClaimTypes.Name).Value;

        //    var eventMessage = new UserCheckoutAcceptedIntegrationEvent(userId, userName, basketCheckout.City, basketCheckout.Street,
        //        basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
        //        basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer, basketCheckout.RequestId, basket);

        //    // Once basket is checkout, sends an integration event to
        //    // ordering.api to convert basket to order and proceeds with
        //    // order creation process
        //    try
        //    {
        //        _eventBus.Publish(eventMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId}", eventMessage.Id);

        //        throw;
        //    }

        //    return Accepted();
        //}

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task DeleteBasketByIdAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        #region Private members
        private readonly IShoppingCartRepository _repository;
        #endregion
    }
}
