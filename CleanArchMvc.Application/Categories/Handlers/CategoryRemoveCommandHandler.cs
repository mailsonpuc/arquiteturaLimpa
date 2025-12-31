using CleanArchMvc.Application.Categories.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Categories.Handlers;

public class CategoryRemoveCommandHandler : IRequestHandler<CategoryRemoveCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryRemoveCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetById(request.CategoryId);

        if (category == null)
        {
            throw new ApplicationException("Error removing entity");
        }

        return await _categoryRepository.Remove(category);
    }
}
