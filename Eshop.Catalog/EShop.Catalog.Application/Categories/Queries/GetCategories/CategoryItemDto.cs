namespace EShop.Catalog.Application.Categories.Queries.GetCategories
{
    public class CategoryItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoryId { get; set; }
    }
}
