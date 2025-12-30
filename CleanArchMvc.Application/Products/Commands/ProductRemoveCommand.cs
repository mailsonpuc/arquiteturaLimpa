

using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Products.Commands;

public class ProductRemoveCommand : IRequest<Product>
{
    public int ProductId { get; set; }

    public ProductRemoveCommand(int productId)
    {
        ProductId = productId;
    }


}
