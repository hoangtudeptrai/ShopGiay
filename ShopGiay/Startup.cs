using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShopGiay.Context;
using ShopGiay.Helps;
using ShopGiay.Interfaces;
using ShopGiay.Repositorys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopGiay
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
            services.AddSingleton<DapperContext>();
            services.AddScoped<IDanhMucRepository, DanhMucRepository>();
            services.AddScoped<ISanPhamRepository, SanPhamRepositorycs>();
            services.AddCors();
            services.AddSwaggerGen();
            services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.WriteIndented = true;
                   options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                   options.JsonSerializerOptions.PropertyNamingPolicy = null;
                   options.JsonSerializerOptions.Converters.Add(new CustomJsonConverterForType());
               });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopGiay", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopGiay v1"));
            }

            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());


            //app.UseStaticFiles(
            //   new StaticFileOptions
            //   {
            //       FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files/Upload"))
            //   });

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
