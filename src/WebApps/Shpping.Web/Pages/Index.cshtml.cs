
namespace Shpping.Web.Pages
{
    public class IndexModel 
        (ICatalogService catalogService, 
        IBasketService basketService, 
        ILogger<IndexModel> logger)
        : PageModel
    {
        public IEnumerable<Product> ProductList { get; set; } = new List<Product>();

        public async Task<IActionResult> OnGet()
        {
            logger.LogInformation("Index page visited");
            var result = await catalogService.GetProducts();
            ProductList = result.Products;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Add to cart button clicked");

            var productResponse = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItem
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = 1,
                Color = "Black"
            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }
    }
}
