// <copyright file="DeploymentValidationResultApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate.ApiModels
{
    /// <summary>
    /// Deployment validation result API model.
    /// </summary>
    public class DeploymentValidationResultApiModel
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public DeploymentValidationErrorResultApiModel Error { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public DeploymentValidationPropertiesExtendedApiModel Properties { get; set; }
    }
}