// <copyright file="StorageConnectionStringInProgressBuilder.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Storage connection string in progress builder.
    /// </summary>
    public class StorageConnectionStringInProgressBuilder
    {
        private readonly IDictionary<string, string> elements;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageConnectionStringInProgressBuilder"/> class.
        /// </summary>
        /// <param name="elements">The elements.</param>
        public StorageConnectionStringInProgressBuilder(IDictionary<string, string> elements)
        {
            this.elements = elements ?? throw new ArgumentNullException(nameof(elements));
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

            this.elements.Add("DefaultEndpointsProtocol", defaultEndpointsProtocol);
            return this;
        }

        /// <summary>
        /// Builds the storage connection string using SAS token.
        /// </summary>
        /// <param name="sasToken">The SAS token.</param>
        /// <returns>The storage connection string.</returns>
        public string BuildSasToken(string sasToken)
        {
            if (string.IsNullOrWhiteSpace(sasToken))
            {
                throw new ArgumentNullException(nameof(sasToken));
            }

            return this.BuildConnectionString(new Dictionary<string, string>
            {
                { "SharedAccessSignature", sasToken },
            });
        }

        /// <summary>
        /// Builds the storage connection string using account name and account key.
        /// </summary>
        /// <param name="accountName">Name of the account.</param>
        /// <param name="accountKey">The account key.</param>
        /// <returns>The storage connection string.</returns>
        public string BuildAccountKey(string accountName, string accountKey)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentNullException(nameof(accountName));
            }

            if (string.IsNullOrWhiteSpace(accountKey))
            {
                throw new ArgumentNullException(nameof(accountKey));
            }

            return this.BuildConnectionString(new Dictionary<string, string>
            {
                { "AccountName", accountName },
                { "AccountKey", accountKey },
            });
        }

        private string BuildConnectionString(Dictionary<string, string> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            var points = this.elements.Concat(dictionary).Select(e => $"{e.Key}={e.Value}");
            return $"{string.Join(";", points)}";
        }
    }
}