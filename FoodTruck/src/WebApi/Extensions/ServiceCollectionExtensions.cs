// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using FoodTruck.WebApi.Options;
using FoodTruck.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FoodTruck.WebApi.Extensions
{
    /// <summary>
    /// The ServiceCollection Extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Configuration Options to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
        /// <returns>The updated serviceCollection.</returns>
        public static IServiceCollection AddConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .AddOptions()
                .Configure<ApiBehaviorOptions>(options =>
                {
                    // Disables ProblemDetails error responses.
                    options.SuppressMapClientErrors = true;
                })
                .Configure<DataFactoryServiceOptions>(options =>
                {
                    options.FileName = configuration["Data"];
                });

            return serviceCollection;
        }

        /// <summary>
        /// Adds services to the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The updated serviceCollection.</returns>
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            var options = serviceCollection.BuildServiceProvider().GetRequiredService<IOptions<DataFactoryServiceOptions>>();
            serviceCollection.AddSingleton<IDataFactoryService>(new DataFactoryService(options));
            serviceCollection.AddSingleton<IDataService, DataService>();

            return serviceCollection;
        }

        /// <summary>
        /// Adds API versioning to dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The updated serviceCollection.</returns>
        public static IServiceCollection AddVersioning(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            serviceCollection.AddVersionedApiExplorer(setup =>
            {
                setup.SubstituteApiVersionInUrl = true;
                setup.SubstitutionFormat = null;
            });

            return serviceCollection;
        }

        /// <summary>
        /// Adds Swagger to dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The updated serviceCollection.</returns>
        public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                // Set the comments path for Swagger.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            serviceCollection.ConfigureOptions<SwaggerOptions>();

            return serviceCollection;
        }
    }
}
