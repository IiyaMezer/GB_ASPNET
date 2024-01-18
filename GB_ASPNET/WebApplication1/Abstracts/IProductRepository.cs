using System.Text.RegularExpressions;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Abstracts
{
    public interface IProductRepository
    {
        public int AddProduct(ProductDTO product);

        public IEnumerable<ProductDTO> GetProducts();
        public byte[] GetBytesForCsv();
        public string GetCache();

    }
}
