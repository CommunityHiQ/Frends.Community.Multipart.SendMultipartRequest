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
            };

            var input = new SendInput
            {
                Url = "",
                FilePaths = files,
                Headers = headers,
                Bodys = bodies,
                TextData = textData
            };

            var options = new SendOptions()
            {
                Authentication = AuthenticationMethod.OAuth2,
                BearerToken = "",
            };

            var result = await MultipartTasks.SendMultipartRequest(input, options, new CancellationToken());

            Assert.IsTrue(result.RequestIsSuccessful);
        }
    }
}
