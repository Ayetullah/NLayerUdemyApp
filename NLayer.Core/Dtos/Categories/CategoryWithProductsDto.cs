using NLayer.Core.Dtos.Products;

namespace NLayer.Core.Dtos.Categories
{
    public class CategoryWithProductsDto:CategoryDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
