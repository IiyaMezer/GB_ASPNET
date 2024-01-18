using Microsoft.AspNetCore.Mvc;
using WebApplication1.Abstracts;
using WebApplication1.Models;
using WebApplication1.Models.DTO;


namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupRepository _repository;

    public GroupController(IGroupRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("getGroups")]
    public IActionResult GetGroups()
    {
        var groups = _repository.GetGroups();
        return Ok(groups);

    }
    [HttpPost("addGroups")]
    public IActionResult AddGroups([FromBody] GroupDTO groupsDto)
    {

        var result = _repository.AddGroup(groupsDto);
        return Ok(result);

    }

    [HttpDelete("deleteGroup/{group.Id}")]
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

    [HttpGet("groupsToCsv")]
    public FileContentResult ExportToCsv()
    {

        byte[] filebytes = _repository.GetBytesForCsv();
        return File(filebytes, "text/csv", "groups.csv");
    }



}
