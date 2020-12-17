using System.Collections.Generic;
using System.IO;
using log4net.Appender;
using log4net.Core;
using Log4netOutputJson.Log;

namespace Log4netOutputJson.Tests.Log
{
    public class TestAppender : IAppender
    {
        public string Name { get; set; }
        public List<string> RecordedMessages { get; }
        public JsonLayout Layout { get; set; }

        public TestAppender()
        {
            RecordedMessages = new List<string>();
        }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            var writer = new StringWriter();
            
            if (Layout == null)
            {
                loggingEvent.WriteRenderedMessage(writer);
            }
            else
            {
                Layout.Format(writer, loggingEvent);
            }
            
            RecordedMessages.Add(writer.ToString());
        }

        public void Close()
        {
        }
    }
}