// -----------------------------------------------------------------------
// <copyright file="MockLogging.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using Moq;

namespace FoodTruck.Tests.WebApi.Mocks
{
    /// <summary>
    /// Mock ILogger for unit tests.
    /// </summary>
    /// <typeparam name="T">The type used for the category name.</typeparam>
    internal class MockLogging<T>
    {
        /// <summary>
        /// Gets the mock ILogger value.
        /// </summary>
        public Mock<ILogger<T>> Logger { get; private set; }

        /// <summary>
        /// Gets the mock ILoggerFactory value.
        /// </summary>
        public Mock<ILoggerFactory> LoggerFactory { get; private set; }

        /// <summary>
        /// Creates the default logging services.
        /// </summary>
        /// <returns>A new MockLogging object.</returns>
        public static MockLogging<T> Default()
        {
            return new MockLogging<T>()
            {
                LoggerFactory = GetMockLoggerFactory(),
                Logger = GetMockLogger(),
            };
        }

        /// <summary>
        /// Get a new mock ILoggerFactory.
        /// </summary>
        /// <returns>A new mock ILoggerFactory.</returns>
        private static Mock<ILoggerFactory> GetMockLoggerFactory()
            => new ();

        /// <summary>
        /// Get a new mock ILogger.
        /// </summary>
        /// <returns>A new mock ILogger.</returns>
        private static Mock<ILogger<T>> GetMockLogger()
            => new ();
    }
}
