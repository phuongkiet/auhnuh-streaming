using auhnuh_server.Application.IRepository;
using auhnuh_server.Application.IService;
using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebRequest.Movie;
using auhnuh_server.Domain.DTO.WebResponse.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.Service
{
    [AutoRegister]
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<ApiResponseModel<List<ListAllMovieDTO>>> ListMovie()
        {
            return await _movieRepository.ListMovie();
        }

        public async Task<ApiResponseModel<MovieDetailDTO>> GetDetail(int id)
        {
            return await _movieRepository.GetDetail(id);
        }

        public async Task<ApiResponseModel<MovieAfterAddResponseDTO>> AddMovie(AddMovieDTO movie, CancellationToken cancellationToken)
        {
            return await _movieRepository.AddMovie(movie, cancellationToken);
        }
    }
}
