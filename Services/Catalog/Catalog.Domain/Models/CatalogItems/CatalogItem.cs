using Catalog.Domain.Exceptions;
using Catalog.Domain.Models.CatalogBrands;

namespace Catalog.Domain.Models.CatalogItems
{
    public class CatalogItem : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public int CatalogBrandId { get; set; }
        public CatalogBrand CatalogBrand { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
        public int AddQuantity(int quantity)
        {
            this.AvailableQuantity += quantity;

            return this.AvailableQuantity;
        }
        public int RemoveQuantity(int quantity)
        {
            if (AvailableQuantity == 0)
            {
                throw new CatalogDomainException($"Empty stock, product item {Name} is sold out");
            }

            if (AvailableQuantity <= 0)
            {
                throw new CatalogDomainException($"Item units desired should be greater than zero");
            }

            int removed = Math.Min(quantity, this.AvailableQuantity);

            this.AvailableQuantity -= removed;

            return removed;
        }
    }
}
