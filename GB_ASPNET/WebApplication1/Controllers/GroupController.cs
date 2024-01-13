using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {

        [HttpDelete("deleteGroup/{groupId}")]
        public IActionResult DelGroup([FromQuery] int groupId)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (context.Groups.Any(x => x.Id == groupId))
                    {
                        Group group = context.Groups.FirstOrDefault(x => x.Id == groupId);
                        context.Remove(group);
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
