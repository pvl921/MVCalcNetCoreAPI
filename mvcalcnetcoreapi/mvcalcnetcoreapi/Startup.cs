using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using MVCalcNetCoreAPI.Interfaces;
using MVCalcNetCoreAPI.Services;
using MVCalcNetCoreAPI.Models;
using MVCalcNetCoreAPI.Controllers;


namespace MVCalcNetCoreAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddScoped<ILogDbAccess, SqlDbService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            
            // разбираем HTTP запрос вручную, вместо методов MVC - считаем и записываем в БД
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet("{operation:regex(^(sum|subtract|multiply|divide|power)$)}/{op1?}/{op2?}", 
                context => {
                    var op = context.GetRouteValue("operation").ToString().ToLowerInvariant();
                    string op1 = (context.GetRouteValue("op1") != null) ? context.GetRouteValue("op1").ToString() : "";
                    string op2 = (context.GetRouteValue("op2") != null) ? context.GetRouteValue("op2").ToString() : "";

                    // инициализация объекта модели  
                    var model = new DataModel();
                    // инициализация БД для HTTP-запроса через инъекцию в конструктор
                    var logCtrl = new SqlDbService();

                    bool isDbAccessSuccess = false;
                    switch (op)
                    {
                        case "sum":
                            model = EvaluationController.Sum(op1, op2);
                            break;
                        case "subtract":
                            model = EvaluationController.Subtract(op1, op2);
                            break;
                        case "multiply":
                            model = EvaluationController.Multiply(op1, op2);
                            break;
                        case "divide":
                            model = EvaluationController.Divide(op1, op2);
                            break;
                        case "power":
                            model = EvaluationController.Power(op1, op2);
                            break;
                        default:
                            model = EvaluationController.Undefined(op);
                            break;
                    };
                    isDbAccessSuccess = (logCtrl.Add(model, op1, op, op2) == 0) ? false : true;
                    if ((!model.IsResultOk)||(!isDbAccessSuccess)) context.Response.StatusCode=StatusCodes.Status400BadRequest;
                    return context.Response.WriteAsync($"{model.ToString()}, Запись в БД:{isDbAccessSuccess}"); 
                }
                );

            // разбираем HTTP запрос вручную, вместо методов MVC - считываем и удаляем из БД            
            routeBuilder.MapGet("{operation:regex(^(list|log|del)$)}/{id?}",
                context => {
                    var op = context.GetRouteValue("operation").ToString().ToLowerInvariant();
                    string id = (context.GetRouteValue("id") != null)? context.GetRouteValue("id").ToString() : "";
                    // инициализация БД для HTTP-запроса через инъекцию в конструктор
                    var logCtrl = new SqlDbService();
                    bool isDbAccessSuccess = false;
                    int log_id = 0;
                    List<LogModel> logList = new List<LogModel>();
                    string logToDisplay;
                    switch (op)
                    {
                        case Constants.Commands.LIST:
                            logList = logCtrl.List();
                            if (logList.Count > 0) isDbAccessSuccess = true;
                            break;
                        case Constants.Commands.LOG_ID:
                            if (int.TryParse(id, out log_id))
                            {
                                LogModel row = logCtrl.Get(log_id);
                                isDbAccessSuccess = (row.ID > 0) ? true : false;
                                logList.Add(row);
                            };
                            break;
                        case Constants.Commands.DEL:
                            if (int.TryParse(id, out log_id)) { isDbAccessSuccess = logCtrl.Delete(log_id) > 0 ? true : false; };
                            break;
                        default:
                            isDbAccessSuccess = false;
                            break;
                    };
                    
                    if (!isDbAccessSuccess)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        logToDisplay = Constants.Messages.MSG_DB_FAILURE;
                    }
                    else
                    {
                        logToDisplay = "{" + string.Join("}\n{", logList) + "}";
                    }
                    return context.Response.WriteAsync(logToDisplay); 
                }
                );

            var routes = routeBuilder.Build();
            app.UseRouter(routes);

        }
    }
}
