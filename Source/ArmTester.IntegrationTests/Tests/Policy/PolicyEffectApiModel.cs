// <copyright file="PolicyEffectApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.IntegrationTests.Tests.Policy
{
    /// <summary>
    /// Policy effect API model.
    /// </summary>
    public class PolicyEffectApiModel
    {
        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        /// <value>
        /// The metadata.
        /// </value>
        public PolicyEffectMetadataApiModel Metadata { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public string DefaultValue { get; set; }
    }
}