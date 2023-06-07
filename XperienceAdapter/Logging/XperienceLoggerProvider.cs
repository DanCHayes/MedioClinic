using CMS.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace XperienceAdapter.Logging
{
    internal class XperienceLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, XperienceLogger> _loggers = new ConcurrentDictionary<string, XperienceLogger>();

        private readonly IEventLogService _eventLogService;

        public XperienceLoggerProvider(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new XperienceLogger(name, _eventLogService));

        public void Dispose() => _loggers.Clear();
    }
}
