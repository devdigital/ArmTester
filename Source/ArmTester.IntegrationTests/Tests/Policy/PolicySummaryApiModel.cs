// <copyright file="PolicySummaryApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.IntegrationTests.Tests.Policy
{
    /// <summary>
    /// Policy summary API model.
    /// </summary>
    public class PolicySummaryApiModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the default effect.
        /// </summary>
        /// <value>
        /// The default effect.
        /// </value>
        public string DefaultEffect { get; set; }
    }
}