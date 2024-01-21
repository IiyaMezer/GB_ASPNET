using SEM3.Models.Base;

namespace SEM3.Models
{
    public class Product : BaseModel
    {
        public string Description { get; set; } = null!;
        public int? GroupId { get; set; }
        public virtual Group? Group { get; set; }
        public int? StoreId { get; set; }
        public int Price { get; set; }
        public virtual List<Store> Stores { get; set; } = null!;
    }
}
