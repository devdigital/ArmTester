// <copyright file="DeploymentValidationAliasTypeApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate.ApiModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Deployment validation alias type API model.
    /// </summary>
    public class DeploymentValidationAliasTypeApiModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the paths.
        /// </summary>
        /// <value>
        /// The paths.
        /// </value>
        public IEnumerable<DeploymentValidationAliasPathTypeApiModel> Paths { get; set; }
    }
}