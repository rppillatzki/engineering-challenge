// -----------------------------------------------------------------------
// <copyright file="FoodTruckController.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using FoodTruck.WebApi.Models;
using FoodTruck.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers.V1
{
    /// <summary>
    /// The Food Truck Controller.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class FoodTruckController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodTruckController"/> class.
        /// </summary>
        /// <param name="dataService">The <see cref="IDataService"/>.</param>
        public FoodTruckController(
            IDataService dataService)
        {
            DataService = dataService;
        }

        /// <summary>
        /// Gets the DataService.
        /// </summary>
        private IDataService DataService { get; }

        /// <summary>
        /// Gets the list of Food Trucks.
        /// </summary>
        /// <returns>A collection of <see cref="FoodTruckModel"/>.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetFoodTrucks()
        {
            var result = DataService.GetFoodTrucks();

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Gets a Food Truck by locationId.
        /// </summary>
        /// <param name="locationId">The Food Truck unique identifier.</param>
        /// <returns>A <see cref="FoodTruckModel"/> or null.</returns>
        [HttpGet("locationId/{locationId}")]
        [ProducesResponseType(typeof(FoodTruckModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFoodTruckById([FromRoute] long locationId)
        {
            var result = DataService.GetFoodTruckByLocationId(locationId);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, null);
            }
        }

        /// <summary>
        /// Gets a list of Food Trucks by block.
        /// </summary>
        /// <param name="block">The block identifier.</param>
        /// <returns>A collection of <see cref="FoodTruckModel"/> or null.</returns>
        [HttpGet("block/{block}")]
        [ProducesResponseType(typeof(IEnumerable<FoodTruckModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFoodTrucksByBlock([FromRoute] string block)
        {
            var result = DataService.GetFoodTrucksByBlock(block);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, null);
            }
        }

        /// <summary>
        /// Adds a new Food Truck.
        /// </summary>
        /// <param name="foodTruck">The Food Truck to add <see cref="FoodTruckModel"/>.</param>
        /// <returns>A <see cref="IActionResult"/> with a 201 created status code.</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddFoodTruck([FromBody] FoodTruckModel foodTruck)
        {
            var result = DataService.AddFoodTruck(foodTruck);

            if (result)
            {
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
