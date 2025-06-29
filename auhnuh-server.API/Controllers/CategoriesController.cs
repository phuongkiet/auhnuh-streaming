using auhnuh_server.Application.IService;
using auhnuh_server.Domain.DTO.WebRequest.Category;
using Microsoft.AspNetCore.Mvc;

namespace auhnuh_server.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryService.GetCategories());
        }

        [HttpGet("category-detail")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] RequestCategoryDTO requestCategoryDTO, CancellationToken cancellationToken)
        {
            if (requestCategoryDTO == null)
            {
                return BadRequest("Invalid category data.");
            }
            var response = await _categoryService.AddCategory(requestCategoryDTO, cancellationToken);
            if (response.Errors.Any())
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(GetCategoryById), new { id = response.Data }, response);
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] RequestCategoryDTO requestCategoryDTO, CancellationToken cancellationToken)
        {
            if (requestCategoryDTO == null)
            {
                return BadRequest("Invalid category data.");
            }
            var response = await _categoryService.UpdateCategory(id, requestCategoryDTO, cancellationToken);
            if (response.Errors.Any())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var response = await _categoryService.DeleteCategory(id, cancellationToken);
            if (response.Errors.Any())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
