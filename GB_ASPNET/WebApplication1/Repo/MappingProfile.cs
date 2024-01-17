using AutoMapper;
using WebApplication1.Models;
using WebApplication1.Models.DTO;


namespace WebApplication1.Repo;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDTO>(MemberList.Destination).ReverseMap();
        CreateMap<Group, GroupDTO>(MemberList.Destination).ReverseMap();
        CreateMap<Store, StoreDTO>(MemberList.Destination).ReverseMap();
    }
}
