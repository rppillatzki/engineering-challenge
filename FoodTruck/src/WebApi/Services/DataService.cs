// -----------------------------------------------------------------------
// <copyright file="DataService.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FoodTruck.WebApi.Models;
using FoodTruck.WebApi.Objects;
using Microsoft.Extensions.Logging;

namespace FoodTruck.WebApi.Services
{
    /// <summary>
    /// The data service.
    /// </summary>
    public class DataService : IDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataService"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        /// <param name="dataFactoryService">The <see cref="IDataFactoryService"/>.</param>
        public DataService(ILogger<DataService> logger, IDataFactoryService dataFactoryService)
        {
            Logger = logger;
            FoodTrucks = dataFactoryService.GetFoodTruckDataCollection();
        }

        /// <summary>
        /// Gets the Logger.
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Gets the FoodTrucks dictionary.
        /// </summary>
        private FoodTruckDataCollection FoodTrucks { get; }

        /// <summary>
        /// Get the JSON Data.
        /// </summary>
        /// <returns>A JSON string.</returns>
        public IEnumerable<FoodTruckModel> GetFoodTrucks()
        {
            return FoodTrucks.Values;
        }

        /// <summary>
        /// Get a Food Truck by locationId.
        /// </summary>
        /// <param name="locationId">The FoodTruck unique identifier locationId.</param>
        /// <returns>A FoodTruck by Id or null.</returns>
        public FoodTruckModel GetFoodTruckByLocationId(long locationId)
        {
            try
            {
                if (!FoodTrucks.TryGetValue(locationId, out var model))
                {
                    return null;
                }

                return model;
            }
            catch (ArgumentOutOfRangeException exp)
            {
                Logger.LogError(exp, $"Invalid locationId: {locationId}");
                return null;
            }
        }

        /// <summary>
        /// Get a collection of Food Trucks by block.
        /// </summary>
        /// <param name="block">The block identifier.</param>
        /// <returns>A collection of Food Trucks or null.</returns>
        public IEnumerable<FoodTruckModel> GetFoodTrucksByBlock(string block)
        {
            try
            {
                if (!FoodTrucks.TryGetValuesByBlock(block, out var foodTrucks))
                {
                    return null;
                }

                return foodTrucks;
            }
            catch (ArgumentNullException exp)
            {
                Logger.LogError(exp, "Value for block is null or empty");
                return null;
            }
        }

        /// <summary>
        /// Add a new Food Truck.
        /// </summary>
        /// <param name="foodTruck">The <see cref="FoodTruckModel"/>.</param>
        /// <returns>True if the Food Truck was added else false.</returns>
        public bool AddFoodTruck(FoodTruckModel foodTruck)
        {
            try
            {
                if (!FoodTrucks.TryAdd(foodTruck))
                {
                    return false;
                }

                return true;
            }
            catch (ArgumentNullException exp)
            {
                Logger.LogError(exp, "FoodTruckModel is null");
                return false;
            }
            catch (ArgumentOutOfRangeException exp)
            {
                Logger.LogError(exp, $"Invalid locationId: {foodTruck?.LocationId}");
                return false;
            }
        }
    }
}
