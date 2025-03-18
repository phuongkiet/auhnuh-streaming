using auhnuh_server.Application.IService;
using auhnuh_server.Domain.DTO.WebRequest.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auhnuh_server.API.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("movies")]
        public async Task<IActionResult> Get()
        {
            var response = await _movieService.ListMovie();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("movie-detail")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var response = await _movieService.GetDetail(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPost("add-movie")]
        public async Task<IActionResult> AddMovie([FromBody] AddMovieDTO movie, CancellationToken cancellationToken)
        {
            var response = await _movieService.AddMovie(movie, cancellationToken);
            return Ok(response);
        }
    }
}
