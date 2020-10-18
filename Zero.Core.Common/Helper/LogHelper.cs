using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Common.Helper
{
    public class LogHelper:ILogHelper
    {
        readonly ILogger<LogHelper> _logger;
        public LogHelper(ILogger<LogHelper> logger)
        {
            _logger = logger;
        }

        public void Info(Exception exception, string message, params object[] args)
        {
            _logger.LogInformation(exception, message, args);
        }
        public void Info(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }

    public interface ILogHelper
    {
        void Info(Exception exception, string message, params object[] args);

        void Info(string message, params object[] args);
    }
}
