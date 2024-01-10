namespace WebApplication1.Models
{
    public class ProductGroup: BaseModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
