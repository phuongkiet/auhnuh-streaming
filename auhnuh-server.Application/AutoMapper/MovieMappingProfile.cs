using auhnuh_server.Domain;
using auhnuh_server.Domain.DTO.WebResponse.Movie;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.AutoMapper
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile() 
        {
            CreateMap<Movie, ListAllMovieDTO>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
                .ForMember(dest => dest.MovieCategories, opt => opt.MapFrom(src => src.MovieCategories != null
                            ? src.MovieCategories.Select(m => m.Categories != null ? m.Categories.Name : "Unknown").ToList()
                            : new List<string>()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();
        }
    }
}
