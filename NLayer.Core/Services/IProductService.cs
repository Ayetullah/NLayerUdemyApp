using NLayer.Core.Dtos;
using NLayer.Core.Dtos.Products;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product, List<ProductWithCategoryDto>>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();
        Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto entity);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto);
    }
}
