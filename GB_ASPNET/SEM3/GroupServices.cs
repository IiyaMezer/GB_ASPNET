using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SEM3.Abstractions;
using SEM3.Models;
using SEM3.Models.DTO;

namespace SEM3
{
    public class GroupServices : IGroupServices
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GroupServices(StoreContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddGroup(GroupDTO group)
        {
            using (_context)
            {
                var entity = _mapper.Map<Group>(group);
                _context.Groups.Add(entity);
                _context.SaveChanges();
                _cache.Remove("groups");

                return entity.Id;
            }
        }

        public IEnumerable<GroupDTO> GetGroups()
        {
            if (_cache.TryGetValue("groups", out List<GroupDTO> groups))
                return groups;
            groups = _context.Groups.Select(x => _mapper.Map<GroupDTO>(x)).ToList();
            _cache.Set("groups", groups, TimeSpan.FromMinutes(30));
            return groups;
        }

    }
}
