// <copyright file="StorageAccountService.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Storage
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Azure.Management.Fluent;
    using Microsoft.Azure.Management.ResourceManager.Fluent;
    using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
    using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
    using Microsoft.WindowsAzure.Storage;

    /// <summary>
    /// Storage account service.
    /// </summary>
    public class StorageAccountService
    {
        /// <summary>
        /// Gets the storage account.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The storage account.</returns>
        public StorageAccount GetStorageAccount(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            if (storageAccount == null)
            {
                throw new InvalidOperationException("The storage account could not be retrieved.");
            }

            return new StorageAccount(storageAccount: storageAccount);
        }

        /// <summary>
        /// Creates a storage account.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="resourceGroup">The resource group.</param>
        /// <returns>The storage account.</returns>
        public StorageAccount CreateStorageAccount(
            AzureCredentials credentials,
            string subscriptionId,
            string resourceGroup)
        {
            var storageAccountName = SdkContext.RandomResourceName("sa", 20);
            return this.CreateStorageAccount(
                credentials: credentials,
                subscriptionId: subscriptionId,
                resourceGroup: resourceGroup,
                storageAccountName: storageAccountName);
        }

        /// <summary>
        /// Creates the storage account.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="resourceGroup">The resource group.</param>
        /// <param name="storageAccountName">Name of the storage account.</param>
        /// <returns>The storage account.</returns>
        public StorageAccount CreateStorageAccount(
            AzureCredentials credentials,
            string subscriptionId,
            string resourceGroup,
            string storageAccountName)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentNullException(nameof(subscriptionId));
            }

            if (string.IsNullOrWhiteSpace(resourceGroup))
            {
                throw new ArgumentNullException(nameof(resourceGroup));
            }

            if (string.IsNullOrWhiteSpace(storageAccountName))
            {
                throw new ArgumentNullException(nameof(storageAccountName));
            }

            var azure = Azure.Authenticate(credentials)
                .WithSubscription(subscriptionId);

            var storageAccount = azure.StorageAccounts.Define(storageAccountName)
                .WithRegion(Region.USEast)
                .WithExistingResourceGroup(resourceGroup)
                .Create();

            var blobEndpoint = storageAccount?.EndPoints?.Primary?.Blob;
            var accountKey = storageAccount?.GetKeys()?.First()?.Value;

            var connectionString = new StorageConnectionStringBuilder()
                .WithBlobEndpoint(blobEndpoint)
                .WithDefaultEndpoint("https")
                .BuildAccountKey(storageAccountName, accountKey);

            return this.GetStorageAccount(connectionString);
        }

        /// <summary>
        /// Deletes the storage account.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="resourceGroup">The resource group.</param>
        /// <param name="storageAccountName">Name of the storage account.</param>
        /// <returns>The task.</returns>
        public async Task DeleteStorageAccount(
            AzureCredentials credentials,
            string subscriptionId,
            string resourceGroup,
            string storageAccountName)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }

            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentNullException(nameof(subscriptionId));
            }

            if (string.IsNullOrWhiteSpace(resourceGroup))
            {
                throw new ArgumentNullException(nameof(resourceGroup));
            }

            if (string.IsNullOrWhiteSpace(storageAccountName))
            {
                throw new ArgumentNullException(nameof(storageAccountName));
            }

            var azure = Azure.Authenticate(credentials)
                .WithSubscription(subscriptionId);

            await azure.StorageAccounts.DeleteByResourceGroupAsync(
                resourceGroup,
                storageAccountName);
        }
    }
}