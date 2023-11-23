using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        public ShoppingCartController(IShoppingCartService shoppingCartService, ICatalogService catalogService, ILogger<ShoppingCartController> logger)
        {
            _shoppingCartService=shoppingCartService;
            _catalogService=catalogService;
            _logger=logger;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = HttpContext.User.GetAppUser();
                var vm = await _shoppingCartService.GetCartAsync(user);

                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching shopping cart");
                ViewBag.Error = $"Error occured - {ex.GetType().Name} - {ex.Message}";
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<string, int> quantities, string action)
        {
            try
            {
                var user = HttpContext.User.GetAppUser();
                var vm = await _shoppingCartService.SetQuantitiesAsync(user, quantities);
                if (action == "Checkout")
                {
                    return RedirectToAction("Create", "Order", vm);
                }

                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating shopping cart");
                ViewBag.Error = $"Error occured - {ex.GetType().Name} - {ex.Message}";
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CatalogItem catalogItem)
        {
            try
            {
                if (catalogItem?.Id != null)
                {
                    var item = await _catalogService.GetCatalogItemById(catalogItem.Id);

                    var user = HttpContext.User.GetAppUser();
                    var currentShoppingCart = await _shoppingCartService.GetCartAsync(user) ?? new ShoppingCart(user.Id);
                    var product = currentShoppingCart.Items.SingleOrDefault(i => i.ProductId == item.Id);

                    if (product != null)
                    {
                        product.UnitQuantity += 1;
                    }
                    else
                    {
                        currentShoppingCart.Items.Add(new ShoppingCartItem()
                        {
                            UnitPrice = item.Price,
                            ProductId = item.Id,
                            ProductName = item.Name,
                            UnitQuantity = 1,
                            Id = Guid.NewGuid().ToString()
                        });
                    }
                    await _shoppingCartService.UpdateCartAsync(currentShoppingCart);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating shopping cart");
                ViewBag.Error = $"Error occured - {ex.GetType().Name} - {ex.Message}";
            }

            return RedirectToAction("Index", "Home", new { errorMsg = ViewBag.BasketInoperativeMsg });
        }

        public async Task<IActionResult> RemoveFromCart(string id)
        {
            try
            {
                var user = HttpContext.User.GetAppUser();
                var vm = await _shoppingCartService.RemoveItemFromCartAsync(user, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing from shopping cart");
                ViewBag.Error = $"Error occured - {ex.GetType().Name} - {ex.Message}";
            }

            return RedirectToAction("Index");
        }   

        #region Private members
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICatalogService _catalogService;
        private readonly ILogger<ShoppingCartController> _logger;
        #endregion
    }
}
