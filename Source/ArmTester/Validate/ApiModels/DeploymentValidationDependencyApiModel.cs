// <copyright file="DeploymentValidationDependencyApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate.ApiModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Deployment validation dependency API model.
    /// </summary>
    public class DeploymentValidationDependencyApiModel
    {
        /// <summary>
        /// Gets or sets the depends on.
        /// </summary>
        /// <value>
        /// The depends on.
        /// </value>
        public IEnumerable<DeploymentValidationBasicDependency> DependsOn { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        /// <value>
        /// The name of the resource.
        /// </value>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>
        /// The type of the resource.
        /// </value>
        public string ResourceType { get; set; }
    }
}