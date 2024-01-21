using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;
using WebMVC.ViewModels.HomeViewModels;

namespace WebMVC.Controllers;

public class HomeController : Controller
{
    public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
    {
        _logger = logger;
        this._catalogService=catalogService;
    }

    public async Task<IActionResult> Index()
    {
        var items = new List<ViewModels.CatalogItem>();
        try
        {
            var remoteItems = await _catalogService.GetCatalogItems(null);
            items.AddRange(remoteItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching catalog items");
            ViewBag.Error = $"Error occured - {ex.GetType().Name} - {ex.Message}";
        }

        var vm = new IndexViewModel()
        {
            CatalogItems = items
        };


        return View(vm);
    }

    #region Private members
    private readonly ILogger<HomeController> _logger;
    private readonly ICatalogService _catalogService;
    #endregion
}
