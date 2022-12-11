using NLayer.Core.Dtos.Categories;

namespace NLayer.Core.Dtos.Products
{
    public class ProductWithCategoryDto:ProductDto
    {
        public CategoryDto Category { get; set; }
    }
}
