namespace NLayer.Core.Models
{
    public class Category: BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<ProductFeature> Products { get; set; }
    }
}
