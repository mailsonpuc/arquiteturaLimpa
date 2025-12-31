using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.Categories.Commands;
using CleanArchMvc.Application.Categories.Queries;
using CleanArchMvc.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArchMvc.WebUI.Controllers
{
    [Route("[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return View(categoriesDto);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid) return View(categoryDto);

            await _mediator.Send(new CategoryCreateCommand { Name = categoryDto.Name });
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (category == null) return NotFound();

            var dto = _mapper.Map<CategoryDTO>(category);
            return View(dto);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId) return BadRequest();
            if (!ModelState.IsValid) return View(categoryDto);

            await _mediator.Send(new CategoryUpdateCommand { CategoryId = id, Name = categoryDto.Name });
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (category == null) return NotFound();

            var dto = _mapper.Map<CategoryDTO>(category);
            return View(dto);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (category == null) return NotFound();

            var dto = _mapper.Map<CategoryDTO>(category);
            return View(dto);
        }

        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new CategoryRemoveCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}