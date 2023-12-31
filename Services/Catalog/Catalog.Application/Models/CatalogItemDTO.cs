﻿namespace Catalog.Application.Models
{
    public record CatalogItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public int CatalogBrandId { get; set; }
        public string? CatalogBrand { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
    }
}
