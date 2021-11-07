// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using FoodTruck.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodTruck.WebApi
{
    /// <summary>
    /// The Application Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration <see cref="IConfiguration"/>.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddConfiguration(Configuration)
                .AddServices()
                .AddVersioning()
                .AddSwagger();

            services.AddControllers();
        }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="application">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="environment">The <see cref="IWebHostEnvironment"/>.</param>
        /// <param name="apiVersionProvider">The <see cref="IApiVersionDescriptionProvider"/>.</param>
        public void Configure(
            IApplicationBuilder application,
            IWebHostEnvironment environment,
            IApiVersionDescriptionProvider apiVersionProvider)
        {
            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
                application.UseApiDocs(apiVersionProvider);
            }

            application.UseHttpsRedirection();
            application.UseRouting();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
