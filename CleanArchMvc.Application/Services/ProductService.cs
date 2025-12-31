

using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    // private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public ProductService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }



    // public async Task AddAsync(ProductDTO productDto)
    // {
    //     var productEntity = _mapper.Map<Product>(productDto);
    //     await _productRepository.Create(productEntity);
    // }



    // public async Task<ProductDTO> GetByIdAsync(int? id)
    // {
    //     var productEntity = await _productRepository.GetById(id);
    //     return _mapper.Map<ProductDTO>(productEntity);
    // }



    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {

        // var productEntities = await _productRepository.GetProducts();
        // return _mapper.Map<IEnumerable<ProductDTO>>(productEntities);

        var query = new GetProductsQuery();
        if (query == null) 
        {
            throw new ArgumentNullException(nameof(query));
        }

        var productEntities = await _mediator.Send(query);
        return _mapper.Map<IEnumerable<ProductDTO>>(productEntities);

    }



    // public async Task RemoveAsync(int? id)
    // {
    //     var productEntity = await _productRepository.GetById(id);
    //     await _productRepository.Remove(productEntity);
    // }



    // public async Task UpdateAsync(ProductDTO productDto)
    // {
    //     var productEntity = _mapper.Map<Product>(productDto);
    //     await _productRepository.Update(productEntity);
    // }


}
