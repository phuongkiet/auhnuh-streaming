using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.Common.ResponseModel;
using auhnuh_server.Domain.DTO.WebRequest.Movie;
using auhnuh_server.Domain.DTO.WebResponse.Movie;

namespace auhnuh_server.Application.IRepository
{
    [AutoRegister]
    public interface IMovieRepository
    {
        Task<ApiResponseModel<List<ListAllMovieDTO>>> ListMovie();
        Task<PagedModel<ListAllMovieDTO>> ListMovieAdmin(int pageSize, int pageNumber, string? term);
        Task<ApiResponseModel<MovieDetailDTO>> GetDetail(int id);
        Task<ApiResponseModel<string>> AddMovie(AddMovieDTO movie, CancellationToken cancellationToken);
    }
}
