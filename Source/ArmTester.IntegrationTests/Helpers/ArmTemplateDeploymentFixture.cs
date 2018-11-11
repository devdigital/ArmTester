// <copyright file="ArmTemplateDeploymentFixture.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.IntegrationTests.Helpers
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using ArmTester.Authentication;
    using ArmTester.Configuration;
    using ArmTester.Storage;
    using Microsoft.Azure.Management.ResourceManager.Fluent;
    using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
    using Xunit;

    /// <summary>
    /// ARM template deployment fixture.
    /// </summary>
    /// <seealso cref="Xunit.IAsyncLifetime" />
    public class ArmTemplateDeploymentFixture : IAsyncLifetime
    {
        private Settings settings;

        private StorageAccount storageAccount;

        private string remoteFolder;

        private string sasToken;

        private AzureCredentials credentials;

        /// <inheritdoc />
        public async Task InitializeAsync()
        {
            this.settings = new SettingsService()
                .GetFromJson<Settings>("deployment");

            var certificate = new CertificateResolver().FromStoreByThumbprint(
                StoreName.My,
                StoreLocation.CurrentUser,
                this.settings.Thumbprint);

            this.credentials = new AzureCredentialsFactory().FromServicePrincipal(
                this.settings.ClientId,
                certificate,
                this.settings.TenantId,
                AzureEnvironment.AzureGlobalCloud);

            var storageAccountService = new StorageAccountService();
            this.storageAccount = storageAccountService.CreateStorageAccount(
                credentials: this.credentials,
                subscriptionId: this.settings.SubscriptionId,
                resourceGroup: "my-resource-group");

            this.sasToken = this.storageAccount.CreateContainerSasToken("armtemplates");
            this.remoteFolder = Guid.NewGuid().ToString();

            await this.storageAccount.UploadFolder(
                localFolder: "templates",
                containerName: "armtemplates",
                remoteFolder: this.remoteFolder,
                createContainerIfNotExists: true);
        }

        /// <summary>
        /// Converts the template file to a container URI with SAS token.
        /// </summary>
        /// <param name="templateFile">The template file.</param>
        /// <returns>The container URI with SAS token.</returns>
        public string ToContainerUri(string templateFile)
        {
            if (string.IsNullOrWhiteSpace(templateFile))
            {
                throw new ArgumentNullException(nameof(templateFile));
            }

            return $"{this.storageAccount.ContainerUri}/armtemplates/{this.remoteFolder}/{templateFile}{this.sasToken}";
        }

        /// <inheritdoc />
        public async Task DisposeAsync()
        {
            var storageAccountService = new StorageAccountService();
            await storageAccountService.DeleteStorageAccount(
                credentials: this.credentials,
                subscriptionId: this.settings.SubscriptionId,
                resourceGroup: "my-resource-group",
                storageAccountName: this.storageAccount.Name);
        }
    }
}