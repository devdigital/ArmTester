// <copyright file="AzureAuthenticator.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Authentication
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    /// <summary>
    /// Azure authenticator.
    /// See https://github.com/azure-samples/active-directory-dotnet-daemon-certificate-credential
    /// and https://github.com/Azure-Samples/active-directory-dotnet-daemon-certificate-credential/blob/master/TodoListDaemonWithCert/Program.cs.
    /// </summary>
    public class AzureAuthenticator
    {
        private readonly string authority;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureAuthenticator"/> class.
        /// Example cloud is "https://login.microsoftonline.com"
        /// Example tenantId is "contoso.onmicrosoft.com".
        /// </summary>
        /// <param name="cloud">The cloud.</param>
        /// <param name="tenantId">The tenant identifier.</param>
        public AzureAuthenticator(string cloud, string tenantId)
        {
            if (string.IsNullOrWhiteSpace(cloud))
            {
                throw new ArgumentNullException(nameof(cloud));
            }

            if (string.IsNullOrWhiteSpace(tenantId))
            {
                throw new ArgumentNullException(nameof(tenantId));
            }

            this.authority = $"{cloud}/{tenantId}";
        }

        /// <summary>
        /// Authenticates against Azure from an X509 certificate.
        /// clientId is the application id within Azure AD.
        /// resourceId is e.g. "https://management.azure.com" for resource management API.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="resourceId">The resource identifier.</param>
        /// <returns>The authentication result.</returns>
        public async Task<AuthenticationResult> AuthenticateFromCertificate(
            string clientId,
            X509Certificate2 certificate,
            string resourceId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentNullException(nameof(clientId));
            }

            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            var clientAssertionCertificate = new ClientAssertionCertificate(clientId, certificate);
            return await this.AuthenticateFromClientAssertionCertificate(resourceId, clientAssertionCertificate);
        }

        private async Task<AuthenticationResult> AuthenticateFromClientAssertionCertificate(
            string resourceId,
            IClientAssertionCertificate certificate)
        {
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                throw new ArgumentNullException(nameof(resourceId));
            }

            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            var authContext = new AuthenticationContext(this.authority);
            return await authContext.AcquireTokenAsync(resourceId, certificate);
        }
    }
}