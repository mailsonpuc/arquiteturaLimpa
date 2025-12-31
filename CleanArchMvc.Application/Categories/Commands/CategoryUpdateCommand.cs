using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Categories.Commands;

public class CategoryUpdateCommand : IRequest<Category>
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
}
