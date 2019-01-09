// <copyright file="AzureRestResponseApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.IntegrationTests.Tests.Policy
{
    /// <summary>
    /// Azure REST API response API model.
    /// </summary>
    /// <typeparam name="T">The type of properties.</typeparam>
    public class AzureRestResponseApiModel<T>
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public T Properties { get; set; }
    }
}