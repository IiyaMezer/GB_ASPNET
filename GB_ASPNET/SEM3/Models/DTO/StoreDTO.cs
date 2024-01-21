namespace SEM3.Models.DTO
{
    public class StoreDTO
    {
        public int Count { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; } = null!;
    }
}
