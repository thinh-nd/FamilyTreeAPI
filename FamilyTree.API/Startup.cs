using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using FamilyTree.API.Repositories;
using Newtonsoft.Json;
using FamilyTree.API.Filters;
using FamilyTree.API.Services;

namespace FamilyTree.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(option => option.Filters.Add<ExceptionFilter>())
                .AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen();

            var dataSourcePath = $"{Directory.GetCurrentDirectory()}\\DatabaseFiles\\FamilyTree";
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlite($"Data Source={dataSourcePath}"));

            services.AddScoped<IFamilyRepository, FamilyRepository>();
            services.AddScoped<IFamilyTreeService, FamilyTreeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Family Tree API"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
