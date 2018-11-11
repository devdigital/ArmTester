// <copyright file="RestApiClient.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Http
{
    using System;
    using System.Threading.Tasks;
    using Flurl.Http;
    using Flurl.Http.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Rest API client.
    /// </summary>
    public class RestApiClient
    {
        private readonly string token;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestApiClient"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public RestApiClient(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(token);
            }

            FlurlHttp.Configure(settings =>
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                };

                settings.JsonSerializer = new NewtonsoftJsonSerializer(
                    jsonSettings);
            });

            this.token = token;
        }

        /// <summary>
        /// Gets the HTTP response.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The HTTP response.</returns>
        public async Task<HttpResponse> Get(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var httpResponseMessage = await uri
                .WithOAuthBearerToken(this.token)
                .GetAsync();

            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            return new HttpResponse(
                statusCode: httpResponseMessage.StatusCode,
                headers: httpResponseMessage.Headers,
                requestBody: null,
                responseBody: responseBody);
        }

        /// <summary>
        /// Posts the json.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <returns>The HTTP response.</returns>
        public async Task<HttpResponse> PostJson<TRequest>(string uri, TRequest data)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var httpResponseMessage = await uri
                .WithOAuthBearerToken(this.token)
                .PostJsonAsync(data);

            // string requestBody = await httpResponseMessage.RequestMessage.Content.ReadAsStringAsync();
            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            return new HttpResponse(
                statusCode: httpResponseMessage.StatusCode,
                headers: httpResponseMessage.Headers,
                requestBody: null,
                responseBody: responseBody);
        }
    }
}