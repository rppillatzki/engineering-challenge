// -----------------------------------------------------------------------
// <copyright file="FoodTruckControllerTests.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using FoodTruck.Tests.WebApi.Mocks;
using FoodTruck.WebApi.Constants;
using FoodTruck.WebApi.Controllers.V1;
using FoodTruck.WebApi.Models;
using FoodTruck.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FoodTruck.Tests.WebApi.ControllerTests
{
    /// <summary>
    /// Food Truck Controller Tests.
    /// </summary>
    internal class FoodTruckControllerTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodTruckControllerTests"/> class.
        /// </summary>
        public FoodTruckControllerTests()
        {
            DataService = MockServices.Default().DataService;
        }

        /// <summary>
        /// Gets the Mock DataService.
        /// </summary>
        private Mock<IDataService> DataService { get; }

        /// <summary>
        /// GetFoodTrucks action OkResult test.
        /// </summary>
        [Test]
        public void GetFoodTrucksReturnsOkResult()
        {
            DataService
                .Setup(x => x.GetFoodTrucks())
                .Returns(new List<FoodTruckModel>
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

            var controller = new FoodTruckController(DataService.Object);

            var objectResult = controller.GetFoodTrucks() as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        /// <summary>
        /// GetFoodTruckById action OkResult test.
        /// </summary>
        [Test]
        public void GetFoodTruckByIdReturnsOkResult()
        {
            var locationId = 1L;

            DataService
                .Setup(x => x.GetFoodTruckByLocationId(It.IsAny<long>()))
                .Returns(new FoodTruckModel
                {
                    LocationId = locationId,
                });

            var controller = new FoodTruckController(DataService.Object);

            var objectResult = controller.GetFoodTruckById(locationId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        /// <summary>
        /// GetFoodTruckById action NotFound test.
        /// </summary>
        [Test]
        public void GetFoodTruckByIdReturnsNotFoundResult()
        {
            var locationId = 1L;

            DataService
                .Setup(x => x.GetFoodTruckByLocationId(It.IsAny<long>()))
                .Returns<FoodTruckModel>(null);

            var controller = new FoodTruckController(DataService.Object);

            var objectResult = controller.GetFoodTruckById(locationId) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        /// <summary>
        /// GetFoodTrucksByBlock action OkResult test.
        /// </summary>
        [Test]
        public void GetFoodTrucksByBlockReturnsOkResult()
        {
            var block = "1111";

            DataService
                .Setup(x => x.GetFoodTrucksByBlock(It.IsAny<string>()))
                .Returns(new List<FoodTruckModel>
                {
                    new FoodTruckModel
                    {
                        LocationId = 1L,
                        Block = "1111",
                    },
                });

            var controller = new FoodTruckController(DataService.Object);

            var objectResult = controller.GetFoodTrucksByBlock(block) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        /// <summary>
        /// GetFoodTrycksByBlock action NotFound Test.
        /// </summary>
        [Test]
        public void GetFoodTrucksByBlockReturnsNotFoundResult()
        {
            var block = "1111";

            DataService
                .Setup(x => x.GetFoodTrucksByBlock(It.IsAny<string>()))
                .Returns<IEnumerable<FoodTruckModel>>(null);

            var controller = new FoodTruckController(DataService.Object);

            var objectResult = controller.GetFoodTrucksByBlock(block) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        /// <summary>
        /// AddFoodTruck action Created test.
        /// </summary>
        [Test]
        public void AddFoodTruckReturnsCreatedResult()
        {
            var foodTruck = new FoodTruckModel
            {
                LocationId = 1L,
            };

            DataService
                .Setup(x => x.AddFoodTruck(It.IsAny<FoodTruckModel>()))
                .Returns(true);

            var controller = new FoodTruckController(DataService.Object);

            var result = controller.AddFoodTruck(foodTruck) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
        }

        /// <summary>
        /// AddFoodTruck action BadRequest test.
        /// </summary>
        [Test]
        public void AddFoodTruckReturnsBadRequestResult()
        {
            var foodTruck = new FoodTruckModel
            {
                LocationId = ValidationConstants.MinLocationId - 1,
            };

            DataService
                .Setup(x => x.AddFoodTruck(It.IsAny<FoodTruckModel>()))
                .Returns(false);

            var controller = new FoodTruckController(DataService.Object);

            var result = controller.AddFoodTruck(foodTruck) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }
    }
}
