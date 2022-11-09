// **************************************************************************
// <copyright file="SerilogExtensions.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.Infrastructure.Logger
{
    using Microsoft.Extensions.Configuration;
    using Serilog;

    /// <summary>
    /// Class to deal with Serilog configurations.
    /// </summary>
    public static class SerilogExtensions
    {
        /// <summary>
        /// Create Logger for Serilog related configuration from the appSettings file
        /// </summary>
        public static void AddSerilog()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
        }
    }
}
