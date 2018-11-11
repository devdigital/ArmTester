// <copyright file="StorageAccount.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Storage
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.DataMovement;

    /// <summary>
    /// Storage account.
    /// </summary>
    public class StorageAccount
    {
        private readonly CloudStorageAccount storageAccount;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageAccount"/> class.
        /// </summary>
        /// <param name="storageAccount">The storage account.</param>
        public StorageAccount(CloudStorageAccount storageAccount)
        {
            this.storageAccount = storageAccount ?? throw new ArgumentNullException(nameof(storageAccount));
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                var host = this.storageAccount?.BlobEndpoint?.Host;
                return string.IsNullOrWhiteSpace(host)
                    ? null
                    : host.Substring(0, host.IndexOf('.'));
            }
        }

        /// <summary>
        /// Gets the container URI.
        /// </summary>
        /// <value>
        /// The container URI.
        /// </value>
        public string ContainerUri
        {
            get
            {
                var uri = this.storageAccount.BlobEndpoint.ToString();
                return uri.EndsWith("/") ? uri.Substring(0, uri.Length - 1) : uri;
            }
        }

        /// <summary>
        /// Creates the container SAS token.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="expiryHours">The expiry hours.</param>
        /// <returns>
        /// The SAS token.
        /// </returns>
        public string CreateContainerSasToken(string containerName, int expiryHours = 8)
        {
            var blobClient = this.storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);

            var policy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(expiryHours),
                Permissions = SharedAccessBlobPermissions.List | SharedAccessBlobPermissions.Read,
            };

            return container.GetSharedAccessSignature(policy);
        }

        /// <summary>
        /// Uploads the folder.
        /// </summary>
        /// <param name="localFolder">The local folder.</param>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="remoteFolder">The remote folder.</param>
        /// <param name="createContainerIfNotExists">if set to <c>true</c> [create container if not exists].</param>
        /// <returns>The upload status.</returns>
        public async Task<UploadStatus> UploadFolder(
          string localFolder,
          string containerName,
          string remoteFolder,
          bool createContainerIfNotExists = false)
        {
            if (string.IsNullOrWhiteSpace(localFolder))
            {
                throw new ArgumentNullException(nameof(localFolder));
            }

            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentNullException(nameof(containerName));
            }

            if (string.IsNullOrWhiteSpace(remoteFolder))
            {
                throw new ArgumentNullException(nameof(remoteFolder));
            }

            var blobClient = this.storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(containerName);

            if (createContainerIfNotExists)
            {
                await blobContainer.CreateIfNotExistsAsync();
            }

            var options = new UploadDirectoryOptions
            {
                Recursive = true,
            };

            var blobDirectory = blobContainer.GetDirectoryReference(remoteFolder);

            var transferStatus = await TransferManager.UploadDirectoryAsync(
                localFolder,
                blobDirectory,
                options,
                new DirectoryTransferContext(checkpoint: null));

            return new UploadStatus(
                bytesTransferred: transferStatus.BytesTransferred,
                numberOfFilesTransferred: transferStatus.NumberOfFilesTransferred,
                numberOfFilesFailed: transferStatus.NumberOfFilesFailed,
                numberOfFilesSkipped: transferStatus.NumberOfFilesSkipped);
        }

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="remoteFolder">The remote folder.</param>
        /// <returns>The task.</returns>
        public async Task DeleteFolder(
            string containerName,
            string remoteFolder)
        {
            var blobClient = this.storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(containerName);

            if (blobContainer == null)
            {
                throw new InvalidOperationException($"Container '{containerName}' does not exist.");
            }

            var blobDirectory = blobContainer.GetDirectoryReference(remoteFolder);
            if (blobDirectory == null)
            {
                return;
            }

            var blobResultSegment = await blobDirectory.ListBlobsSegmentedAsync(
                useFlatBlobListing: true,
                blobListingDetails: BlobListingDetails.None,
                maxResults: null,
                currentToken: null,
                options: null,
                operationContext: null);

            foreach (var blobItem in blobResultSegment.Results)
            {
                if (blobItem.GetType() == typeof(CloudBlob) || blobItem.GetType().BaseType == typeof(CloudBlob))
                {
                    await ((CloudBlob)blobItem).DeleteIfExistsAsync();
                }
            }
        }
    }
}