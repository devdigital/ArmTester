// <copyright file="HttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Http
{
    using System.Net;
    using System.Net.Http.Headers;
    using Newtonsoft.Json;

    /// <summary>
    /// HTTP response.
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponse"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="requestBody">The request body.</param>
        /// <param name="responseBody">The response body.</param>
        public HttpResponse(
            HttpStatusCode statusCode,
            HttpResponseHeaders headers,
            string requestBody,
            string responseBody)
        {
            this.StatusCode = statusCode;
            this.Headers = headers;
            this.RequestBody = requestBody;
            this.ResponseBody = responseBody;
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public HttpResponseHeaders Headers { get; }

        /// <summary>
        /// Gets the request body.
        /// </summary>
        /// <value>
        /// The request body.
        /// </value>
        public string RequestBody { get; }

        /// <summary>
        /// Gets the response body.
        /// </summary>
        /// <value>
        /// The response body.
        /// </value>
        public string ResponseBody { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess => this.StatusCode >= (HttpStatusCode)200 && this.StatusCode <= (HttpStatusCode)299;

        /// <summary>
        /// Gets the response JSON as a type.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <returns>The instance of the type.</returns>
        public TData ResponseAs<TData>() => string.IsNullOrWhiteSpace(this.ResponseBody)
            ? default(TData)
            : JsonConvert.DeserializeObject<TData>(this.ResponseBody);
    }
}