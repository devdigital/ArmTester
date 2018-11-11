// <copyright file="Program.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Console
{
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using ArmTester.Authentication;
    using ArmTester.Configuration;
    using ArmTester.Storage;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            var settings = new SettingsService()
                .GetFromJson<Settings>("deployment");

            Run(settings).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Runs the application.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The task.</returns>
        private static async Task Run(Settings settings)
        {
            var certificate = new CertificateResolver()
                .FromStoreByThumbprint(
                    StoreName.My,
                    StoreLocation.CurrentUser,
                    thumbprint: settings.Thumbprint);

            var containerUriBuilder = new StorageContainerUriBuilder(
                settings.StorageContainerUri,
                settings.SasToken);

            // See https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string#storing-your-connection-string
            var connectionString = new StorageConnectionStringBuilder()
                .WithBlobEndpoint(settings.BlobEndpoint)
                .BuildSasToken(settings.SasToken);

            var storageAccountService = new StorageAccountService();
            var storageAccount = storageAccountService.GetStorageAccount(connectionString);

            // var status = await storageAccountService.UploadFolder("templates", settings., "foo3");
            await storageAccount.DeleteFolder(settings.ContainerName, "foo3");

            // var tester = new ArmTester(settings.Authority, clientId: settings.ClientId, certificate: certificate);
            // var result = await tester.ValidateResourceGroupDeployment(
            //    subscriptionId: settings.SubscriptionId,
            //    resourceGroupName: settings.ResourceGroupName,
            //    templateLink: containerUriBuilder.WithFile("azuredeploy.json").Build(),
            //    parametersLink: containerUriBuilder.WithFile("azuredeploy.parameters.json").Build());
        }
    }
}