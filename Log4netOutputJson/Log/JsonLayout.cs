using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;

namespace Log4netOutputJson.Log
{
    public class JsonLayout : LayoutSkeleton
    {
        [ExcludeFromCodeCoverage]
        public override void ActivateOptions()
        {
        }

        public override void Format(TextWriter writer, LoggingEvent e)
        {
            var log = new
            {
                messageObject = e.MessageObject,
                requestId = Guid.NewGuid().ToString(),
                pid = Process.GetCurrentProcess().Id,
                timestamp = e.TimeStamp.ToUniversalTime().ToString("O"),
                level = e.Level.DisplayName,
                logger = e.LoggerName,
                location = e.LocationInformation.ClassName,
                thread = e.ThreadName,
                exceptionObject = e.ExceptionObject,
                exceptionObjectString = e.ExceptionObject == null ? null : e.GetExceptionString()
            };
            
            writer.WriteLine(JsonConvert.SerializeObject(log));
        }
    }
}