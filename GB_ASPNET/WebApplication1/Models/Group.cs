using WebApplication1.Models.Base;

namespace WebApplication1.Models
{
    public class Group : BaseModel 
    {
      
        public virtual List<Product> Products { get; set; } = null!;
    }
}
