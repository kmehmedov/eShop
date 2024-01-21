using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Models.ShoppingCart;

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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBasketByIdAsync(string id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        #region Private members
        private readonly IShoppingCartRepository _repository;
        #endregion
    }
}
