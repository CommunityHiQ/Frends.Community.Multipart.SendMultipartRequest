using NUnit.Framework;
using System;
using System.IO;
using System.Net;
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
                FilePaths = new SendFile[] { new SendFile { FileParameterKey = FileParameterKey.file, Fullpath = testFile } },
                Headers = new InputHeader[] { },
                TextData = new TextData[] { new TextData { Key = "randomKey", Value = "SomeValue" } },
            };

            var options = new SendOptions()
            {
                Authentication = AuthenticationMethod.None,
                BearerToken = null,
            };

            var result = await MultipartTasks.SendMultipartRequest(input, options, new CancellationToken());
            Assert.IsTrue(result.RequestIsSuccessful);
        }

        [Test]
        public async Task SendMultipartRequestWithFileAsContentTest()
        {
            var input = new SendInput
            {
                Url = @"https://httpbin.org/post",
                FilePaths = new SendFile[] { new SendFile { FileParameterKey = FileParameterKey.content, Fullpath = testFile } },
                Headers = new InputHeader[] { },
                TextData = new TextData[] { new TextData { Key = "randomKey", Value = "SomeValue" } },
            };

            var options = new SendOptions()
            {
                Authentication = AuthenticationMethod.None,
                BearerToken = null,
            };

            var result = await MultipartTasks.SendMultipartRequest(input, options, new CancellationToken());
            Assert.IsTrue(result.RequestIsSuccessful);
        }

        [Test]
        public void ThrowsSendMultipartRequestTestWithInvalidBearerToken()
        {
            var input = new SendInput
            {
                Url = @"https://httpbin.org/bearer",
                FilePaths = new SendFile[] { new SendFile { FileParameterKey = FileParameterKey.file, Fullpath = testFile } },
                Headers = new InputHeader[] { new InputHeader { Name = "Authorization", Value = "Bearer" } },
                TextData = new TextData[] { new TextData { Key = "randomKey", Value = "SomeValue" } },
            };

            var options = new SendOptions()
            {
                Authentication = AuthenticationMethod.OAuth2,
                BearerToken = "cvijhbihjb rewuhb cvieh",
                ThrowExceptionOnErrorResponse = true
            };

            var ex = Assert.ThrowsAsync<WebException>(async () => await MultipartTasks.SendMultipartRequest(input, options, new CancellationToken()));
            Assert.IsTrue(ex.Message.StartsWith("Status Code: 405"));
        }

        [Test]
        public async Task SendMultipartRequestTestWithInvalidBearerToken()
        {
            var input = new SendInput
            {
                Url = @"https://httpbin.org/bearer",
                FilePaths = new SendFile[] { new SendFile { FileParameterKey = FileParameterKey.file, Fullpath = testFile } },
                Headers = new InputHeader[] { new InputHeader { Name = "Authorization", Value = "Bearer" } },
                TextData = new TextData[] { new TextData { Key = "randomKey", Value = "SomeValue" } },
            };

            var options = new SendOptions()
            {
                Authentication = AuthenticationMethod.OAuth2,
                BearerToken = "cvijhbihjb rewuhb cvieh",
                ThrowExceptionOnErrorResponse = false
            };

            var result = await MultipartTasks.SendMultipartRequest(input, options, new CancellationToken());
            Assert.AreEqual("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 3.2 Final//EN\">\n<title>405 Method Not Allowed</title>\n<h1>Method Not Allowed</h1>\n<p>The method is not allowed for the requested URL.</p>\n", result.ErrorMessage);
        }
    }
}
