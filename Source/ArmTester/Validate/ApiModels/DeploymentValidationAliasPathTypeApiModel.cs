// <copyright file="DeploymentValidationAliasPathTypeApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate.ApiModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Deployment validation alias path type API model.
    /// </summary>
    public class DeploymentValidationAliasPathTypeApiModel
    {
        /// <summary>
        /// Gets or sets the API versions.
        /// </summary>
        /// <value>
        /// The API versions.
        /// </value>
        public IEnumerable<string> ApiVersions { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }
    }
}