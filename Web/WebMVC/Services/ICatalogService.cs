using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public interface ICatalogService
    {
        Task<CatalogItem> GetCatalogItemById(int id);
        Task<IEnumerable<CatalogItem>> GetCatalogItems(int? brand);
        Task<IEnumerable<CatalogBrand>> GetBrands();
    }
}
