using Microsoft.AspNetCore.Mvc;
using WebApplication1.Abstracts;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("getProducts")]
        public IActionResult GetProducts()
        {
            var products = _repository.GetProducts();
            return Ok();

        }
        [HttpPost("addProducts")]
        public IActionResult AddProducts([FromBody] ProductDTO productDto)
        {

            var result = _repository.AddProduct(productDto);
            return Ok(result);

        }
        [HttpDelete("deleteProduct/{productId}")]
        public IActionResult DelProducts([FromQuery] int Id)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (context.Products.Any(x => x.Id == Id))
                    {
                        Product product = context.Products.FirstOrDefault(x => x.Id == Id);
                        context.Remove(product);
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        StatusCode(404);
                    }

                }
            }
            catch
            {

                return StatusCode(500);
            }
            return Ok();

        }
        [HttpPut("updatePrice/{productId}")]
        public IActionResult SetPrice([FromQuery] int productId, int price)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (context.Products.Any(x => x.Id == productId))
                    {
                        Product product = context.Products.FirstOrDefault(x => x.Id == productId);
                        product.Price = price;
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        StatusCode(404);
                    }

                }
            }
            catch
            {

                return StatusCode(500);
            }
            return Ok();
        }

        [HttpGet("productsToCsv")]
        public FileContentResult ExportToCsv()
        {

            byte[] filebytes = _repository.GetBytesForCsv();
            return File(filebytes, "text/csv", "products.csv");
        }

        [HttpGet("cacheStats")]
        public ActionResult<string> CacheStats()
        {
            string cache = _repository.GetCache();
            if (cache != null)
            {
                string filename = $"products_{DateTime.Now.ToBinary().ToString()}.csv";
                System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", filename), cache);
                return $"https://{Request.Host.ToString()}/static/{filename}";
            }
            return StatusCode(500);
        }
    }
}
