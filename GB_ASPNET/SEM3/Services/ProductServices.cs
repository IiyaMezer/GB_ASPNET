using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SEM3.Abstractions;
using SEM3.Models;
using SEM3.Models.DTO;

namespace SEM3.Services
{
    public class ProductServices : IProductServices
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductServices(StoreContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProduct(ProductDTO product)
        {

            var entity = _mapper.Map<Product>(product);
            _context.Products.Add(entity);
            _context.SaveChanges();
            _cache.Remove("products");

            return entity.Id;


        }

        public IEnumerable<ProductDTO> GetProducts()
        {

            if (_cache.TryGetValue("products", out List<ProductDTO> products))
                return products;
            products = _context.Products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
            _cache.Set("products", products, TimeSpan.FromMinutes(30));
            return products;


        }
    }
}
