using NLayer.Core.Dtos;
using NLayer.Core.Dtos.Categories;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface ICategoryService: IService<Category>
    {
        public Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductAsync(int categoryId);
    }
}
