// <copyright file="TemplatesShould.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.IntegrationTests.Tests
{
    using System.Threading.Tasks;
    using ArmTester.IntegrationTests.Helpers;
    using AutoFixture.Xunit2;
    using Xunit;

    // ReSharper disable StyleCop.SA1600
    #pragma warning disable SA1600
    #pragma warning disable 1591

    [Collection("ArmTemplateDeployment")]
    public class TemplatesShould
    {
        private readonly ArmTemplateDeploymentFixture fixture;

        public TemplatesShould(ArmTemplateDeploymentFixture fixture)
        {
           this.fixture = fixture;
        }

        [Theory]
        [AutoData]
        public async Task TestResourceGroup(SampleArmTester armTester)
        {
            var result = await armTester.ValidateResourceGroup(
                templateLink: this.fixture.ToContainerUri("azuredeploy.json"),
                parametersLink: this.fixture.ToContainerUri("azuredeploy.parameters.json"));

            Assert.True(result.IsSuccess);
        }
    }
}
