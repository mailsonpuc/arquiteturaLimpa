using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Application.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Logging;

namespace CleanArchMvc.WebUI.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProductsController(IMediator mediator, IMapper mapper, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return View(productsDto);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            ViewBag.Categories = _mapper.Map<IEnumerable<CleanArchMvc.Application.DTOs.CategoryDTO>>(categories);
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO productDto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _mediator.Send(new GetCategoriesQuery());
                ViewBag.Categories = _mapper.Map<IEnumerable<CleanArchMvc.Application.DTOs.CategoryDTO>>(categories);
                return View(productDto);
            }

            // handle image upload
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(uploads);
                var ext = Path.GetExtension(imageFile.FileName);
                var fileName = $"{Guid.NewGuid()}{ext}";
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                // store relative path or filename
                productDto.Image = Path.Combine("images", fileName).Replace("\\","/");
            }

            var command = new ProductCreateCommand
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                Image = productDto.Image,
                CategoryId = productDto.CategoryId
            };

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _mediator.Send(new GetProductByIdQuery(id.Value));
            if (product == null) return NotFound();

            var productDto = _mapper.Map<ProductDTO>(product);
            var categories = await _mediator.Send(new GetCategoriesQuery());
            ViewBag.Categories = _mapper.Map<IEnumerable<CleanArchMvc.Application.DTOs.CategoryDTO>>(categories);
            return View(productDto);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDTO productDto, IFormFile? imageFile)
        {
            if (id != productDto.ProductId) return BadRequest();
            if (!ModelState.IsValid)
            {
                var categories = await _mediator.Send(new GetCategoriesQuery());
                ViewBag.Categories = _mapper.Map<IEnumerable<CleanArchMvc.Application.DTOs.CategoryDTO>>(categories);
                return View(productDto);
            }

            // handle image upload (replace if provided)
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(uploads);
                var ext = Path.GetExtension(imageFile.FileName);
                var fileName = $"{Guid.NewGuid()}{ext}";
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                productDto.Image = Path.Combine("images", fileName).Replace("\\","/");
            }

            var command = new ProductUpdateCommand
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
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _mediator.Send(new GetProductByIdQuery(id.Value));
            if (product == null) return NotFound();

            var dto = _mapper.Map<ProductDTO>(product);
            return View(dto);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _mediator.Send(new GetProductByIdQuery(id.Value));
            if (product == null) return NotFound();

            var dto = _mapper.Map<ProductDTO>(product);
            return View(dto);
        }

        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new CleanArchMvc.Application.Products.Commands.ProductRemoveCommand(id));
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}