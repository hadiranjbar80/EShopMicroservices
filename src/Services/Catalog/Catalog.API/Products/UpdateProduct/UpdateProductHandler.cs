namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id, string name, List<string> category, string description, string ImageFile, decimal price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Product ID is required");

            RuleFor(x=>x.name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2,150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(x => x.price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class UpdateProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(command.id);
            }

            product.Name = command.name;
            product.Category = command.category;
            product.Description = command.description;
            product.ImageFile = command.ImageFile;
            product.Price = command.price;

            session.Update(product);
            await session.SaveChangesAsync();

            return new UpdateProductResult(true);
        }
    }
}
