namespace Shpping.Web.Models.Catalog
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }
    }

    public record GetProductResponse(IEnumerable<Product> Products);
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public record GetProductByIdResponse(Product Product);
}
