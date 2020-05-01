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
            CreateMap<UserDto, UserEditViewModel>();
            CreateMap<UserEditViewModel, User>();

            // Locality
            CreateMap<Locality, LocalityDto>();
            CreateMap<Locality, LocalityEditViewModel>();
            CreateMap<LocalityDto, LocalityEditViewModel>();
            CreateMap<LocalityEditViewModel, Locality>();

            // Batik
            CreateMap<Batik, BatikDto>()
                .ForMember(dest => dest.Locality, expr => expr.MapFrom(x => x.Locality.Name));
            CreateMap<Batik, CatalogEditViewModel>();
            CreateMap<BatikDto, CatalogDetailViewModel>();
            CreateMap<BatikDto, CatalogEditViewModel>();
            CreateMap<CatalogEditViewModel, Batik>();
        }
    }
}
