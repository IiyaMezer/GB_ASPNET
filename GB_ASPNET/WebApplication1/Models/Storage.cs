namespace WebApplication1.Models;

public class Storage : BaseModel
{
    public virtual List<Product> Products { get; set; }
    public  Product  Product{ get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }

}