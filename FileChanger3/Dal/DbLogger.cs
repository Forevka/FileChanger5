using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileChanger3.Dal
{
    public class DbLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        public void Dispose() { }

        private class MyLogger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId,
                TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                var jsonLine = JsonConvert.SerializeObject(new
                {
                    logLevel,
                    eventId,
                    parameters = (state as IEnumerable<KeyValuePair<string, object>>)?.ToDictionary(i => i.Key, i => i.Value),
                    message = formatter(state, exception),
                    exception = exception?.GetType().Name
                }, Formatting.Indented);

                File.AppendAllText("db.log", "\r\n" + jsonLine + "\r\n");
            }
        }
    }
}
