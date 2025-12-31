using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Categories.Commands;

public class CategoryCreateCommand : IRequest<Category>
{
    public string? Name { get; set; }
}
