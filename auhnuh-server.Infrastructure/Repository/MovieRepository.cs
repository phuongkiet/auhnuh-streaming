using auhnuh_server.Application.IRepository;
using auhnuh_server.Domain;
using auhnuh_server.Domain.DTO.WebResponse.Movie;
using auhnuh_server.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace auhnuh_server.Infrastructure.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IApplicationDbContext _context;

        public MovieRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ListAllMovieDTO>> ListMovie()
        {
            return await _context.CreateSet<Movie>()
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
                    MovieCategories = movie.MovieCategories.Select(m => m.Categories.Name).ToList()
                })
                .ToListAsync();
        }
    }
}
