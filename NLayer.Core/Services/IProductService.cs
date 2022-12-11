using NLayer.Core.Dtos;
using NLayer.Core.Dtos.Products;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductService: IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();
    }
}
