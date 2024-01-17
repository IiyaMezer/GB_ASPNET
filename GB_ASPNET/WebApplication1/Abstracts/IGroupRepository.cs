using WebApplication1.Models.DTO;

namespace WebApplication1.Abstracts
{
    public interface IGroupRepository
    {
        public int AddGroup(GroupDTO group);

        public IEnumerable<GroupDTO> GetGroups();
    }
}
