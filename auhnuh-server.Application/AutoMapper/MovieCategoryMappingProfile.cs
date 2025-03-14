using auhnuh_server.Domain;
using auhnuh_server.Domain.DTO.WebRequest.MovieCategory;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.AutoMapper
{
    public class MovieCategoryMappingProfile : Profile
    {
        public MovieCategoryMappingProfile() 
        {
            CreateMap<MovieCategory, MovieCategoryDTO>().ReverseMap();
        }
    }
}
