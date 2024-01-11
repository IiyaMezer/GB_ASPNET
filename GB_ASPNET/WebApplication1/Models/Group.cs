using WebApplication1.Models.Base;

namespace WebApplication1.Models;

public class Group: BaseModel
{
    public List<Product> Products { get; set; } = new List<Product>();
}
