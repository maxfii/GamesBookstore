using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesBookstore.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.IO;

namespace GamesBookstore
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
            services.AddDbContext<GamesBookstoreContext>
                (opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(ILogger<GamesBookstoreItem> logger, IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp => errorApp.Run (async context =>
            {

                var exception = context.Features.Get<IExceptionHandlerFeature>().Error;

                logger.LogError("ERROR ({err}): {msg}", nameof(exception), exception.Message);

                await context.Response.WriteAsync(exception switch
                {
                    FileNotFoundException e => "ERROR: Didn't find some file",
                    Exception e => "ERROR: I don't know what it is, but it's wrong!"
                    //Still don't know _what_ exceptions should I care about
                });
            }));

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
