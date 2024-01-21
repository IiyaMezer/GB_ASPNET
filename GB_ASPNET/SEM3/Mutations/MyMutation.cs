using SEM3.Abstractions;
using SEM3.Models.DTO;

namespace SEM3.Mutations
{
    public class MyMutation
    {
        public int AddProduct([Service] IProductServices services, ProductDTO product)
        {
            var id = services.AddProduct(product);
            return id;
        }
    }
}
