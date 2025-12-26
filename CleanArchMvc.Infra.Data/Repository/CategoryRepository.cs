

using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;

namespace CleanArchMvc.Infra.Data.Repository;

public class CategoryRepository : ICategoryRepository
{
    AppDbContext _Categorycontext;
    public CategoryRepository(AppDbContext context)
    {
        _Categorycontext = context;
    }


    public async Task<Category> Create(Category category)
    {
        _Categorycontext.Add(category);
        await _Categorycontext.SaveChangesAsync();
        return category;
    }



    public async Task<Category> GetById(int? id)
    {
       return await _Categorycontext.Categories.FindAsync(id);
    }



    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _Categorycontext.Categories.AsAsyncEnumerable().ToListAsync();
    }



    public async Task<Category> Remove(Category category)
    {
        _Categorycontext.Remove(category);
        await _Categorycontext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        _Categorycontext.Update(category);
        await _Categorycontext.SaveChangesAsync();
        return category;
    }
}
