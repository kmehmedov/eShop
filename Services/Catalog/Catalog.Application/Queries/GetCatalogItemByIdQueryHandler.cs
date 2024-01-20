using Catalog.Application.Models;
using Catalog.Domain.Models.CatalogItems;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetCatalogItemByIdQueryHandler : IRequestHandler<GetCatalogItemByIdQuery, QueryResult<CatalogItemDTO>>
    {
        public GetCatalogItemByIdQueryHandler(ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }
        public async Task<QueryResult<CatalogItemDTO>> Handle(GetCatalogItemByIdQuery request, CancellationToken cancellationToken)
        {
            CatalogItem catalogItem = new CatalogItem();

            try
            {
                catalogItem = await _catalogItemRepository.GetAsync(request.Id);
            }
            catch (InvalidOperationException)
            {
                return new QueryResult<CatalogItemDTO>(result: new CatalogItemDTO(), type: QueryResultTypeEnum.NotFound);
            }

            return new QueryResult<CatalogItemDTO>(result: catalogItem.ToCatalogItemDTO(), type: QueryResultTypeEnum.Success);
        }

        #region Private members
        private readonly ICatalogItemRepository _catalogItemRepository;
        #endregion
    }
}
