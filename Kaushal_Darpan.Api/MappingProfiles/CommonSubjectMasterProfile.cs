using AutoMapper;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Models.CommonSubjectMaster;

namespace UoWAdoDotNetApiCurd.Api.MappingProfiles
{
    public class CommonSubjectMasterProfile : Profile
    {
        public CommonSubjectMasterProfile()
        {
            // get
            CreateMap<M_CommonSubject, CommonSubjectMasterModel>();
            CreateMap<M_CommonSubject_Details, CommonSubjectDetailsMasterModel>();

            // save
            CreateMap<CommonSubjectMasterModel, M_CommonSubject>();
            CreateMap<CommonSubjectDetailsMasterModel, M_CommonSubject_Details>();
        }
    }
}
