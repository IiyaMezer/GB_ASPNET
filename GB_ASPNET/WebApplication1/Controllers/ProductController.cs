using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("getProducts")]
        public IActionResult GetProducts()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    var products = context.Products.Select(x => new Product()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    });
                    return Ok(products);

                }
            }
            catch
            {

                return StatusCode(500);
            }
            return Ok();

        }
        [HttpPost("putProducts")]
        public IActionResult PutProducts([FromQuery] string name, string description, int groupId, int price)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (!context.Products.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Product()
                        {
                            Name = name,
                            Description = description,
                            Price = price,
                            GroupID = groupId
                        });
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        StatusCode(409);
                    }

                }
            }
            catch
            {

                return StatusCode(500);
            }
            return Ok();

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
    }
}
