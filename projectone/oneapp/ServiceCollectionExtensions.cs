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


            // FOR PRODUCTION ONLY
            //var azureConnections = configuration.GetSection("AzureBlobStorage");
            //services.AddScoped<IFileService>(provider =>new FileService(azureConnections.GetSection("AzureBlobStorageConnection").Value,
            //    azureConnections.GetSection("ContainerName").Value,
            //    azureConnections.GetSection("SharedAzureBlobKey").Value));


            services.AddScoped<IFileService, FileServiceStub>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentRepo, CommentRepo>();

            services.AddScoped<IFeedService, FeedService>();
            services.AddScoped<IFeedRepo, FeedRepo>();

            services.AddScoped<IImageHubRepo, ImageHubRepo>();

        }
    }

}

