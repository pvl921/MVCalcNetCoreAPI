using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MVCalcNetCoreAPI2.Controllers;

namespace MVCalcNetCoreAPI2.Filters
{
    public class MVCalcExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public MVCalcExceptionFilter(ILogger<EvaluationController> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            int status = StatusCodes.Status500InternalServerError;
            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ArgumentNullException)) { status = StatusCodes.Status409Conflict; }
            else if (exceptionType == typeof(ArgumentOutOfRangeException)) { status = StatusCodes.Status422UnprocessableEntity; }
            else if (exceptionType == typeof(OverflowException)) { status = StatusCodes.Status422UnprocessableEntity; }
            else if (exceptionType == typeof(ArgumentException)) { status = StatusCodes.Status409Conflict; }
            else if (exceptionType == typeof(FormatException)) { status = StatusCodes.Status409Conflict; }
            else if (exceptionType == typeof(IndexOutOfRangeException)) { status = StatusCodes.Status422UnprocessableEntity; }

            context.Result = new ContentResult
            {
                Content = $"Error: {context.Exception.Message}",
                ContentType = "text/plain",
                StatusCode = status
            };

            _logger.LogError(context.Exception.Message);

            base.OnException(context);
        }


    }
}
