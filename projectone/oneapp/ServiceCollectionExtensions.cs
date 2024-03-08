using AutoMapper;
using Microsoft.EntityFrameworkCore;
using oneapp.Repos;
using oneapp.Repos.DbConnection;
using oneapp.Services;

namespace oneapp
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
        }
    }

}

