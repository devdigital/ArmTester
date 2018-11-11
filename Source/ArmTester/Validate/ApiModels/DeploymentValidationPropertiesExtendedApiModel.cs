// <copyright file="DeploymentValidationPropertiesExtendedApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate.ApiModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Deployment validation properties extended API model.
    /// </summary>
    public class DeploymentValidationPropertiesExtendedApiModel : DeploymentValidationPropertiesApiModel
    {
        /// <summary>
        /// Gets or sets the correlation identifier.
        /// </summary>
        /// <value>
        /// The correlation identifier.
        /// </value>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the dependencies.
        /// </summary>
        /// <value>
        /// The dependencies.
        /// </value>
        public IEnumerable<DeploymentValidationDependencyApiModel> Dependencies { get; set; }

        /// <summary>
        /// Gets or sets the outputs.
        /// </summary>
        /// <value>
        /// The outputs.
        /// </value>
        public IDictionary<string, string> Outputs { get; set; }

        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>
        /// The providers.
        /// </value>
        public IEnumerable<DeploymentValidationProviderApiModel> Providers { get; set; }

        /// <summary>
        /// Gets or sets the state of the provisioning.
        /// </summary>
        /// <value>
        /// The state of the provisioning.
        /// </value>
        public string ProvisioningState { get; set; }
    }
}