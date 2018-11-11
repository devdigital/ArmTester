// <copyright file="DeploymentValidationProviderResourceTypeApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate.ApiModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Deployment validation provider resource type API model.
    /// </summary>
    public class DeploymentValidationProviderResourceTypeApiModel
    {
        /// <summary>
        /// Gets or sets the aliases.
        /// </summary>
        /// <value>
        /// The aliases.
        /// </value>
        public IEnumerable<DeploymentValidationAliasTypeApiModel> Aliases { get; set; }

        /// <summary>
        /// Gets or sets the API versions.
        /// </summary>
        /// <value>
        /// The API versions.
        /// </value>
        public IEnumerable<string> ApiVersions { get; set; }

        /// <summary>
        /// Gets or sets the locations.
        /// </summary>
        /// <value>
        /// The locations.
        /// </value>
        public IEnumerable<string> Locations { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public IDictionary<string, string> Properties { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>
        /// The type of the resource.
        /// </value>
        public string ResourceType { get; set; }
    }
}