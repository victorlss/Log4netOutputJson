using System;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Log4netOutputJson.Log;
using Newtonsoft.Json;
using Xunit;

namespace Log4netOutputJson.Tests.Log
{
    public class JsonLayoutTest
    {
        private readonly ILog _logger;
        private readonly TestAppender _appender;

        public JsonLayoutTest()
        {
            var repositoryId = Guid.NewGuid().ToString();

            _appender = new TestAppender
            {
                Layout = new JsonLayout()
            };

            if (LogManager.CreateRepository(repositoryId) is Hierarchy hierarchy)
            {
                hierarchy.Root.AddAppender(_appender);
                hierarchy.Root.Level = Level.All;
                hierarchy.Configured = true;
            }

            _logger = LogManager.GetLogger(repositoryId, nameof(JsonLayoutTest));
        }

        [Fact]
        public void Should_capture_json_message_test()
        {
            var logObject = new
            {
                message = "new log message",
                field = "some data"
            };

            _logger.Info(logObject);

            dynamic obj = JsonConvert.DeserializeObject(_appender.RecordedMessages[0]);
            string message = obj?.messageObject.message;
            string field = obj?.messageObject.field;

            Assert.Equal("new log message", message);
            Assert.Equal("some data", field);
        }

        [Fact]
        public void Should_capture_json_error_message_test()
        {
            var logObject = new
            {
                message = "new log error",
                field = "some data"
            };
            
            _logger.Error(logObject, new Exception("DummyError"));

            dynamic obj = JsonConvert.DeserializeObject(_appender.RecordedMessages[0]);
            string message = obj?.messageObject.message;
            string exceptionType = obj?.exceptionObject.ClassName;
            string exceptionMessage = obj?.exceptionObject.Message;

            Assert.Equal("new log error", message);
            Assert.Equal("System.Exception", exceptionType);
            Assert.Equal("DummyError", exceptionMessage);
        }
    }
}