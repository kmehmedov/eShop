using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;
using WebMVC.ViewModels;

namespace WebMVC.ViewComponents
{
    public class ShoppingCart : ViewComponent
    {
        public ShoppingCart(IShoppingCartService shoppingCartService)
        {
            this._shoppingCartService=shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync(AppUser user)
        {
            var vm = new ViewModels.ShoppingCartViewModels.ShoppinCartComponentViewModel();
            try
            {
                var shoppingCart = await _shoppingCartService.GetCartAsync(user);
                vm.ItemsCount = shoppingCart.Items.Count;
            }
            catch { }

            return View(vm);
        }

        #region Private members
        private readonly IShoppingCartService _shoppingCartService;
        #endregion
    }
}
