using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSeriesCore5.Data;
using WebApiSeriesCore5.Repository;
using WebApiSeriesCore5.Repository.Contract;
using WebApiSeriesCore5.Service;
using WebApiSeriesCore5.Service.Contract;
using Microsoft.AspNetCore.Http;
using WebApiSeriesCore5.Repository.Address;
using WebApiSeriesCore5.Repository.AddressType;
using WebApiSeriesCore5.Services.Contract;
using WebApiSeriesCore5.Services;

namespace WebApiSeriesCore5
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
            services.AddHealthChecks();

            services.AddDbContext<DataContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
            //Add Automapper
            services.AddAutoMapper(typeof(Startup));
            //inject Data Access Layer - Repository
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IAddressRespository, AddressRepository>();
            services.AddScoped<IAddressTypeRepository, AdressTypeRepository>();
            //inject Service layer
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAddressTypeService, AddressTypeService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiSeriesCore5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiSeriesCore5 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");

                endpoints.MapGet("/{**path}", async context =>
                {
                    await context.Response.WriteAsync(
                        "Navigate to /health to see the health status.");
                });
            });
        }
    }
}
