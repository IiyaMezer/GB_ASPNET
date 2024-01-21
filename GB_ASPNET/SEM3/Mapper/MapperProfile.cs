using SEM3.Models;
using SEM3.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace SEM3.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Group, GroupDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Store, StoreDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
