// -----------------------------------------------------------------------
// <copyright file="FoodTruckDataCollectionTests.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using FoodTruck.WebApi.Models;
using FoodTruck.WebApi.Objects;
using NUnit.Framework;

namespace FoodTruck.Tests.WebApi.ObjectTests
{
    /// <summary>
    /// Food Truck Data Collection Tests.
    /// </summary>
    public class FoodTruckDataCollectionTests
    {
        /// <summary>
        /// FoodTruckDataCollection TryGetValues Tests.
        /// </summary>
        /// <param name="foodtruck">The <see cref="FoodTruckModel"/>.</param>
        /// <param name="block">The identifier string.</param>
        /// <param name="expectedLocationId">The expected locationId value.</param>
        /// <param name="expextedBlock">The expected block identifier value.</param>
        [TestCaseSource(nameof(FoodTruckDataTryGetValuesByBlockData))]
        public void FoodTruckDataTryGetValuesByBlockTests(
            FoodTruckModel foodtruck,
            string block,
            long expectedLocationId,
            string expextedBlock)
        {
            var dataCollection = new FoodTruckDataCollection();
            dataCollection.TryAdd(foodtruck);
            dataCollection.TryGetValuesByBlock(block, out var values);
            var value = values.First();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedLocationId, value.LocationId);
                Assert.AreEqual(expextedBlock, value.Block);
            });
        }

        /// <summary>
        /// The test case data for TryGetValuesByBlock.
        /// </summary>
        /// <returns>The test case data.</returns>
        private static IEnumerable<TestCaseData> FoodTruckDataTryGetValuesByBlockData()
        {
            yield return new TestCaseData(
                new FoodTruckModel
                {
                    LocationId = 1,
                    Block = null,
                },
                "0000",
                1L,
                null)
                .SetArgDisplayNames("TryGetValuesByBlockNullTest");

            yield return new TestCaseData(
                new FoodTruckModel
                {
                    LocationId = 2,
                    Block = string.Empty,
                },
                "0000",
                2L,
                string.Empty)
                .SetArgDisplayNames("TryGetValuesByBlockNullTest");

            yield return new TestCaseData(
                new FoodTruckModel
                {
                    LocationId = 3,
                    Block = "1234",
                },
                "1234",
                3L,
                "1234")
                .SetArgDisplayNames("TryGetValuesByBlockTest");
        }
    }
}
