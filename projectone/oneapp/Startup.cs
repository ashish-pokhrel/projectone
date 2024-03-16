using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using oneapp.Entities;
using oneapp.Repos;

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
            services.AddCustomServices(Configuration);
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins(Configuration.GetSection("AllowSpecificOrigin").Value) 
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            // Add Identity services
            services.AddIdentity<IdentityUser, IdentityRole>()
                   .AddEntityFrameworkStores<AppDbContext>()
                   .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("NormalUser", policy => policy.RequireRole("NormalUser"));
            });



            var passwordRequirements = Configuration.GetSection("PasswordRequirements");
            // Configure Identity options
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });


            //var faceBookAuth = Configuration.GetSection("FaceBookAuth");
            //services.AddAuthentication().AddFacebook(opt =>
            //{
            //    opt.AppId = faceBookAuth.GetSection("AppId").Value;
            //    opt.AppSecret = faceBookAuth.GetSection("AppSecret").Value;
            //});

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue; // Set the limit to the maximum value
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseCustomExceptionHandler();
            if (env.IsDevelopment())
            {
               
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
