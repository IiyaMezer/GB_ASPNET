using WebApplication1.Models;

namespace WebApplication1.Models.DTO
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; } = null!;
    }
}
