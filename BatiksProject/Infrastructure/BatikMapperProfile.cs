using AutoMapper;
using BatiksProject.Dto;
using BatiksProject.Models;
using BatiksProject.ViewModels;

namespace BatiksProject.Infrastructure
{
    public class BatikMapperProfile : Profile
    {
        public BatikMapperProfile()
        {
            // Dashboard
            CreateMap<DashboardDto, DashboardViewModel>();

            // Users
            CreateMap<User, UserDto>();
            CreateMap<User, UserEditViewModel>()
                .ForMember(dest => dest.Password, expr => expr.Ignore());
            CreateMap<UserEditViewModel, User>();

            // Batik
            CreateMap<Batik, BatikDto>()
                .ForMember(dest => dest.ImageUrl, expr => expr.MapFrom(x => "http://127.0.0.1/" + x.MinioObjectName))
                .ForMember(dest => dest.Locality, expr => expr.MapFrom(x => x.Locality.Name));
        }
    }
}
