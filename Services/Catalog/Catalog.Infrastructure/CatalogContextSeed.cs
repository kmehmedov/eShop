using Catalog.Domain.Models.CatalogBrands;
using Catalog.Domain.Models.CatalogItems;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure
{
    public class CatalogContextSeed
    {
        public async Task SeedAsync(CatalogContext context)
        {
            using (context)
            {
                if (!context.CatalogBrands.Any())
                {
                    await context.CatalogBrands.AddRangeAsync(GetCatalogBrands());
                    await context.SaveChangesAsync();
                }

                if (!context.CatalogItems.Any())
                {
                    var firstBrand = await context.CatalogBrands.FirstAsync();
                    await context.CatalogItems.AddRangeAsync(GetCatalogItems(firstBrand));
                    await context.SaveChangesAsync();
                }
            }
        }

        #region Private members
        private IEnumerable<CatalogItem> GetCatalogItems(CatalogBrand brand)
        {
            return new List<CatalogItem>()
            {
                new() { Name = "Galaxy S23", Description ="Galaxy S23", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = brand.Id, AvailableQuantity = 100, Price = 300m },
                new() { Name = "Galaxy Z Fold4", Description ="Galaxy Z Fold4", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = brand.Id, AvailableQuantity = 130, Price = 1450m },
                new() { Name = "Galaxy Z Flip", Description ="Galaxy Z Flip", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = brand.Id, AvailableQuantity = 130, Price = 950m },
                new() { Name = "Galaxy A52", Description ="\"Galaxy A52", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = brand.Id, AvailableQuantity = 130, Price = 450m },
                new() { Name = "Galaxy M14", Description ="Galaxy M14", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = brand.Id, AvailableQuantity = 130, Price = 450m },
                new() { Name = "Galaxy F04", Description ="Galaxy F04", PictureFileName = "file.png", PictureUri = "file.png", CatalogBrandId = brand.Id, AvailableQuantity = 130, Price = 450m },
            };
        }

        private IEnumerable<CatalogBrand> GetCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new() { Name = "Samsung"},
                new() { Name = "iPhone" },
                new() { Name = "Xiaomi" },
                new() { Name = "Nokia" },
                new() { Name = "Other" }
            };
        }
        #endregion
    }
}
