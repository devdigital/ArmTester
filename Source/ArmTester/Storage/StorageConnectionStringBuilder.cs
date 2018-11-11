// <copyright file="StorageConnectionStringBuilder.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Storage
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Storage connection string builder.
    /// See https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string.
    /// </summary>
    public class StorageConnectionStringBuilder
    {
        /// <summary>
        /// Builds the storage connection string using the storage emulator.
        /// </summary>
        /// <returns>The storage connection string.</returns>
        public string BuildEmulator()
        {
            return "UseDevelopmentStorage=true";
        }

        /// <summary>
        /// Adds a default endpoint.
        /// </summary>
        /// <param name="defaultEndpointsProtocol">The default endpoints protocol.</param>
        /// <returns>The builder.</returns>
        public StorageConnectionStringInProgressBuilder WithDefaultEndpoint(string defaultEndpointsProtocol)
        {
            if (string.IsNullOrWhiteSpace(defaultEndpointsProtocol))
            {
                throw new ArgumentNullException(nameof(defaultEndpointsProtocol));
            }

            return new StorageConnectionStringInProgressBuilder(new Dictionary<string, string>
            {
                { "DefaultEndpointsProtocol", defaultEndpointsProtocol },
            });
        }

        /// <summary>
        /// Adds blog endpoint.
        /// </summary>
        /// <param name="blobEndpoint">The blob endpoint.</param>
        /// <returns>The builder.</returns>
        public StorageConnectionStringInProgressBuilder WithBlobEndpoint(string blobEndpoint)
        {
            if (string.IsNullOrWhiteSpace(blobEndpoint))
            {
                throw new ArgumentNullException(nameof(blobEndpoint));
            }

            return new StorageConnectionStringInProgressBuilder(new Dictionary<string, string>
            {
                { "BlobEndpoint", blobEndpoint },
            });
        }

        /// <summary>
        /// Adds a endpoint suffix.
        /// </summary>
        /// <param name="endpointSuffix">The endpoint suffix.</param>
        /// <returns>The builder.</returns>
        public StorageConnectionStringInProgressBuilder WithEndpointSuffix(string endpointSuffix)
        {
            if (string.IsNullOrWhiteSpace(endpointSuffix))
            {
                throw new ArgumentNullException(nameof(endpointSuffix));
            }

            return new StorageConnectionStringInProgressBuilder(new Dictionary<string, string>
            {
                { "EndpointSuffix", endpointSuffix },
            });
        }
    }
}