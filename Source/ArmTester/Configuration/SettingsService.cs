// <copyright file="SettingsService.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Configuration
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Settings service.
    /// </summary>
    public class SettingsService
    {
        /// <summary>
        /// Gets settings from appsettings.json files.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <param name="sectionKey">The section key.</param>
        /// <returns>The settings instance.</returns>
        public TData GetFromJson<TData>(string sectionKey)
            where TData : new()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appSettings.Development.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var settings = new TData();
            configuration.GetSection(sectionKey).Bind(settings);

            return settings;
        }
    }
}