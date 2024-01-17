using WebApplication1.Abstracts;
using WebApplication1.Models.DTO;
using WebApplication1.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApplication1.Repo
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GroupRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }
        public int AddGroup(GroupDTO group)
        {

            using (var context = new StoreContext())
            {
                var entityGroup = context.Groups.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
                if (entityGroup == null)
                {
                    entityGroup = _mapper.Map<Models.Group>(group);
                    context.Groups.Add(entityGroup);
                    context.SaveChanges();
                    _cache.Remove("groups");
                }
                return entityGroup.Id;
            }
        }
        public IEnumerable<GroupDTO> GetGroups()
        {
            if (_cache.TryGetValue("groups", out List<GroupDTO> groups))
            {
                return groups;
            }
            
            using (StoreContext context = new())
            {
                var groupsList = context.Groups.Select(x => _mapper.Map<GroupDTO>(x)).ToList();
                _cache.Set("groups", groupsList, TimeSpan.FromMinutes(30));
                return groupsList;
            }

        }
    }
}
