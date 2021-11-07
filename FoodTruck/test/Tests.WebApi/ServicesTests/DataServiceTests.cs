// -----------------------------------------------------------------------
// <copyright file="DataServiceTests.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using FoodTruck.Tests.WebApi.Mocks;
using FoodTruck.WebApi.Constants;
using FoodTruck.WebApi.Models;
using FoodTruck.WebApi.Objects;
using FoodTruck.WebApi.Services;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace FoodTruck.Tests.WebApi.ServicesTests
{
    /// <summary>
    /// DataService tests.
    /// </summary>
    internal class DataServiceTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataServiceTests"/> class.
        /// </summary>
        public DataServiceTests()
        {
            Logger = MockLogging<DataService>.Default().Logger.Object;

            var foodTruckDataCollection = new FoodTruckDataCollection();
            foodTruckDataCollection.TryAddRange(new List<FoodTruckModel>
            {
                new FoodTruckModel
                {
                    LocationId = 1L,
                    Block = "1111",
                },

                new FoodTruckModel
                {
                    LocationId = 2L,
                    Block = "2222",
                },
            });

            var mockDataFactoryService = MockServices.Default().DataFactoryService;
            mockDataFactoryService
                .Setup(x => x.GetFoodTruckDataCollection())
                .Returns(foodTruckDataCollection);

            DataFactoryService = mockDataFactoryService.Object;
        }

        private IDataFactoryService DataFactoryService { get; }

        private ILogger<DataService> Logger { get; }

        /// <summary>
        /// DataService GetRoodTruckByLocationId test.
        /// </summary>
        /// <param name="locationId">The locationId.</param>
        /// <param name="expectedValue">The expected locationId.</param>
        [TestCaseSource(nameof(DataServiceGetFoodTruckByLocationIdData))]
        public void DataServiceGetFoodTruckByLocationIdTests(long locationId, long? expectedValue)
        {
            var dataService = new DataService(Logger, DataFactoryService);

            var result = dataService.GetFoodTruckByLocationId(locationId);

            Assert.AreEqual(expectedValue, result?.LocationId);
        }

        /// <summary>
        /// DataService GetFoodTruckByBlock test.
        /// </summary>
        /// <param name="block">The block identifier.</param>
        /// <param name="expectedValue">The expected locationId.</param>
        [TestCaseSource(nameof(DataServiceGetFoodTrucksByBlockData))]
        public void DataServiceGetFoodTrucksByBlockTests(string block, long? expectedValue)
        {
            var dataService = new DataService(Logger, DataFactoryService);

            var results = dataService.GetFoodTrucksByBlock(block);
            var result = results?.First();

            Assert.AreEqual(expectedValue, result?.LocationId);
        }

        /// <summary>
        /// DataService AddFoodTruck test.
        /// </summary>
        /// <param name="foodTruck">The <see cref="FoodTruckModel"/> to add.</param>
        /// <param name="expectedValue">The expected boolean return value.</param>
        [TestCaseSource(nameof(DataServiceAddFoodTruckData))]
        public void DataServiceAddFoodTruckTests(FoodTruckModel foodTruck, bool expectedValue)
        {
            var dataService = new DataService(Logger, DataFactoryService);
            var result = dataService.AddFoodTruck(foodTruck);

            Assert.AreEqual(expectedValue, result);
        }

        /// <summary>
        /// DataService GetFoodTruckByLocationId test case data.
        /// </summary>
        /// <returns>Test case data.</returns>
        private static IEnumerable<TestCaseData> DataServiceGetFoodTruckByLocationIdData()
        {
            yield return new TestCaseData(ValidationConstants.MinLocationId - 1, null);
            yield return new TestCaseData(ValidationConstants.MaxLocationId + 1, null);
            yield return new TestCaseData(1L, 1L);
            yield return new TestCaseData(2L, 2L);
        }

        /// <summary>
        /// DataService GetFoodTrucksByBlock test case data.
        /// </summary>
        /// <returns>Test case data.</returns>
        private static IEnumerable<TestCaseData> DataServiceGetFoodTrucksByBlockData()
        {
            yield return new TestCaseData(null, null);
            yield return new TestCaseData(string.Empty, null);
            yield return new TestCaseData("a", null);
            yield return new TestCaseData("1111", 1L);
            yield return new TestCaseData("2222", 2L);
        }

        /// <summary>
        /// DataService AddFoodTruck test case data.
        /// </summary>
        /// <returns>Test case data.</returns>
        private static IEnumerable<TestCaseData> DataServiceAddFoodTruckData()
        {
            yield return new TestCaseData(null, false);
            yield return new TestCaseData(new FoodTruckModel { LocationId = 1L }, false);
            yield return new TestCaseData(new FoodTruckModel { LocationId = ValidationConstants.MinLocationId - 1 }, false);
            yield return new TestCaseData(new FoodTruckModel { LocationId = ValidationConstants.MaxLocationId + 1 }, false);
            yield return new TestCaseData(new FoodTruckModel { LocationId = 500L, Block = "0500" }, true);
        }
    }
}
