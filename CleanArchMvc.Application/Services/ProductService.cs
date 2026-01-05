

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


    public async Task<ProductDTO> GetByIdAsync(int? id)
    {
        if (id == null) return null!;

        var query = new CleanArchMvc.Application.Products.Queries.GetProductByIdQuery(id.Value);
        var product = await _mediator.Send(query);
        return _mapper.Map<ProductDTO>(product);
    }


    public async Task AddAsync(ProductDTO productDto)
    {
        if (productDto == null) return;

        var command = new CleanArchMvc.Application.Products.Commands.ProductCreateCommand
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Stock = productDto.Stock,
            Image = productDto.Image,
            CategoryId = productDto.CategoryId
        };

        await _mediator.Send(command);
    }


    public async Task UpdateAsync(ProductDTO productDto)
    {
        if (productDto == null) return;

        var command = new CleanArchMvc.Application.Products.Commands.ProductUpdateCommand
        {
            ProductId = productDto.ProductId,
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Stock = productDto.Stock,
            Image = productDto.Image,
            CategoryId = productDto.CategoryId
        };

        await _mediator.Send(command);
    }


    public async Task RemoveAsync(int? id)
    {
        if (id == null) return;

        var command = new CleanArchMvc.Application.Products.Commands.ProductRemoveCommand(id.Value);
        await _mediator.Send(command);
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
