
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            if(categories == null)
            {
                return NotFound("Categories not found");
            }

            return Ok(categories);
        }


        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if(category == null)
            {
                return NotFound("Category not found");
            }

            return Ok(category);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            if(categoryDto == null)
            {
                return BadRequest("Invalid data.");
            }

            await _categoryService.AddAsync(categoryDto);
            return CreatedAtRoute("GetCategoryById", new { id = categoryDto.CategoryId }, categoryDto);
        }


        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CategoryDTO categoryDto)
        {
            if(categoryDto == null)
            {
                return BadRequest("Invalid data.");
            }

            await _categoryService.UpdateAsync(categoryDto);
            return Ok(categoryDto);
        }
 
 
 
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryDto = await _categoryService.GetByIdAsync(id);
            if(categoryDto == null)
            {
                return NotFound("Category not found.");
            }

            await _categoryService.RemoveAsync(id);
            return NoContent();
        }




 
 
 
    }
}