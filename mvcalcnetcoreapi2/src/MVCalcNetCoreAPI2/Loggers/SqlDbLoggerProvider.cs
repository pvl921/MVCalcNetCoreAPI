using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCalcNetCoreAPI2.Interfaces;

namespace MVCalcNetCoreAPI2.Loggers
{
    public class SqlDbLoggerProvider : ILoggerProvider
    {
        private ILogDbAccess _logDb;
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly string _cat;

        public SqlDbLoggerProvider(string categoryName, LogLevel logLevel, ILogDbAccess logDb)
        {
            _filter = (cat, level) => (cat.Contains(categoryName)) && (level >= logLevel);
            _logDb = logDb;
            _cat = categoryName;
        }

        public SqlDbLoggerProvider(ILogDbAccess logDb)
        {
            _logDb = logDb;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new SqlDbLogger(categoryName, _filter, _logDb);
        }

        public void Dispose()
        {
        }
    }
}
