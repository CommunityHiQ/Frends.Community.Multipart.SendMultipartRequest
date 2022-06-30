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
            var input = new SendInput
            {
                Url = @"https://httpbin.org/post",
                FilePaths = new SendFile[] { new SendFile { Name = "test.txt", Fullpath = testFile } },
                Headers = new InputHeader[] { },
                TextData = new TextData[] { },
            };

            var options = new SendOptions()
            {
                Authentication = AuthenticationMethod.None,
                BearerToken = null,
            };

            var result = await MultipartTasks.SendMultipartRequest(input, options, new CancellationToken());
            Assert.IsTrue(result.RequestIsSuccessful);
        }
    }
}
