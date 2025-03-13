using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebResponse.Movie;

namespace auhnuh_server.Application.IRepository
{
    [AutoRegister]
    public interface IMovieRepository
    {
        Task<ApiResponseModel<List<ListAllMovieDTO>>> ListMovie();
    }
}
