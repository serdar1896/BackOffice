using AutoMapper;
using Inveon.Core.Models;
using Inveon.Core.Models.DTOs;
using System;

namespace Inveon.Service.Mapping
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<User, LoginDto>();
            CreateMap<LoginDto, User>();

            CreateMap<User, RegisterDto>();
            CreateMap<RegisterDto, User>()
                .ForMember(des=>des.Role,opt=>opt.MapFrom(src=>0))
                .ForMember(des=>des.Status,opt=>opt.MapFrom(src=>true))
                .ForMember(des=>des.RegistirationDate,opt=>opt.MapFrom(src=>DateTime.Now));

            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<UserDto, User>()
                .ForMember(des=>des.Status,opt=>opt.MapFrom(src=>src.Status))
                .ForMember(des=>des.RegistirationDate,opt=>opt.MapFrom(src=>src.RegistirationDate??DateTime.Now));
            CreateMap<User, UserDto>();


        }
    }
}
