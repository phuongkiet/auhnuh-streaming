using auhnuh_server.Application.IRepository;
using auhnuh_server.Application.IService;
using auhnuh_server.Domain.DTO.WebResponse.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auhnuh_server.Application.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<ListAllMovieDTO>> ListMovie()
        {
            return await _movieRepository.ListMovie();
        }
    }
}
