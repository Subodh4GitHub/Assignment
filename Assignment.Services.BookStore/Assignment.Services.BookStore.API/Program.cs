// **************************************************************************
// <copyright file="Program.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.API
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    /// <summary>
    /// Class for hosting the web api.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Configure and run the web application.
        /// </summary>
        /// <param name="args">Array of arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create web host to host web application.
        /// </summary>
        /// <param name="args">Array of arguments.</param>
        /// <returns>Web Host Object.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
