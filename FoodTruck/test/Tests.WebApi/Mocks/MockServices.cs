// -----------------------------------------------------------------------
// <copyright file="MockServices.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using FoodTruck.WebApi.Services;
using Moq;

namespace FoodTruck.Tests.WebApi.Mocks
{
    /// <summary>
    /// Mock Services for unit tests.
    /// </summary>
    internal class MockServices
    {
        /// <summary>
        /// Gets the Mock DataFactoryService value.
        /// </summary>
        public Mock<IDataFactoryService> DataFactoryService { get; private set; }

        /// <summary>
        /// Gets the Mock DataServie value.
        /// </summary>
        public Mock<IDataService> DataService { get; private set; }

        /// <summary>
        /// Creates instances of the default services.
        /// </summary>
        /// <returns>A new MockServices object.</returns>
        public static MockServices Default()
        {
            return new MockServices()
            {
                DataFactoryService = GetMockDataFactoryService(),
                DataService = GetMockDataService(),
            };
        }

        /// <summary>
        /// Get a new mock DataFactoryService.
        /// </summary>
        /// <returns>A mock <see cref="IDataFactoryService"/>.</returns>
        private static Mock<IDataFactoryService> GetMockDataFactoryService() => new ();

        /// <summary>
        /// Get a new mock DataService.
        /// </summary>
        /// <returns>A mock <see cref="IDataService"/>.</returns>
        private static Mock<IDataService> GetMockDataService() => new ();
    }
}
