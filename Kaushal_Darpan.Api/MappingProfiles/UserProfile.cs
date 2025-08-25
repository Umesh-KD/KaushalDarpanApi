using AutoMapper;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Models;

namespace UoWAdoDotNetApiCurd.Api.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Users, UserModel>();
        }
    }
}
