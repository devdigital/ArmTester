// <copyright file="CertificateResolver.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Authentication
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Certificate resolver.
    /// </summary>
    public class CertificateResolver
    {
        /// <summary>
        /// Gets a certificate from the store by thumbprint.
        /// </summary>
        /// <param name="storeName">Name of the store.</param>
        /// <param name="storeLocation">The store location.</param>
        /// <param name="thumbprint">The thumbprint.</param>
        /// <returns>The certificate.</returns>
        public X509Certificate2 FromStoreByThumbprint(
            StoreName storeName,
            StoreLocation storeLocation,
            string thumbprint)
        {
            if (string.IsNullOrWhiteSpace(thumbprint))
            {
                throw new ArgumentNullException(nameof(thumbprint));
            }

            using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);

                return store.Certificates
                    .OfType<X509Certificate2>()
                    .FirstOrDefault(c => string.Compare(c.Thumbprint, thumbprint, StringComparison.OrdinalIgnoreCase) == 0);
            }
        }
    }
}