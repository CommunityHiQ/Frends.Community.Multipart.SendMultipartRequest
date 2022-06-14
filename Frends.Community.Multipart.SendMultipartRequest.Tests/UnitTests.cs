using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Frends.Community.Multipart.SendMultipartRequest.Tests
{
    [TestFixture]
    class TestClass
    {
        private readonly static string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../testFiles/test.txt");

        [Test]
        public async Task SendMultipartRequestTest()
        {
            var files = new SendFile[]
            {
                new SendFile{ Name = "test.txt", Fullpath = testFile },
            };

            var bodies = new InputBody[]
            {
            };

            var headers = new InputHeader[]
            {
            };

            var textData = new TextData[]
            {
                new TextData
                {
                    Key = "attachments",
                    Value = @"[{""title"": ""Random"", ""image_url"": ""https://upload.wikimedia.org/wikipedia/en/0/0b/Postman-Pat.jpg""}]"
                },
                new TextData
                {
                    Key = "channel",
                    Value = "G01QH4ES8SV"
                }
            };

            var input = new SendInput
            {
                Url = @"https://slack.com/api/chat.postMessage", // https://slack.com/api/chat.postMessage
                FilePaths = files,
                Headers = headers,
                Bodys = bodies,
                TextData = textData
            };

            var options = new SendOptions()
            {
                Authentication = AuthenticationMethod.OAuth2,
                BearerToken = "xoxb-1816171944434-1826655541764-UcWHT5s4O9BE50SXNSai4oXs",
            };

            var result = await MultipartTasks.SendMultipartRequest(input, options, new CancellationToken());

            Assert.IsTrue(result.RequestIsSuccessful);
        }
    }
}
