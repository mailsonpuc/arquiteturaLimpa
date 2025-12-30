

using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Products.Queries;

public class GetProductByIdQuery : IRequest<Product>
{
    public int ProductId { get; set; }

    public GetProductByIdQuery(int productId)
    {
        ProductId = productId;
    }
}
