namespace SEM3.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; } = null!;
        public int? Price { get; set; }
        public int? GroupId { get; set; }
    }
}
