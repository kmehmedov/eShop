using Catalog.Application.Models;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetCatalogItemsQuery : IRequest<QueryResult<IEnumerable<CatalogItemDTO>>>
    {
    }
}
