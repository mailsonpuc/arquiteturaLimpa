using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Categories.Commands;

public class CategoryRemoveCommand : IRequest<Category>
{
    public int CategoryId { get; set; }

    public CategoryRemoveCommand(int categoryId)
    {
        CategoryId = categoryId;
    }
}
