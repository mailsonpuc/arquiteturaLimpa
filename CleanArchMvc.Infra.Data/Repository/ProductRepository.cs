

using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repository;

public class ProductRepository : IProductRepository
{
    AppDbContext _Productcontext;
    public ProductRepository(AppDbContext context)
    {
        _Productcontext = context;
    }



    public async Task<Product> Create(Product product)
    {
        _Productcontext.Add(product);
        await _Productcontext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> GetById(int? id)
    {
        return await _Productcontext.Products.FindAsync(id);

    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _Productcontext.Products.ToListAsync();
    }


    public async Task<Product> GetProductWithCategory(int? id)
    {
        return await _Productcontext.Products
            .Include(c => c.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<Product> Remove(Product product)
    {
        _Productcontext.Remove(product);
        await _Productcontext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> Update(Product product)
    {
        _Productcontext.Update(product);
        await _Productcontext.SaveChangesAsync();
        return product;
    }
}
