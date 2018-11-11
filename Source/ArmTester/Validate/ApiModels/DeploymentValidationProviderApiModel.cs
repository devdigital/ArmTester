// <copyright file="DeploymentValidationProviderApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate.ApiModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Deployment validation provider API model.
    /// </summary>
    public class DeploymentValidationProviderApiModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the state of the registration.
        /// </summary>
        /// <value>
        /// The state of the registration.
        /// </value>
        public string RegistrationState { get; set; }

        /// <summary>
        /// Gets or sets the resource types.
        /// </summary>
        /// <value>
        /// The resource types.
        /// </value>
        public IEnumerable<DeploymentValidationProviderResourceTypeApiModel> ResourceTypes { get; set; }
    }
}