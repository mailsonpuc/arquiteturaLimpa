

using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
    Task<CategoryDTO> GetByIdAsync(int? id);
    Task AddAsync(CategoryDTO categoryDto);
    Task UpdateAsync(CategoryDTO categoryDto);
    Task RemoveAsync(int? id);

}
