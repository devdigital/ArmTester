// <copyright file="ArmTesterResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate
{
    using System.Net;
    using Newtonsoft.Json;

    /// <summary>
    /// ARM tester result.
    /// </summary>
    public class ArmTesterResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArmTesterResult"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="requestBody">The request body.</param>
        /// <param name="responseBody">The response body.</param>
        public ArmTesterResult(
            HttpStatusCode? statusCode,
            string requestBody,
            string responseBody)
        {
            this.StatusCode = statusCode;
            this.RequestBody = requestBody;
            this.ResponseBody = responseBody;
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode? StatusCode { get; }

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
        /// Gets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public ApiModels.DeploymentValidationResultApiModel Response =>
            string.IsNullOrWhiteSpace(this.ResponseBody)
                ? null
                : JsonConvert.DeserializeObject<ApiModels.DeploymentValidationResultApiModel>(this.ResponseBody);

        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess => this.StatusCode.HasValue && this.StatusCode.Value >= (HttpStatusCode)200 && this.StatusCode.Value <= (HttpStatusCode)299;
    }
}