using auhnuh_server.Domain;
using auhnuh_server.Domain.Common;
using auhnuh_server.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace auhnuh_server.Infrastructure.Data.Seed
{
    public class Seed
    {
        public static async Task SeedUser(IApplicationDbContext _context, UserManager<User> userManager,
        RoleManager<Role> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            if (!await roleManager.RoleExistsAsync("Client"))
                await roleManager.CreateAsync(new Role { Name = "Client" });

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new Role { Name = "Admin" });

            var admin = new User
            {
                Name = "admin",
                Birthday = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                Email = "admin@gmail.com",
                PhoneNumber = "0123456789",
                EmailConfirmed = true,
                UpdatedAt = DateTime.UtcNow,
                UserName = "admin",
                Status = UserStatus.Active,
                RoleId = 2
            };

            var resultAdmin = await userManager.CreateAsync(admin, "Pa$$w0rd");
            if (resultAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
                _context.SaveChanges();
            }
        }

        public static async Task SeedCategoryFromJson(IApplicationDbContext _context)
        {
            if (await _context.CreateSet<Category>().AnyAsync()) { return; }
            var categoryJson = await File.ReadAllTextAsync("../auhnuh-server.Infrastructure/Data/Seed/CategorySeed.json");

            var categories = JsonSerializer.Deserialize<IEnumerable<Category>>(categoryJson);

            var seedCategories = new List<Category>();

            foreach(var c in categories)
            {
                seedCategories.Add(c);
            }

            await _context.CreateSet<Category>().AddRangeAsync(seedCategories);
            _context.SaveChanges();
        }

        public static async Task SeedMovieFromJson(IApplicationDbContext _context)
        {
            if (await _context.CreateSet<Movie>().AnyAsync()) { return; }

            var movieJson = await File.ReadAllTextAsync("../auhnuh-server.Infrastructure/Data/Seed/MovieSeed.json");

            var movies = JsonSerializer.Deserialize<IEnumerable<Movie>>(movieJson);

            var seedMovies = new List<Movie>();

            foreach (var m in movies)
            {
                seedMovies.Add(m);
            }

            await _context.CreateSet<Movie>().AddRangeAsync(seedMovies);
            _context.SaveChanges();
        }
    }
}
