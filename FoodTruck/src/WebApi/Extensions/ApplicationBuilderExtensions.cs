// -----------------------------------------------------------------------
// <copyright file="ApplicationBuilderExtensions.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace FoodTruck.WebApi.Extensions
{
    /// <summary>
    /// The ApplicationBuilder Extension methods.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds API documentation to the ApplicationBuilder.
        /// </summary>
        /// <param name="applicationBuilder">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="apiVersionProvider">The <see cref="IApiVersionDescriptionProvider"/>.</param>
        /// <returns>The updated ApplicationBuilder.</returns>
        public static IApplicationBuilder UseApiDocs(
            this IApplicationBuilder applicationBuilder,
            IApiVersionDescriptionProvider apiVersionProvider)
        {
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(options =>
            {
                options.EnableTryItOutByDefault();
                foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                {
                    var version = description.GroupName;
                    options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"v{version}");
                }
            });

            return applicationBuilder;
        }
    }
}
