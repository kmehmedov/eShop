using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;
using WebMVC.ViewModels;
using WebMVC.ViewModels.OrderViewModels;

namespace WebMVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public OrderController(IShoppingCartService shoppingCartService, IOrderService orderService, ILogger<OrderController> logger)
        {
            _shoppingCartService=shoppingCartService;
            _orderService = orderService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = HttpContext.User.GetAppUser();
                var orders = await _orderService.GetAll(user);
                var vm = new IndexViewModel()
                {
                    Orders = orders.OrderByDescending(x => x.Date)
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching orders");
                ViewBag.Error = $"Error occured - {ex.GetType().Name} - {ex.Message}";
            }

            return View();
        }
        public async Task<IActionResult> Create()
        {
            var shoppingCart = await _shoppingCartService.GetCartAsync(HttpContext.User.GetAppUser());

            if (shoppingCart == null)
            {
                return BadRequest($"No shopping cart found");
            }

            var order = await _orderService.CreateDraftFromShoppinCartAsync(shoppingCart);

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order model)
        {
            try
            {
                await _orderService.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                ViewBag.Error = $"Error occured - {ex.GetType().Name} - {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _orderService.Cancel(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error canceling order");
                ViewBag.Error = $"Error occured - {ex.GetType().Name} - {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        #region Private members
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        #endregion
    }
}
