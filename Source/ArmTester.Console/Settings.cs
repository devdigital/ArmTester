// <copyright file="Settings.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Console
{
    /// <summary>
    /// Settings.
    /// </summary>
    internal class Settings
    {
        /// <summary>
        /// Gets or sets the thumbprint.
        /// </summary>
        /// <value>
        /// The thumbprint.
        /// </value>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Gets or sets the storage container URI.
        /// </summary>
        /// <value>
        /// The storage container URI.
        /// </value>
        public string StorageContainerUri { get; set; }

        /// <summary>
        /// Gets or sets the sas token.
        /// </summary>
        /// <value>
        /// The sas token.
        /// </value>
        public string SasToken { get; set; }

        /// <summary>
        /// Gets or sets the name of the container.
        /// </summary>
        /// <value>
        /// The name of the container.
        /// </value>
        public string ContainerName { get; set; }

        /// <summary>
        /// Gets or sets the BLOB endpoint.
        /// </summary>
        /// <value>
        /// The BLOB endpoint.
        /// </value>
        public string BlobEndpoint { get; set; }
    }
}