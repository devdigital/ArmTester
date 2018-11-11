// <copyright file="DeploymentValidationPropertiesApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate.ApiModels
{
    /// <summary>
    /// Deployment validation properties API model.
    /// </summary>
    public class DeploymentValidationPropertiesApiModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentValidationPropertiesApiModel"/> class.
        /// </summary>
        public DeploymentValidationPropertiesApiModel()
        {
            this.Mode = "Incremental";
        }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public string Mode { get; set; }

        /// <summary>
        /// Gets or sets the debug setting.
        /// </summary>
        /// <value>
        /// The debug setting.
        /// </value>
        public DeploymentValidationDebugSettingApiModel DebugSetting { get; set; }

        /// <summary>
        /// Gets or sets the template link.
        /// </summary>
        /// <value>
        /// The template link.
        /// </value>
        public DeploymentValidationTemplateLinkApiModel TemplateLink { get; set; }

        /// <summary>
        /// Gets or sets the parameters link.
        /// </summary>
        /// <value>
        /// The parameters link.
        /// </value>
        public DeploymentValidationParametersLinkApiModel ParametersLink { get; set; }
    }
}