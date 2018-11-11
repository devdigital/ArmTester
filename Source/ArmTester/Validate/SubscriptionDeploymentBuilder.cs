// <copyright file="SubscriptionDeploymentBuilder.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Validate
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Subscription deployment builder.
    /// </summary>
    public class SubscriptionDeploymentBuilder
    {
        private readonly string subscriptionId;

        private string currentLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionDeploymentBuilder"/> class.
        /// </summary>
        /// <param name="subscriptionId">The subscription identifier.</param>
        public SubscriptionDeploymentBuilder(
            string subscriptionId)
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentNullException(nameof(subscriptionId));
            }

            this.subscriptionId = subscriptionId;
        }

        /// <summary>
        /// Adds the location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The subscription deployment builder.</returns>
        public SubscriptionDeploymentBuilder WithLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            this.currentLocation = location;
            return this;
        }

        /// <summary>
        /// Validates the subscription deployment.
        /// </summary>
        /// <param name="accessTokenResolver">The access token resolver.</param>
        /// <param name="templateLink">The template link.</param>
        /// <param name="parametersLink">The parameters link.</param>
        /// <returns>The ARM tester result.</returns>
        public Task<ArmTesterResult> Validate(Func<string> accessTokenResolver, string templateLink, string parametersLink = null)
        {
            // TODO: implement
            throw new NotImplementedException();
        }
    }
}