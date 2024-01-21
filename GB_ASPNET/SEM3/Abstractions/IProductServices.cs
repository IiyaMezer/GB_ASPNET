using SEM3.Models.DTO;
using System.Collections;

namespace SEM3.Abstractions
{
    public interface IProductServices
    {
        public IEnumerable<ProductDTO> GetProducts();
        public int AddProduct(ProductDTO product);
    }
}
