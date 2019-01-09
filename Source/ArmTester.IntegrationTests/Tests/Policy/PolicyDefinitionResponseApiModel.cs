// <copyright file="PolicyDefinitionResponseApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.IntegrationTests.Tests.Policy
{
    using System.Collections.Generic;

    /// <summary>
    /// Policy definition response API model.
    /// </summary>
    public class PolicyDefinitionResponseApiModel
    {
        /// <summary>
        /// Gets or sets the policy definitions.
        /// </summary>
        /// <value>
        /// The policy definitions.
        /// </value>
        public IEnumerable<PolicyApiModel> PolicyDefinitions { get; set; }
    }
}