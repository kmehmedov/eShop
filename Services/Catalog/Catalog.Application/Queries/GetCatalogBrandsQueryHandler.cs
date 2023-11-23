using Catalog.Application.Models;
using Catalog.Domain.Models.CatalogBrands;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetCatalogBrandsQueryHandler : IRequestHandler<GetCatalogBrandsQuery, QueryResult<IEnumerable<CatalogBrandDTO>>>
    {
        public GetCatalogBrandsQueryHandler(ICatalogBrandRepository catalogBrandRepository)
        {
            _catalogBrandRepository = catalogBrandRepository;
        }
        public async Task<QueryResult<IEnumerable<CatalogBrandDTO>>> Handle(GetCatalogBrandsQuery request, CancellationToken cancellationToken)
        {
            var catalogBrands = await _catalogBrandRepository.GetAllAsync();

            return new QueryResult<IEnumerable<CatalogBrandDTO>>(result: catalogBrands.ToCatalogBrandsDTO(), type: QueryResultTypeEnum.Success);
        }

        #region Private members
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        #endregion
    }
}
