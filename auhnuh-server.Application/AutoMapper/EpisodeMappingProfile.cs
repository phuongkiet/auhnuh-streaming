using auhnuh_server.Domain;
using auhnuh_server.Domain.DTO.WebResponse.Episode;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.AutoMapper
{
    public class EpisodeMappingProfile : Profile
    {
        public EpisodeMappingProfile() 
        {
            CreateMap<Episode, EpisodeDTO>().ReverseMap();
        }
    }
}
