
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class CategoryService : ICategoryService
{

    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;


    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }


    public async Task AddAsync(CategoryDTO categoryDto)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.Create(categoryEntity);
    }

    public async Task<CategoryDTO> GetByIdAsync(int? id)
    {
        var categoryEntity = await _categoryRepository.GetById(id);
        return _mapper.Map<CategoryDTO>(categoryEntity);
    }



    public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
    {
        var categoriesEntities = await _categoryRepository.GetCategories();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntities);
    }



    public async Task RemoveAsync(int? id)
    {
        var categoryEntity = await _categoryRepository.GetById(id);
        await _categoryRepository.Remove(categoryEntity);
    }



    public async Task UpdateAsync(CategoryDTO categoryDto)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.Update(categoryEntity);
    }
}
