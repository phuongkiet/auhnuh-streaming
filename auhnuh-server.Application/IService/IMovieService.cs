using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebRequest.Movie;
using auhnuh_server.Domain.DTO.WebResponse.Movie;

namespace auhnuh_server.Application.IService
{
    [AutoRegister]
    public interface IMovieService
    {
        Task<ApiResponseModel<List<ListAllMovieDTO>>> ListMovie();
        Task<ApiResponseModel<MovieDetailDTO>> GetDetail(int id);
        Task<ApiResponseModel<MovieAfterAddResponseDTO>> AddMovie(AddMovieDTO movie, CancellationToken cancellationToken);
    }
}
