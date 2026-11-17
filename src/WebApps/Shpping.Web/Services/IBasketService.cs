
namespace Shpping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/Basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/Basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

        public async Task<ShoppingCart> LoadUserBasket()
        {
            var userName = "swn";
            ShoppingCart basket;

            try
            {
                var getBasketResponse = await GetBasket(userName);
                basket = getBasketResponse.Cart;
            }
            catch (ApiException apiException) when (apiException.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                basket = new ShoppingCart
                {
                    UserName = userName,
                    Items = []
                };
            }

            return basket;
        }
    }
}
