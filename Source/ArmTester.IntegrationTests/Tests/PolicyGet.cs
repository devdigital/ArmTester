// <copyright file="PolicyGet.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.IntegrationTests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using ArmTester.Authentication;
    using ArmTester.Configuration;
    using ArmTester.IntegrationTests.Tests.Policy;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Xunit;

    // ReSharper disable StyleCop.SA1600
    #pragma warning disable SA1600
    #pragma warning disable 1591

    public class PolicyGet
    {
        [Fact]
        public async Task GetSecurityCenterPolicies()
        {
            var settings = new SettingsService()
                .GetFromJson<Settings>("deployment");

            var certificate = new CertificateResolver()
                .FromStoreByThumbprint(
                    StoreName.My,
                    StoreLocation.CurrentUser,
                    thumbprint: settings.Thumbprint);

            var accessToken = await this.GetAccessToken(
                settings.TenantId,
                settings.ClientId,
                certificate);

            var policies = await this.GetPoliciesInInitiative(
                accessToken,
                policySetDefinitionId: "1f3afdf9-d0c9-4c3d-847f-89da613e70a8");

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            var jsonExport = JsonConvert.SerializeObject(policies, jsonSettings);
        }

        private async Task<IEnumerable<PolicySummaryApiModel>> GetPoliciesInInitiative(string accessToken, string policySetDefinitionId)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            if (string.IsNullOrWhiteSpace(policySetDefinitionId))
            {
                throw new ArgumentNullException(nameof(policySetDefinitionId));
            }

            var restClient = new Http.RestApiClient(accessToken);

            var uri = $"https://management.azure.com/providers/Microsoft.Authorization/policySetDefinitions/{policySetDefinitionId}?api-version=2018-05-01&_=1544447674105";
            var response = await restClient.Get(uri);
            var policyDefinitions = response.ResponseAs<AzureRestResponseApiModel<PolicyDefinitionResponseApiModel>>();

            var policies = new List<PolicySummaryApiModel>();

            foreach (var policyDefinitionSetPolicy in policyDefinitions.Properties.PolicyDefinitions)
            {
                var policyUri = $"https://management.azure.com{policyDefinitionSetPolicy.PolicyDefinitionId}?api-version=2018-05-01";
                var policyResponse = await restClient.Get(policyUri);
                var policy = policyResponse.ResponseAs<AzureRestResponseApiModel<PolicyDetailApiModel>>();

                policies.Add(new PolicySummaryApiModel
                {
                    Name = policy.Properties.DisplayName,
                    Description = policy.Properties.Description,
                    DefaultEffect = policy.Properties.Parameters.Effect.DefaultValue,
                });
            }

            return policies;
        }

        private async Task<string> GetAccessToken(string tenantId, string clientId, X509Certificate2 certificate)
        {
            var azureAuthenticator = new AzureAuthenticator(
                cloud: "https://login.microsoftonline.com",
                tenantId: tenantId);

            var authenticationResult = await azureAuthenticator.AuthenticateFromCertificate(
                clientId: clientId,
                certificate: certificate,
                resourceId: "https://management.azure.com");

            return authenticationResult.AccessToken;
        }
    }
}