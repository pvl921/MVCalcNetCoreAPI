using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCalcNetCoreAPI2.Interfaces;

namespace MVCalcNetCoreAPI2.Loggers
{
    /// <summary>
    /// Класс для записи лога в базу данных. 
    /// </summary>
    public class SqlDbLogger : ILogger 
    {
        private readonly ILogDbAccess _logDb;
        private readonly string _categoryName;
        private readonly Func<string, LogLevel, bool> _filter;

        public SqlDbLogger(string categoryName, Func<string, LogLevel, bool> filter, ILogDbAccess logDb)
        {
            _categoryName = categoryName;
            _filter = filter;
            _logDb = logDb;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            if (exception != null)
            {
                message += "\n" + exception.ToString();
            }
            _logDb.Add(message);
        }

    }
}
