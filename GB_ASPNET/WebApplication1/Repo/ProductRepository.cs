using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using WebApplication1.Abstracts;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Repo
{
    public class ProductRepository : IProductRepository
    {
       private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }

       

        public int AddProduct(ProductDTO product)
        {
            using (var context = new StoreContext())
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Models.Product>(product);
                    context.Products.Add(entityProduct);
                    context.SaveChanges();
                    _cache.Remove("products");
                }
                return entityProduct.Id;
            }
        }
        public IEnumerable<ProductDTO> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDTO> products))
            {
                return products;
            }
            using (StoreContext context = new())
            {
                var productsList = context.Products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
                _cache.Set("products", productsList, TimeSpan.FromMinutes(30));
                return productsList;
            }
        }
    }
}
