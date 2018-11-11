// <copyright file="StorageConnectionStringBuilderShould.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.UnitTests.Tests
{
    using ArmTester.Storage;
    using AutoFixture.Xunit2;
    using Xunit;

    // ReSharper disable StyleCop.SA1600
    #pragma warning disable SA1600
    #pragma warning disable 1591

    public class StorageConnectionStringBuilderShould
    {
        [Theory]
        [AutoData]
        public void ReturnEmulatorConnectionString(
            StorageConnectionStringBuilder builder)
        {
            var connectionString = builder.BuildEmulator();
            Assert.Equal("UseDevelopmentStorage=true", connectionString);
        }
    }
}
