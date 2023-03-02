using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 1591
#pragma warning disable CS1573

namespace Frends.Community.Multipart.SendMultipartRequest
{
    public static class MultipartTasks
    {
        /// <summary>
        /// Send multipart/form-data request to server.
        /// Documentation: https://github.com/CommunityHiQ/Frends.Community.Multipart.SendMultipartRequest
        /// </summary>
        /// <param name="input"/>
        /// <param name="options"/>
        ///  <returns>Object { Object Body, bool IsSuccessful, Exception ErrorException, string ErrorMessage }</returns>
        public static async Task<SendResult> SendMultipartRequest(
            [PropertyTab] SendInput input,
            [PropertyTab] SendOptions options,
            CancellationToken cancellationToken)
        {
            var secondsToTicks = (int)TimeSpan.FromSeconds(Convert.ToDouble(options.Timeout)).Ticks;
            var client = new RestClient(input.Url);
            var request = new RestRequest("/", Method.Post)
            {
                AlwaysMultipartFormData = true,
                Timeout = secondsToTicks
            };

            foreach (var file in input.FilePaths)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (!File.Exists(file.Fullpath))
                    throw new FileNotFoundException("Input file was not found. File: " + file.Fullpath);

                string fileParameterKey;

                switch (file.FileParameterKey)
                {
                    case FileParameterKey.file:
                        fileParameterKey = "file";
                        break;
                    default:
                        fileParameterKey = "content";
                        break;
                }

                request.AddFile(fileParameterKey, file.Fullpath);
            }

            foreach (var text in input.TextData)
            {
                cancellationToken.ThrowIfCancellationRequested();
                request.AddParameter(text.Key, text.Value, ParameterType.GetOrPost);
            }

            foreach (var header in input.Headers)
                if (header.Name != "Content-Type")
                    request.AddHeader(header.Name, header.Value);

            if (options.Authentication is AuthenticationMethod.Basic)
                client.Authenticator = new HttpBasicAuthenticator(options.Username, options.Password);
            else if (options.Authentication is AuthenticationMethod.OAuth2)
                client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(options.BearerToken, "Bearer");

            var response = await client.ExecuteAsync(request, cancellationToken);

            if (!response.IsSuccessful && options.ThrowExceptionOnErrorResponse)
                throw new WebException($"Status Code: {(int)response.StatusCode}\nDescription: {response.StatusDescription}\nBody:\n{response.Content}");

            if (!response.IsSuccessful)
                return new SendResult { Body = null, RequestIsSuccessful = response.IsSuccessful, ErrorException = response.ErrorException, ErrorMessage = response.Content };

            return new SendResult { Body = response.Content != null ? JsonConvert.DeserializeObject<dynamic>(response.Content) : null, RequestIsSuccessful = response.IsSuccessful, ErrorException = response.ErrorException, ErrorMessage = response.ErrorMessage };
        }
    }
}
