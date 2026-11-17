namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string name, List<string> category, string description, string ImageFile, decimal price)
    : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }


    public class CreateProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.name,
                Category = command.category,
                Description = command.description,
                ImageFile = command.ImageFile,
                Price = command.price
            };

            session.Store(product);
            await session.SaveChangesAsync();

            return new CreateProductResult(product.Id);
        }
    }
}
