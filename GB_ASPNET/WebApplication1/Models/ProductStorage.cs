namespace WebApplication1.Models;

public class ProductStorage
{
    public int? StorageId { get; set; }
    public int? ProductId { get; set;}

    public virtual Product? Product { get; set;}
    public virtual Storage? Storage { get; set; }
}
