using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SEM3.Abstractions;
using SEM3.Models;
using SEM3.Models.DTO;

namespace SEM3.Services
{
    public class StoreServices : IStorageServices
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public StoreServices(StoreContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }


        public int AddStores(StoreDTO store)
        {
            using (_context)
            {
                var entity = _mapper.Map<Store>(store);
                _context.Stores.Add(entity);
                _context.SaveChanges();
                _cache.Remove("stores");

                return entity.Id;
            }
        }

        public IEnumerable<ProductDTO> GetProductsByStoreId(int? storeId)
        {

            if (storeId != null)
            {
                if (_context.Stores.Find(storeId) != null)
                {
                    return _context.Products
                   .Where(p => p.Stores.Any(s => s.Id == storeId))
                   .Select(x => _mapper.Map<ProductDTO>(x))
                   .ToList();
                }
                else
                {
                    return Enumerable.Empty<ProductDTO>();
                }
                
            }
            else { throw new ArgumentNullException(nameof(storeId), "storeId cannot be null."); }
        }

        public IEnumerable<StoreDTO> GetStores()
        {
            if (_cache.TryGetValue("stores", out List<StoreDTO> strores))
                return strores;
            strores = _context.Stores.Select(x => _mapper.Map<StoreDTO>(x)).ToList();
            _cache.Set("stores", strores, TimeSpan.FromMinutes(30));
            return strores;
        }
    }
}
