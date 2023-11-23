using Catalog.Application.Models;
using Catalog.Domain.Models.CatalogItems;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetCatalogItemByIdQuery : IRequest<QueryResult<CatalogItemDTO>>
    {
        public int Id { get; set; }
    }
}
