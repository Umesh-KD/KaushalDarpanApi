using AutoMapper;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;

namespace UoWAdoDotNetApiCurd.Api.MappingProfiles
{
    public class ProductDetailsProfile : Profile
    {
        public ProductDetailsProfile()
        {
            CreateMap<ProductDetails, ProductDetailsModel>();
            CreateMap<ProductDetailsCreateModel, ProductDetails>();
            CreateMap<ProductDetailsIsActiveModel, ProductDetails>();
        }
    }
}
