// -----------------------------------------------------------------------
// <copyright file="FoodTruckDataCollectionExceptionTests.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FoodTruck.WebApi.Constants;
using FoodTruck.WebApi.Models;
using FoodTruck.WebApi.Objects;
using NUnit.Framework;

namespace FoodTruck.Tests.WebApi.ObjectTests
{
    /// <summary>
    /// FoodTruck DataCollection Exception Tests.
    /// </summary>
    internal class FoodTruckDataCollectionExceptionTests
    {
        /// <summary>
        /// Food Truck DataCollection TryAddData Test.
        /// </summary>
        /// <param name="exception">The expected exception.</param>
        /// <param name="foodTruckModel">The <see cref="FoodTruckModel"/>.</param>
        [TestCaseSource(nameof(FoodTruckDataCollectionTryAddData))]
        public void FoodTruckDataCollectionTryAddExceptionTests(Type exception, FoodTruckModel foodTruckModel)
        {
            var dataCollection = new FoodTruckDataCollection();

            Assert.Throws(exception, () =>
            {
                dataCollection.TryAdd(foodTruckModel);
            });
        }

        /// <summary>
        /// Food Truck DataCollection TryAddRange Test.
        /// </summary>
        /// <param name="exception">The expected exception.</param>
        /// <param name="foodTruckModels">The collection of <see cref="FoodTruckModel"/>.</param>
        [TestCaseSource(nameof(FoodTruckDataCollectionTryAddRangeData))]
        public void FoodTruckDataCollectionTryAddRangeExceptionTests(Type exception, IEnumerable<FoodTruckModel> foodTruckModels)
        {
            var dataCollection = new FoodTruckDataCollection();

            Assert.Throws(exception, () =>
            {
                dataCollection.TryAddRange(foodTruckModels);
            });
        }

        /// <summary>
        /// Food Truck DataCollection TryGetValue Test.
        /// </summary>
        /// <param name="exception">The expected exception.</param>
        /// <param name="locationId">The locationId.</param>
        [TestCaseSource(nameof(FoodTruckDataTryGetValueData))]
        public void FoodTruckDataTryGetValueExceptionTests(Type exception, long locationId)
        {
            var dataCollection = new FoodTruckDataCollection();

            Assert.Throws(exception, () =>
            {
                dataCollection.TryGetValue(locationId, out var value);
            });
        }

        /// <summary>
        /// Food Truck DataCollection TryGetValuesByBlock Test.
        /// </summary>
        /// <param name="exception">The expected exception.</param>
        /// <param name="block">The block identifier.</param>
        [TestCaseSource(nameof(FoodTruckDataTryGetValuesByBlockData))]
        public void FoodTruckDataTryGetValuesByBlockExceptionTests(Type exception, string block)
        {
            var dataCollection = new FoodTruckDataCollection();

            Assert.Throws(exception, () =>
            {
                dataCollection.TryGetValuesByBlock(block, out var value);
            });
        }

        /// <summary>
        /// DataCollection TryAdd test case data.
        /// </summary>
        /// <returns>Test case data.</returns>
        private static IEnumerable<TestCaseData> FoodTruckDataCollectionTryAddData()
        {
            yield return new TestCaseData(typeof(ArgumentNullException), null)
                .SetArgDisplayNames("TryAddNullTest");

            yield return new TestCaseData(typeof(ArgumentOutOfRangeException), new FoodTruckModel
            {
                LocationId = ValidationConstants.MinLocationId - 1,
            })
                .SetArgDisplayNames("TryAddMinLocationIdTest");

            yield return new TestCaseData(typeof(ArgumentOutOfRangeException), new FoodTruckModel
            {
                LocationId = ValidationConstants.MaxLocationId + 1,
            })
                .SetArgDisplayNames("TryAddMaxLocationIdTest");
        }

        /// <summary>
        /// DataCollection TryAddRange test case data.
        /// </summary>
        /// <returns>Test case data.</returns>
        private static IEnumerable<TestCaseData> FoodTruckDataCollectionTryAddRangeData()
        {
            yield return new TestCaseData(typeof(ArgumentNullException), null)
                .SetArgDisplayNames("TryAddRangeNullTest");

            yield return new TestCaseData(
                typeof(ArgumentOutOfRangeException),
                new List<FoodTruckModel>
                {
                    new FoodTruckModel
                    {
                        LocationId = ValidationConstants.MinLocationId - 1,
                    },
                })
                .SetArgDisplayNames("TryAddRangeMinLocationIdTest");

            yield return new TestCaseData(
                typeof(ArgumentOutOfRangeException),
                new List<FoodTruckModel>
                {
                    new FoodTruckModel
                    {
                        LocationId = ValidationConstants.MaxLocationId + 1,
                    },
                })
                .SetArgDisplayNames("TryAddRangeMaxLocationIdTest");
        }

        /// <summary>
        /// DataCollection TryGetValue test case data.
        /// </summary>
        /// <returns>Test case data.</returns>
        private static IEnumerable<TestCaseData> FoodTruckDataTryGetValueData()
        {
            yield return new TestCaseData(typeof(ArgumentOutOfRangeException), ValidationConstants.MinLocationId - 1)
                .SetArgDisplayNames("TryGetValueMinLocationIdTest");
            yield return new TestCaseData(typeof(ArgumentOutOfRangeException), ValidationConstants.MaxLocationId + 1)
                .SetArgDisplayNames("TryGetValueMaxLocationIdTest");
        }

        /// <summary>
        /// DataCollection TryGetValuesByBlock test case data.
        /// </summary>
        /// <returns>Test case data.</returns>
        private static IEnumerable<TestCaseData> FoodTruckDataTryGetValuesByBlockData()
        {
            yield return new TestCaseData(typeof(ArgumentNullException), null)
                .SetArgDisplayNames("TryGetValueByBlockNullTest");
            yield return new TestCaseData(typeof(ArgumentNullException), string.Empty)
                .SetArgDisplayNames("TryGetValueByBlockEmptyTest");
        }
    }
}
