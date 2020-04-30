using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BatiksProject.Dto;
using BatiksProject.Models;

namespace BatiksProject.Infrastructure
{
    public class BatikMapperProfile : Profile
    {
        public BatikMapperProfile()
        {
            CreateMap<Batik, BatikDto>()
                .ForMember(dest => dest.ImageUrl, expr => expr.MapFrom(x => "http://127.0.0.1/" + x.MinioObjectName))
                .ForMember(dest => dest.Locality, expr => expr.MapFrom(x => x.Locality.Name));
        }
    }
}
