//using Chinook.DataJson.Repositories;
//using Chinook.DataDapper.Repositories;
using Chinook.DataEFCore.Repositories;
//using Chinook.DataEFCoreCmpldQry.Repositories;
using Chinook.Domain.Repositories;
using Chinook.Domain.Supervisor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Chinook.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(EfRepository<>));
            services.AddScoped<AlbumRepository>();
            services.AddScoped<ArtistRepository>();
            services.AddScoped<CustomerRepository>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<GenreRepository>();
            services.AddScoped<InvoiceRepository>();
            services.AddScoped<InvoiceLineRepository>();
            services.AddScoped<MediaTypeRepository>();
            services.AddScoped<PlaylistRepository>();
            services.AddScoped<TrackRepository>();
        }

        public static void ConfigureSupervisor(this IServiceCollection services)
        {
            services.AddScoped<IChinookSupervisor, ChinookSupervisor>();
        }

        public static void AddLogging(this IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Information)
            );
        }
        
        public static void AddCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();            
            services.AddResponseCaching();
        }
        
        public static void AddCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }
    }
}