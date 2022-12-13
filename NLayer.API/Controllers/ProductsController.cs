using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.Dtos;
using NLayer.Core.Dtos.Products;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _service;

        public ProductsController(IProductService productService)
        {
            _service = productService;
        }

        [HttpGet("GetProductsWithCategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _service.GetProductsWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            return CreateActionResult(products);
        }

        [ServiceFilter(typeof(NotFoundFilter<Product, ProductDto>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            return CreateActionResult(product);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto productDto)
        {
            var product = await _service.AddAsync(productDto);
            return CreateActionResult(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _service.UpdateAsync(productDto);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product, ProductDto>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.RemoveAsync(id);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
