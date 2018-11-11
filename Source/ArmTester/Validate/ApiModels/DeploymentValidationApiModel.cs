// <copyright file="DeploymentValidationApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate.ApiModels
{
    /// <summary>
    /// Deployment validation API model.
    /// </summary>
    public class DeploymentValidationApiModel
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public DeploymentValidationPropertiesApiModel Properties { get; set; }
    }
}