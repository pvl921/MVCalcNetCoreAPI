using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MVCalcNetCoreAPI2.Interfaces;
using MVCalcNetCoreAPI2.Loggers;
using MVCalcNetCoreAPI2.Filters;

namespace MVCalcNetCoreAPI2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(options =>
           {
               options.Filters.Add(typeof(MVCalcExceptionFilter));
           })
            .AddJsonFormatters();
            //регистрируем класс для работы с БД для последующих DI
            services.AddSingleton<ILogDbAccess, SqlDbService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory
                .AddConsole()
                .AddDebug()
                // добавляем свой провайдер для записи сообщений в БД
                .AddProvider(new SqlDbLoggerProvider("MVCalcNetCoreAPI2", LogLevel.Information, new SqlDbService()));

            app.UseMvc();
        }
    }
}
