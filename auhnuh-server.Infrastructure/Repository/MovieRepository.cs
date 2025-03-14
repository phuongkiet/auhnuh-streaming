using auhnuh_server.Application.IRepository;
using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.DTO.WebRequest.Movie;
using auhnuh_server.Domain.DTO.WebResponse.Movie;
using auhnuh_server.Infrastructure.Data.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace auhnuh_server.Infrastructure.Repository
{
    [AutoRegister]
    public class MovieRepository : IMovieRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MovieRepository(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponseModel<List<ListAllMovieDTO>>> ListMovie()
        {
            var response = new ApiResponseModel<List<ListAllMovieDTO>>();

            try
            {
                var movies = await _context.CreateSet<Movie>()
                    .Include(mc => mc.MovieCategories)
                    .ThenInclude(mc => mc.Categories)
                    .Select(movie => new ListAllMovieDTO
                    {
                        MovieId = movie.MovieId,
                        Title = movie.Title,
                        Description = movie.Description,
                        Publisher = movie.Publisher,
                        ReleaseDate = movie.ReleaseDate,
                        CreatedAt = movie.CreatedAt,
                        UpdatedAt = movie.UpdatedAt,
                        Status = movie.Status.ToString(),
                        TotalSeason = movie.TotalSeason,
                        Thumbnail = movie.Thumbnail,
                        MovieCategories = movie.MovieCategories.Select(m => m.Categories.Name).ToList()
                    })
                    .ToListAsync();

                response.Data = movies;
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while retrieving movies.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponseModel<MovieDetailDTO>> GetDetail(int id)
        {
            var response = new ApiResponseModel<MovieDetailDTO>();

            try
            {
                var movie = await _context.CreateSet<Movie>()
                    .Where(m => m.MovieId == id)
                    .Include(m => m.MovieCategories).ThenInclude(mc => mc.Categories)
                    .Include(m => m.Seasons).ThenInclude(s => s.Episodes)
                    .FirstOrDefaultAsync();

                if (movie == null)
                {
                    response.Errors.Add("Movie not found.");
                }
                else
                {
                    response.Data = _mapper.Map<MovieDetailDTO>(movie);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while retrieving the movie details.");
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ApiResponseModel<MovieAfterAddResponseDTO>> AddMovie(AddMovieDTO movie, CancellationToken cancellationToken)
        {
            var response = new ApiResponseModel<MovieAfterAddResponseDTO>();

            var existingMovie = await _context.CreateSet<Movie>()
                                              .FirstOrDefaultAsync(m => m.Title == movie.Title && m.ReleaseDate == movie.ReleaseDate, cancellationToken);

            if (existingMovie == null)
            {
                var result = new Movie
                {
                    Title = movie.Title,
                    Publisher = movie.Publisher,
                    ReleaseDate = movie.ReleaseDate,
                    TotalSeason = movie.TotalSeason,
                    Description = movie.Description,
                    Status = movie.Status,
                    Thumbnail = movie.Thumbnail,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                await _context.CreateSet<Movie>().AddAsync(result, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                if (movie.MovieCategories != null && movie.MovieCategories.Any())
                {
                    var movieCategory = movie.MovieCategories.Select(mc => new MovieCategory
                    {
                        MovieId = result.MovieId,
                        CategoryId = mc.CategoryId,
                    }).ToList();

                    await _context.CreateSet<MovieCategory>().AddRangeAsync(movieCategory, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                response.Data = new MovieAfterAddResponseDTO
                {
                    Title = movie.Title,
                    Publisher = movie.Publisher,
                    ReleaseDate = movie.ReleaseDate,
                    TotalSeason = movie.TotalSeason,
                    Description = movie.Description,
                    Status = movie.Status.ToString(),
                    Thumbnail = movie.Thumbnail,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                return response;
            }

            response.Errors.Add("The movie is exist!");
            return response;
        } 
    }
}
