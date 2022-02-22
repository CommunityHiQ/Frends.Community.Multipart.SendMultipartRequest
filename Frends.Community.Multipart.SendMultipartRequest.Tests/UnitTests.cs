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

            var input = new SendInput
            {
                Url = @"https://httpbin.org/post",
                FilePaths = files,
                Headers = new InputHeader[]{}
            };

            var options = new SendOptions();
            var result = await MultipartTasks.SendMultipartRequest(input, options, new CancellationToken());

            Assert.IsTrue(result.RequestIsSuccessful);
        }
    }
}
