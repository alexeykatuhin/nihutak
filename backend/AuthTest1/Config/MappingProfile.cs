using AuthTest.Core.DTO;
using AuthTest.Core.DTO.Photo;
using AuthTest.Core.DTO.User;
using AuthTest.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTest.API.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityUser, UserInfoDto>()
                .ForMember(x => x.userName, y => y.MapFrom(z => z.UserName))
                .ForMember(x=>x.id, y=>y.MapFrom(z=>z.Id))
                ;

            CreateMap<AuthenticationScheme, AuthProviderDto>();

            CreateMap<Photo, PhotoDto>()
                .ForMember(x => x.Tags, y => y.MapFrom(z => z.PhotoTags.Select(u => new SimpleDto { Id = u.TagId, Name = u.Tag.Name })))
                .ForMember(x => x.City, y => y.MapFrom(z => new SimpleDto { Id = z.City.Id, Name = z.City.Name }))
                .ForMember(x => x.Country, y => y.MapFrom(z => new CountryDto { Id = z.City.Country.Id, Name = z.City.Country.Name, Code = z.City.Country.Code }))
                .ForMember(x=>x.Date, y => y.MapFrom(z=>z.CreatedDt.ToShortDateString()));
        }
    }
}
