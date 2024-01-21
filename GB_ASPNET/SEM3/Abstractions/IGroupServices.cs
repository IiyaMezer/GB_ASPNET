using SEM3.Models.DTO;

namespace SEM3.Abstractions
{
    public interface IGroupServices
    {
        public IEnumerable<GroupDTO> GetGroups();
        public int AddGroup(GroupDTO group);
    }
}