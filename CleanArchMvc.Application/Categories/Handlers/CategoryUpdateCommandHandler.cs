using CleanArchMvc.Application.Categories.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Categories.Handlers;

public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryUpdateCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetById(request.CategoryId);

        if (category == null)
        {
            throw new ApplicationException("Error updating entity");
        }

        category.Update(request.Name!);
        return await _categoryRepository.Update(category);
    }
}
