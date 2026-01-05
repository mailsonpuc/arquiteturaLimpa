using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;

namespace CleanArchMvc.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProductsAsync();
            if (products == null)
            {
                return NotFound("Products not found");
            }

            return Ok(products);
        }


        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid data.");
            }

            await _productService.AddAsync(productDto);
            return CreatedAtRoute("GetProductById", new { id = productDto.ProductId }, productDto);
        }


        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid data.");
            }

            await _productService.UpdateAsync(productDto);
            return Ok(productDto);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productDto = await _productService.GetByIdAsync(id);
            if (productDto == null)
            {
                return NotFound("Product not found.");
            }

            await _productService.RemoveAsync(id);
            return NoContent();
        }
        
    }
}