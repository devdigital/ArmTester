// <copyright file="ArmTester.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate
{
    using System;

    /// <summary>
    /// ARM tester.
    /// </summary>
    public class ArmTester
    {
        /// <summary>
        /// Begins the subscription deployment.
        /// </summary>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <returns>The subscription deployment builder.</returns>
        public SubscriptionDeploymentBuilder BeginSubscriptionDeployment(string subscriptionId)
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentNullException(nameof(subscriptionId));
            }

            return new SubscriptionDeploymentBuilder(
                subscriptionId: subscriptionId);
        }

        /// <summary>
        /// Begins the resource group deployment.
        /// </summary>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="resourceGroup">The resource group.</param>
        /// <returns>The resource group deployment builder.</returns>
        public ResourceGroupDeploymentBuilder BeginResourceGroupDeployment(string subscriptionId, string resourceGroup)
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentNullException(nameof(subscriptionId));
            }

            if (string.IsNullOrWhiteSpace(resourceGroup))
            {
                throw new ArgumentNullException(nameof(resourceGroup));
            }

            return new ResourceGroupDeploymentBuilder(
                subscriptionId: subscriptionId,
                resourceGroup: resourceGroup);
        }
    }
}