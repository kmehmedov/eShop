using Catalog.Application.Models;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        public CatalogController(IMediator mediator, ILogger<CatalogController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CatalogItemDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("catalogitems/{id}")]
        public async Task<ActionResult<CatalogItemDTO>> GetCatalogItemByIdAsync([FromRoute] int id)
        {
            var query = new GetCatalogItemByIdQuery()
            {
                Id = id,
            };
            var result = await _mediator.Send(query);

            if (result.Type == QueryResultTypeEnum.NotFound)
            {
                _logger.LogWarning("The CatalogItem with Id {id} Could not be found.", id);
                return new NotFoundResult();
            }

            return new OkObjectResult(result.Result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CatalogItemDTO>), StatusCodes.Status200OK)]
        [Route("catalogitems")]
        public async Task<ActionResult<IEnumerable<CatalogItemDTO>>> GetCatalogItemsAsync()
        {
            var query = new GetCatalogItemsQuery();
            var result = await _mediator.Send(query);

            return Ok(result.Result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CatalogBrandDTO>), StatusCodes.Status200OK)]
        [Route("catalogbrands")]
        public async Task<ActionResult<IEnumerable<CatalogBrandDTO>>> GetCatalogBrandsAsync()
        {
            var query = new GetCatalogBrandsQuery();
            var brands = await _mediator.Send(query);

            return Ok(brands.Result);
        }

        #region Private members
        private readonly IMediator _mediator;
        private readonly ILogger<CatalogController> _logger;
        #endregion
    }
}
