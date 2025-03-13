using auhnuh_server.Domain;
using auhnuh_server.Infrastructure.Data;
using auhnuh_server.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace auhnuh_server.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
                // options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<MovieDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IApplicationBuilder EnsureMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            dbContext.Migrate();

            return app;
        }
    }
}
