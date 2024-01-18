﻿using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
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

        public byte[] GetBytesForCsv()
        {
            using (StoreContext context = new())
            {
                List<Product> products = context.Products.ToList();
                using (MemoryStream memoryStream = new())
                {
                    using (StreamWriter streamWriter = new(memoryStream))
                    {
                        streamWriter.WriteLine("Id, Name, Description");

                        foreach (Product product in products)
                        {
                            streamWriter.WriteLine($"{product.Id}_{product.Name}_{product.Description}");
                        }
                        streamWriter.Flush();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        public string GetCache()
        {
            var currentCatche = _cache.GetCurrentStatistics();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Entry count: {currentCatche.CurrentEntryCount}");
            stringBuilder.AppendLine($"Size: {currentCatche.CurrentEstimatedSize}");
            stringBuilder.AppendLine($"Misses: {currentCatche.TotalMisses}");
            stringBuilder.AppendLine($"Hits: {currentCatche.TotalHits}");
            stringBuilder.AppendLine($"_________________________________");
            stringBuilder.AppendLine(currentCatche.ToString());

            return stringBuilder.ToString();
        }
    }
}
