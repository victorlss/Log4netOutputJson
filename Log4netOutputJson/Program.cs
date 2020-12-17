using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using log4net;

namespace Log4netOutputJson
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public static void Main(string[] args)
        {
            var logObject = new
            {
                message = "new log message",
                field = "some data"
            };
            
            Logger.Info(logObject);
        }
    }
}