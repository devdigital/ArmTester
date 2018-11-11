// <copyright file="DeploymentValidationParametersLinkApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate.ApiModels
{
    /// <summary>
    /// Deployment validation parameters link API model.
    /// </summary>
    public class DeploymentValidationParametersLinkApiModel
    {
        /// <summary>
        /// Gets or sets the content version.
        /// </summary>
        /// <value>
        /// The content version.
        /// </value>
        public string ContentVersion { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public string Uri { get; set; }
    }
}