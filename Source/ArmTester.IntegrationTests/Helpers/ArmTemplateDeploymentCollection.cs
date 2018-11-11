// <copyright file="ArmTemplateDeploymentCollection.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.IntegrationTests.Helpers
{
    using Xunit;

    /// <summary>
    /// ARM template deployment collection.
    /// </summary>
    [CollectionDefinition("ArmTemplateDeployment")]
    public class ArmTemplateDeploymentCollection : ICollectionFixture<ArmTemplateDeploymentFixture>
    {
    }
}