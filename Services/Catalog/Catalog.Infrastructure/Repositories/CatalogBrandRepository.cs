using Catalog.Domain.Models.CatalogBrands;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogBrandRepository : EntityRepositoryBase<CatalogBrand>, ICatalogBrandRepository
    {
        public CatalogBrandRepository(CatalogContext context) : base(context)
        {
        }
    }
}
