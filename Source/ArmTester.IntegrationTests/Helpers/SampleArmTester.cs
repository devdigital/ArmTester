// <copyright file="SampleArmTester.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.IntegrationTests.Helpers
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using ArmTester.Authentication;
    using ArmTester.Configuration;
    using ArmTester.Validate;

    /// <summary>
    /// Sample ARM tester.
    /// </summary>
    public class SampleArmTester
    {
        private readonly Settings settings;

        private readonly X509Certificate2 certificate;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleArmTester"/> class.
        /// </summary>
        public SampleArmTester()
        {
            this.settings = new SettingsService()
                .GetFromJson<Settings>("deployment");

            this.certificate = new CertificateResolver()
                .FromStoreByThumbprint(
                    StoreName.My,
                    StoreLocation.CurrentUser,
                    thumbprint: this.settings.Thumbprint);
        }

        /// <summary>
        /// Validates a resource group deployment.
        /// </summary>
        /// <param name="templateLink">The template link.</param>
        /// <param name="parametersLink">The parameters link.</param>
        /// <returns>The ARM tester result.</returns>
        public async Task<ArmTesterResult> ValidateResourceGroup(string templateLink, string parametersLink = null)
        {
            var azureAuthenticator = new AzureAuthenticator(
                cloud: "https://login.microsoftonline.com",
                tenantId: this.settings.TenantId);

            var resolver = new Func<Task<string>>(async () =>
            {
                var authenticationResult = await azureAuthenticator.AuthenticateFromCertificate(
                    clientId: this.settings.ClientId,
                    certificate: this.certificate,
                    resourceId: "https://management.azure.com");

                return authenticationResult.AccessToken;
            });

            return await new ArmTester()
                .BeginResourceGroupDeployment(this.settings.SubscriptionId, this.settings.ResourceGroupName)
                .Validate(resolver, templateLink, parametersLink);
        }
    }
}