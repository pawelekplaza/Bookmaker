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
using Bookmaker.Infrastructure.ServicesInterfaces;
using Microsoft.IdentityModel.Tokens;
using Bookmaker.Infrastructure.Settings;
using System.Text;

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

            services.AddScoped<ITeamRepository, DbTeamRepository>();
            services.AddScoped<ITeamService, TeamService>();

            services.AddScoped<IMatchRepository, DbMatchRepository>();
            services.AddScoped<IMatchService, MatchService>();

            services.AddScoped<IBetRepository, DbBetRepository>();
            services.AddScoped<IBetService, BetService>();

            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddSingleton<JwtSettings>();
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddSingleton<IEncrypter, Encrypter>();
            
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


            var jwtSettings = app.ApplicationServices.GetService<JwtSettings>();
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                }
            });

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