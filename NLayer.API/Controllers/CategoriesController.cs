using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProductAsync(int id)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductAsync(id));
        }
    }
}
