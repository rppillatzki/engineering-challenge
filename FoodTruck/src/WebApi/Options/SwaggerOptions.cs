// -----------------------------------------------------------------------
// <copyright file="SwaggerOptions.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FoodTruck.WebApi.Options
{
    /// <summary>
    /// Swagger Configuration Options.
    /// </summary>
    public class SwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider"/>.</param>
        public SwaggerOptions(
            IApiVersionDescriptionProvider provider)
        {
            Provider = provider;
        }

        /// <summary>
        /// Gets the API Version Description Provider.
        /// </summary>
        private IApiVersionDescriptionProvider Provider { get; }

        /// <inheritdoc/>
        public void Configure(SwaggerGenOptions options)
        {
            // adds a swagger document for every API version discovered
            foreach (var description in Provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        /// <inheritdoc/>
        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        /// <summary>
        /// Creates versioned Open API Info.
        /// </summary>
        /// <param name="description">The <see cref="ApiVersionDescription"/>.</param>
        /// <returns>A new <see cref="OpenApiInfo"/> object.</returns>
        private static OpenApiInfo CreateVersionInfo(
                ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Food Truck API",
                Version = $"{description.ApiVersion}",
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
