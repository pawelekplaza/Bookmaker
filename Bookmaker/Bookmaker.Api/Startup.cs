using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Bookmaker.Core.Repository;
using Bookmaker.Infrastructure.Repositories;
using Bookmaker.Infrastructure.Services;
using Bookmaker.Infrastructure.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using NLog.Extensions.Logging;
using Bookmaker.Infrastructure.Helpers;

namespace Bookmaker.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            ConnectionHelper.SetConnectionString(Configuration["connectionStrings:Bookmaker"]);
        }

        public static IConfigurationRoot Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddScoped<IUserRepository, DbUserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICountryRepository, DbCountryRepository>();
            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IScoreRepository, DbScoreRepository>();
            services.AddScoped<IScoreService, ScoreService>();

            services.AddScoped<ICityRepository, DbCityRepository>();            
            services.AddScoped<ICityService, CityService>();

            services.AddScoped<IStadiumRepository, DbStadiumRepository>();
            services.AddScoped<IStadiumService, StadiumService>();

            services.AddScoped<IResultRepository, DbResultRepository>();
            services.AddScoped<IResultService, ResultService>();

            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddMvc();
            services.AddCors();
            services.AddTransient<IMailService, LocalMailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials();
            });

            app.UseMvc();
        }
    }
}