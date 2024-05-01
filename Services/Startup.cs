﻿using Business.Business;
using Business.Interface;
using Data.Context;
using Data.Interface;
using Quartz;

namespace Services
{
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(cfg => {
                cfg.EnableAnnotations();
            });
            services.AddDbContext<FutureSpaceContext>();

            services.AddHttpClient();

            services.AddTransient<ILaunchApiBusiness, LaunchApiBusiness>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();
        }
    }

    public interface IStartup
    {
        IConfiguration Configuration { get; }
        void Configure(WebApplication app, IWebHostEnvironment enviroment);
        void ConfigureServices(IServiceCollection services);
    }

    public static class StartupExtensions
    {
        public static WebApplicationBuilder UseStartup<T>(this WebApplicationBuilder webApplicationBuilder) where T : IStartup
        {
            var startup = Activator.CreateInstance(typeof(T), webApplicationBuilder.Configuration) as IStartup;
            if (startup == null)
            {
                throw new ArgumentException("Startup class is not available.");
            }

            startup.ConfigureServices(webApplicationBuilder.Services);

            var app = webApplicationBuilder.Build();

            startup.Configure(app, app.Environment);
            app.Run();

            return webApplicationBuilder;
        }
    }
}
