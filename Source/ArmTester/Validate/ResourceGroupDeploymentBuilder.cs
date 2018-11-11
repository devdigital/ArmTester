// <copyright file="ResourceGroupDeploymentBuilder.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate
{
    using System;
    using System.Threading.Tasks;
    using Flurl.Http;

    /// <summary>
    /// Resource group deployment builder.
    /// </summary>
    public class ResourceGroupDeploymentBuilder
    {
        private readonly string subscriptionId;

        private readonly string resourceGroup;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceGroupDeploymentBuilder"/> class.
        /// </summary>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="resourceGroup">The resource group.</param>
        public ResourceGroupDeploymentBuilder(
            string subscriptionId,
            string resourceGroup)
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentNullException(nameof(subscriptionId));
            }

            if (string.IsNullOrWhiteSpace(resourceGroup))
            {
                throw new ArgumentNullException(nameof(resourceGroup));
            }

            this.subscriptionId = subscriptionId;
            this.resourceGroup = resourceGroup;
        }

        /// <summary>
        /// Validates the ARM template deployment.
        /// </summary>
        /// <param name="accessTokenResolver">The access token resolver.</param>
        /// <param name="templateLink">The template link.</param>
        /// <param name="parametersLink">The parameters link.</param>
        /// <returns>The ARM tester result.</returns>
        public async Task<ArmTesterResult> Validate(
            Func<Task<string>> accessTokenResolver,
            string templateLink,
            string parametersLink = null)
        {
            if (accessTokenResolver == null)
            {
                throw new ArgumentNullException(nameof(accessTokenResolver));
            }

            if (string.IsNullOrWhiteSpace(templateLink))
            {
                throw new ArgumentNullException(nameof(templateLink));
            }

            var deploymentName = Guid.NewGuid().ToString();

            var uri =
                $"https://management.azure.com/subscriptions/{this.subscriptionId}/resourcegroups/{this.resourceGroup}/providers/Microsoft.Resources/deployments/{deploymentName}/validate?api-version=2018-05-01";

            var apiModel = new ApiModels.DeploymentValidationApiModel
            {
                Properties = new ApiModels.DeploymentValidationPropertiesApiModel
                {
                    DebugSetting = new ApiModels.DeploymentValidationDebugSettingApiModel
                    {
                        DetailLevel = "requestContent, responseContent",
                    },
                    TemplateLink = new ApiModels.DeploymentValidationTemplateLinkApiModel
                    {
                        Uri = templateLink,
                    },
                    ParametersLink = string.IsNullOrWhiteSpace(parametersLink) ? null : new ApiModels.DeploymentValidationParametersLinkApiModel
                    {
                        Uri = parametersLink,
                    },
                },
            };

            try
            {
                var accessToken = await accessTokenResolver();
                var restClient = new Http.RestApiClient(accessToken);
                var result = await restClient.PostJson(uri, apiModel);
                return new ArmTesterResult(
                    statusCode: result.StatusCode,
                    requestBody: result.RequestBody,
                    responseBody: result.ResponseBody);
            }
            catch (FlurlHttpException exception)
            {
                var responseBody = await exception.GetResponseStringAsync();
                return new ArmTesterResult(
                    statusCode: exception.Call.HttpStatus,
                    requestBody: exception.Call.RequestBody,
                    responseBody: responseBody);
            }
        }
    }
}