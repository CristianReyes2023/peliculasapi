using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pelicula,PeliculaDto>().ReverseMap();
            CreateMap<Pelicula,PeliculaDtoPost>().ReverseMap();
        }
    }
}