using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAccess.Repository.Contract;
using DataAccess.Repository;
using BusinessLogic.Contract;
using BusinessLogic;

namespace FruitsAPI
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

            services.AddControllers();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Fruits API",
                    Version = "v1",
                    Description = "Fruits simple api for Number8 challenge.",
                    Contact = new OpenApiContact
                    {
                        Name = "Felipe Sartori",
                        Email = "felipe@sartori.app",
                        Url = null
                    },
                });
            });

            services.AddScoped<IFruitRepository, FruitRepository>();
            services.AddScoped<IFruitTypeRepository, FruitTypeRepository>();

            services.AddScoped<IBLFruit, BLFruit>();
            services.AddScoped<IBLFruitType, BLFruitType>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fruits API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
