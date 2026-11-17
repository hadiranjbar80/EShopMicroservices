namespace Shpping.Web.Pages
{
    public class CartModel 
        (IBasketService basketService, ILogger<CartModel> logger)
        : PageModel
    {
        public ShoppingCart Cart { get; set; } = new ShoppingCart();
        public async Task<IActionResult> OnGet()
        {
            Cart = await basketService.LoadUserBasket();
            
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
        {
            logger.LogInformation("Remove to cart button clicked");
            Cart = await basketService.LoadUserBasket();

            Cart.Items.RemoveAll(x=>x.ProductId == productId);

            await basketService.StoreBasket(new StoreBasketRequest(Cart));

            return RedirectToPage();
        }
    }
}
