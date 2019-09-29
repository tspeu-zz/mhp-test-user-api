﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using user_parking_api.Persistence;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace user_parking_api
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Test User Parking API", Version = "v1" });
            });
            //services.AddDbContext<DataContext>(x => x.UseSqlite.(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DataContext>(x => x.UseSqlite("Data Source=userParking.db"));
            //services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("UserParking"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors();
            /*services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder => builder.AllowAnyOrigin());
                //WithOrigins("http://localhost:8100/"));
            });
            */
            /*
services.Configure<MvcOptions>(options =>
{
options.Filters.Add(new CorsAuthorizationFilterFactory("AllowMyOrigin"));
});*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test User Parking API");
            });
            //app.UseCors("AllowMyOrigin");
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowCredentials().AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
