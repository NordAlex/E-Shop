using EShop.Catalog.Domain.Common;

namespace EShop.Catalog.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category> ChildCategory { get; } = new();
    }
}
