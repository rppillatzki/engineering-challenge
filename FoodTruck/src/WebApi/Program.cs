// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FoodTruck.WebApi
{
    /// <summary>
    /// The application entry point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method of the application.
        /// </summary>
        /// <param name="args">The arguments collection.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates Web Host.
        /// </summary>
        /// <param name="args">The arguments collection.</param>
        /// <returns>A HostBuilder <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureKestrel(options =>
                        {
                            options.Listen(IPAddress.IPv6Any, 443, listenOptions =>
                            {
                                listenOptions.UseHttps(
                                    options =>
                                    {
                                        options.SslProtocols = SslProtocols.Tls12;
                                    });
                            });
                        })
                        .ConfigureLogging(logging =>
                        {
                            logging.ClearProviders();
                            logging.AddDebug();
                        })
                        .UseStartup<Startup>();
                });
    }
}
