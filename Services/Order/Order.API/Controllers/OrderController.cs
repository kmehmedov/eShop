using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Commands;
using Order.Application.Models;
using Order.Application.Queries;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("catalogitems/{id}")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderByIdAsync([FromRoute] int id)
        {
            var query = new GetOrderByIdQuery()
            {
                Id = id,
            };
            var result = await _mediator.Send(query);

            return GetResponseFromQueryResult(result);
        }

        [HttpPost]
        [Route("cancel")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelOrderAsync([FromBody] CancelOrderCommand command)
        {
            var commandResult = await _mediator.Send(command);

            return GetResponseFromCommandResult(commandResult);
        }

        [HttpPost]
        [Route("draft")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrderFromShoppingCartAsync([FromBody] CreateOrderFromShoppingCartCommand command)
        {
            var commandResult = await _mediator.Send(command);

            return GetResponseFromCommandResult(commandResult);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderCommand command)
        {
            var commandResult = await _mediator.Send(command);

            return GetResponseFromCommandResult(commandResult);
        }

        [HttpGet]
        [Route("buyer/{id}")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllOrdersAsync([FromRoute] string id)
        {
            var query = new GetOrdersQuery { BuyerId = id };
            var queryResult = await _mediator.Send(query);

            return GetResponseFromQueryResult(queryResult);
        }

        #region Private members
        private IActionResult GetResponseFromCommandResult<T>(CommandResult<T> commandResult)
        {
            switch (commandResult.Type)
            {
                case CommandResultTypeEnum.NotFound:
                    return new NotFoundResult();
                case CommandResultTypeEnum.Success:
                    return new OkObjectResult(commandResult.Result);
                default:
                    return new BadRequestResult();
            }
        }

        private IActionResult GetResponseFromQueryResult<T>(QueryResult<T> queryResult)
        {
            switch (queryResult.Type)
            {
                case QueryResultTypeEnum.NotFound:
                    return new NotFoundResult();
                case QueryResultTypeEnum.Success:
                    return new OkObjectResult(queryResult.Result);
                default:
                    return new BadRequestResult();
            }
        }

        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        #endregion
    }
}
