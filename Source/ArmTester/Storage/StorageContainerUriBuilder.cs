// <copyright file="StorageContainerUriBuilder.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Storage
{
    using System;

    /// <summary>
    /// Storage container URI builder.
    /// </summary>
    public class StorageContainerUriBuilder
    {
        private readonly string containerUri;

        private readonly string sasToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageContainerUriBuilder"/> class.
        /// </summary>
        /// <param name="containerUri">The container URI.</param>
        /// <param name="sasToken">The SAS token.</param>
        public StorageContainerUriBuilder(string containerUri, string sasToken)
        {
            if (string.IsNullOrWhiteSpace(containerUri))
            {
                throw new ArgumentNullException(nameof(containerUri));
            }

            if (string.IsNullOrWhiteSpace(sasToken))
            {
                throw new ArgumentNullException(nameof(sasToken));
            }

            if (sasToken.StartsWith("?"))
            {
                throw new ArgumentException("SAS token should not start with '?'", nameof(sasToken));
            }

            this.containerUri = containerUri;
            this.sasToken = sasToken;
        }

        /// <summary>
        /// Builds the storage container URI.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The storage container URI.</returns>
        public string Build(string file)
        {
            if (string.IsNullOrWhiteSpace(file))
            {
                throw new ArgumentNullException(nameof(file));
            }

            return $"{this.containerUri}/{file}?{this.sasToken}";
        }
    }
}