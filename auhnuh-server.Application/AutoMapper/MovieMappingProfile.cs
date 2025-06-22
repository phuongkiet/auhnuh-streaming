using auhnuh_server.Domain;
using auhnuh_server.Domain.DTO.WebRequest.Movie;
using auhnuh_server.Domain.DTO.WebResponse.Movie;
using AutoMapper;

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
                .ReverseMap();

            CreateMap<Movie, MovieDetailDTO>()
            .ForMember(dest => dest.MovieCategories, opt => opt.MapFrom(src => src.MovieCategories.Select(mc => mc.Categories.Name).ToList()));

            CreateMap<Movie, MovieAfterAddResponseDTO>().ReverseMap();

            CreateMap<AddMovieDTO, Movie>().ReverseMap();
            CreateMap<UpdateMovieDTO, Movie>().ReverseMap();
        }
    }
}
