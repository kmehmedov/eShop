using Catalog.Application.Models;
using Catalog.Application.Queries;
using Catalog.Domain.Models.CatalogBrands;
using Catalog.Domain.Models.CatalogItems;
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
        [ProducesResponseType(typeof(CatalogItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("catalogitems/{id}")]
        public async Task<ActionResult<CatalogItem>> GetCatalogItemByIdAsync([FromRoute] int id)
        {
            var query = new GetCatalogItemByIdQuery()
            {
                Id = id,
            };
            var result = await _mediator.Send(query);

            if (result.Type == QueryResultTypeEnum.NotFound)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result.Result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CatalogItem>), StatusCodes.Status200OK)]
        [Route("catalogitems")]
        public async Task<ActionResult<IEnumerable<CatalogItem>>> GetCatalogItemsAsync()
        {
            var query = new GetCatalogItemsQuery();
            var result = await _mediator.Send(query);

            return Ok(result.Result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CatalogBrand>), StatusCodes.Status200OK)]
        [Route("catalogbrands")]
        public async Task<ActionResult<IEnumerable<CatalogBrand>>> GetCatalogBrandsAsync()
        {
            var query = new GetCatalogBrandsQuery();
            var brands = await _mediator.Send(query);

            return Ok(brands);
        }

        #region Private members
        private readonly IMediator _mediator;
        private readonly ILogger<CatalogController> _logger;
        #endregion
    }
}
