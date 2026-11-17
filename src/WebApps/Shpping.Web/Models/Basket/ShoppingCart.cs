namespace Shpping.Web.Models.Basket
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }

    public record GetBasketResponse(ShoppingCart Cart);
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);
    public record DeleteBasketResponse(bool IsSucsess);
}
