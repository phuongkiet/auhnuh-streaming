using auhnuh_server.Domain.DTO.WebResponse.Movie;

namespace auhnuh_server.Application.IRepository
{
    public interface IMovieRepository
    {
        Task<List<ListAllMovieDTO>> ListMovie(); 
    }
}
