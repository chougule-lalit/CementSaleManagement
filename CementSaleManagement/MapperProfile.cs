using AutoMapper;
using CementSaleManagement.Contract.Dto;
using CementSaleManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserMaster, UserMasterDto>()
                .ForMember(dest => dest.RoleName, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UserMaster, UserMasterCreateUpdateDto>()
                .ReverseMap();

            CreateMap<RoleMaster, RoleMasterDto>()
                .ReverseMap();

            CreateMap<Enquiry, EnquiryDto>()
                .ReverseMap();
        }
    }
}
