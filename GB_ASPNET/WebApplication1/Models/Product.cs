using System.Data.SqlTypes;

namespace WebApplication1.Models
{
    public class Product: BaseModel
    {
        public int Cost { get; set; }
        public int GroupId { get; set; }
        public virtual ProductGroup? ProductGroup { get; set; }
        public virtual List<ProductStorage> Products { get; set; } = new List<ProductStorage>();
    }
}
