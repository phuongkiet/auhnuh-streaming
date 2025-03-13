using auhnuh_server.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace auhnuh_server.API.Controllers
{
    [Route("api/[controller]")]
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
            return Ok(await _movieService.ListMovie());
        }
    }
}
