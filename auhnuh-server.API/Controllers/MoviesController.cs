using auhnuh_server.Application.IService;
using auhnuh_server.Application.Service;
using auhnuh_server.Domain.Common.ResponseModel;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebRequest.Movie;
using auhnuh_server.Domain.DTO.WebResponse.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using auhnuh_server.Domain.DTO.WebResponse.Movie;

namespace auhnuh_server.API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _movieService.ListMovie();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("admin-movies")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminMovies(int pageSize, int pageNumber, string? term)
        {
            var response = await _movieService.ListMovieAdmin(pageSize, pageNumber, term);

            var result = new ApiResponseModel<PagedModel<ListAllMovieDTO>>();

            if (response == null)
            {
                result.Errors.Add("There is no movies!");
                return BadRequest(result);
            }
            else
            {
                result.Data = response;
                return Ok(result);
            }
        }

        [HttpGet("movies-by-category")]
        public async Task<IActionResult> GetMoviesByCategory(int pageSize, int pageNumber, int? categoryId)
        {
            var response = await _movieService.ListMovieByCategory(pageSize, pageNumber, categoryId);

            var result = new ApiResponseModel<PagedModel<ListAllMovieDTO>>();

            if (response == null)
            {
                result.Errors.Add("There is no movies!");
                return BadRequest(result);
            }
            else
            {
                result.Data = response;
                return Ok(result);
            }
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
