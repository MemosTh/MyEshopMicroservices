using Catalog.API.Exceptions;
using Marten.Pagination;

namespace Catalog.API.Products.GetProductsById;

public record GetProductQuery(Guid guid) : IQuery<GetProductResult>;
public record GetProductResult(Product Product);

internal class GetProductByIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.guid, cancellationToken);
        
        if (product == null)
            throw new ProductNotFoundException(query.guid);

        return new GetProductResult(product);
    }
}
