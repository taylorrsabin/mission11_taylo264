using Microsoft.AspNetCore.Mvc;
using mission11.Models;

//View component for the shopping cart

namespace mission11.Components
{

    public class CartSummaryViewComponent : ViewComponent
    {
        private Basket basket;

        public CartSummaryViewComponent(Basket cartService)
        {
            basket = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(basket);
        }
    }
}