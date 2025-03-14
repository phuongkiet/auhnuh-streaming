using auhnuh_server.Domain;
using auhnuh_server.Domain.DTO.WebRequest.Season;
using auhnuh_server.Domain.DTO.WebResponse.Season;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.AutoMapper
{
    public class SeasonMappingProfile : Profile
    {
        public SeasonMappingProfile() 
        {
            CreateMap<Season, SeasonDTO>().ReverseMap();
            CreateMap<AddSeasonDTO, Season>().ReverseMap();
        }
    }
}
