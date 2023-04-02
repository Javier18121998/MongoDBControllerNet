using System;
using PersonalHealthManager.Infrastructure.Data;
using MongoDB.Driver;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalHealthManager
{
    public class Startup
    {
        public IConfiguration Configuration{ get;}
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void configureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddSingleton<IMongoClient>(s => new MongoClient(Configuration.GetConnectionString("ConnectionString")));
            services.AddScoped(s => s.GetService<IMongoClient>().StartSession());
            services.AddScoped(s => s.GetService<IMongoClient>().StartSession());
            services.AddScoped<AppDbContext>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => {
                options.WithOrigins("http://localhost:3000");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
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