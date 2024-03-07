using oneapp.Repos.DbConnection;
using oneapp.Services;
using oneapp.Repos;
using Microsoft.OpenApi.Models;
using oneapp.Utilities;

namespace oneapp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins("http://localhost:5173")  // Add your client origin
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            // Set up configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Retrieve the connection string
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Register the connection string
            services.AddSingleton(provider => connectionString);

            // Register your repository
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();

            // Register DbConnectionFactory
            services.AddSingleton<DbConnectionFactory>(provider =>
            {
                return new DbConnectionFactory(connectionString);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "COMMUNITY APP", Version = "v1" });

                c.OperationFilter<SwaggerFileUploadFilter>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseCustomExceptionHandler();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {

            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseStaticFiles();
            //app.UseCors("AllowAnyOrigin");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
        }
    }
}