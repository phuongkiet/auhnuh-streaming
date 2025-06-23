using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.Common.ResponseModel;
using auhnuh_server.Domain.DTO.WebRequest.Movie;
using auhnuh_server.Domain.DTO.WebResponse.Movie;

namespace auhnuh_server.Application.IService
{
    [AutoRegister]
    public interface IMovieService
    {
        Task<ApiResponseModel<List<ListAllMovieDTO>>> ListMovie();
        Task<PagedModel<ListAllMovieDTO>> ListMovieAdmin(int pageSize, int pageNumber, string? term);
        Task<PagedModel<ListAllMovieDTO>> ListMovieByCategory(int pageSize, int pageNumber, int? categoryId);
        Task<ApiResponseModel<MovieDetailDTO>> GetDetail(int id);
        Task<ApiResponseModel<string>> AddMovie(AddMovieDTO movie, CancellationToken cancellationToken);
    }
}
