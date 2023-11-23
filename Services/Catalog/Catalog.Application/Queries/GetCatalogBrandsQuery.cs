using Catalog.Application.Models;
using Catalog.Domain.Models.CatalogBrands;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetCatalogBrandsQuery : IRequest<QueryResult<IEnumerable<CatalogBrandDTO>>>
    {
    }
}
