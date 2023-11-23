using Catalog.Application.Models;
using Catalog.Domain.Models.CatalogBrands;
using Catalog.Domain.Models.CatalogItems;

namespace Catalog.Application
{
    public static class Extensions
    {
        public static IEnumerable<CatalogBrandDTO> ToCatalogBrandsDTO(this IEnumerable<CatalogBrand> brands)
        {
            foreach (var brand in brands)
            {
                yield return brand.ToCatalogBrandDTO();
            }
        }

        public static CatalogBrandDTO ToCatalogBrandDTO(this CatalogBrand brand)
        {
            return new CatalogBrandDTO
            {
                Id = brand.Id,
                Name = brand.Name,
            };
        }

        public static IEnumerable<CatalogItemDTO> ToCatalogItemsDTO(this IEnumerable<CatalogItem> items)
        {
            foreach (var item in items)
            {
                yield return item.ToCatalogItemDTO();
            }
        }

        public static CatalogItemDTO ToCatalogItemDTO(this CatalogItem item)
        {
            return new CatalogItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                PictureFileName = item.PictureFileName,
                PictureUri = item.PictureUri,
                CatalogBrandId = item.CatalogBrandId,
                CatalogBrand = item.CatalogBrand?.Name,
                AvailableQuantity = item.AvailableQuantity,
                Price = item.Price
            };
        }
    }
}
