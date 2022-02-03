using api.Models;
using api.Models.Entities;
using api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string CorsServiceName = "CorsPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy(
                    CorsServiceName,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
            
            // Controllers
            services.AddControllers();
            
            // Database
            var connectionString = Configuration["SummaryContext:ConnectionString"];
            services.AddDbContext<SummaryContext>(opt =>
                opt.UseNpgsql(connectionString)
            );
            
            // Summary
            services.AddScoped<IAbstractRepositoryImpl<Summary>, AbstractRepositoryDatabaseImpl<Summary>>();
            services.AddScoped<IAbstractRepository<Summary>, AbstractRepository<Summary>>();
            services.AddScoped<ISummaryService, SummaryService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(CorsServiceName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}