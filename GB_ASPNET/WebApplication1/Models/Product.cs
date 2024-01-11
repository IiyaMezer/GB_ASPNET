using System.Data.SqlTypes;
using WebApplication1.Models.Base;

namespace WebApplication1.Models;

public class Product: BaseModel
{
    public int Cost { get; set; }
    public int GroupId { get; set; }
    public virtual Group Group { get; set; } = null!;
    public virtual List<Storage> Storages { get; set; } = null!;
}
