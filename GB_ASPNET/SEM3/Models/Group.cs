using SEM3.Models.Base;

namespace SEM3.Models
{
    public class Group : BaseModel 
    {
      
        public virtual List<Product> Products { get; set; } = null!;
    }
}
