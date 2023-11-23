using Catalog.Application.Models;
using Catalog.Domain.Models.CatalogItems;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetCatalogItemsQueryHandler : IRequestHandler<GetCatalogItemsQuery, QueryResult<IEnumerable<CatalogItemDTO>>>
    {
        public GetCatalogItemsQueryHandler(ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }
        public async Task<QueryResult<IEnumerable<CatalogItemDTO>>> Handle(GetCatalogItemsQuery request, CancellationToken cancellationToken)
        {
            var catalogItems = await _catalogItemRepository.GetAllAsync();

            return new QueryResult<IEnumerable<CatalogItemDTO>>(result: catalogItems.ToCatalogItemsDTO(), type: QueryResultTypeEnum.Success);
        }

        #region Private members
        private readonly ICatalogItemRepository _catalogItemRepository;
        #endregion
    }
}
