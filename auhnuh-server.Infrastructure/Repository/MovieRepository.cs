using auhnuh_server.Application.IRepository;
using auhnuh_server.Common.Attibutes;
using auhnuh_server.Domain;
using auhnuh_server.Domain.Common;
using auhnuh_server.Domain.Common.ResponseModel;
using auhnuh_server.Domain.DTO.WebRequest.Movie;
using auhnuh_server.Domain.DTO.WebResponse.Movie;
using auhnuh_server.Domain.DTO.WebResponse.User;
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
                        Status = movie.Status,
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

        public async Task<PagedModel<ListAllMovieDTO>> ListMovieAdmin(int pageSize, int pageNumber, string? term)
        {
            if (pageSize == 0 && pageNumber == 0)
            {
                pageSize = int.MaxValue;
                pageNumber = 1;
            }
            var query = _context.CreateSet<Movie>()
                .AsEnumerable();

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(u => u.Title.Contains(term));
            }

            var totalItems = query.Count();

            var response = query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            var result = _mapper.Map<List<ListAllMovieDTO>>(response);

            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedModel = new PagedModel<ListAllMovieDTO>
            {
                PageNo = pageNumber,
                TotalItems = totalItems,
                TotalPage = totalPages,
                Results = result,
            };

            return pagedModel;
        }

        public async Task<PagedModel<ListAllMovieDTO>> ListMovieByCategory(int pageSize, int pageNumber, int? categoryId)
        {
            if (pageSize == 0 && pageNumber == 0)
            {
                pageSize = 20;
                pageNumber = 1;
            }

            IQueryable<Movie> query = _context.CreateSet<Movie>();

            if (categoryId != null)
            {
                query = query.Where(m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId));
            }
            else
            {
                query = query.AsQueryable();
            }


            var totalItems = await query.CountAsync();

            var response = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<ListAllMovieDTO>>(response);

            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedModel = new PagedModel<ListAllMovieDTO>
            {
                PageNo = pageNumber,
                TotalItems = totalItems,
                TotalPage = totalPages,
                Results = result,
            };

            return pagedModel;
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

        public async Task<ApiResponseModel<string>> AddMovie(AddMovieDTO movie, CancellationToken cancellationToken)
        {
            var response = new ApiResponseModel<string>();

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

                response.Data = "Movie added successfully.";

                return response;
            }

            response.Errors.Add("The movie is exist!");
            return response;
        }
    }
}
