using Catalog.Domain.Models.CatalogItems;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogItemRepository : EntityRepositoryBase<CatalogItem>, ICatalogItemRepository
    {
        public CatalogItemRepository(CatalogContext context) : base(context)
        {
        }
    }
}
