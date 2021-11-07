// -----------------------------------------------------------------------
// <copyright file="IDataService.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using FoodTruck.WebApi.Models;

namespace FoodTruck.WebApi.Services
{
    /// <summary>
    /// The Data Service Contract.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Get the list of Food Trucks.
        /// </summary>
        /// <returns>A JSON string.</returns>
        IEnumerable<FoodTruckModel> GetFoodTrucks();

        /// <summary>
        /// Get a Food Truck by locationId.
        /// </summary>
        /// <param name="locationId">The FoodTruck unique identifier locationId.</param>
        /// <returns>A FoodTruck by Id or null.</returns>
        FoodTruckModel GetFoodTruckByLocationId(long locationId);

        /// <summary>
        /// Get a collection of Food Trucks by block.
        /// </summary>
        /// <param name="block">The block identifier.</param>
        /// <returns>A collection of Food Trucks or null.</returns>
        IEnumerable<FoodTruckModel> GetFoodTrucksByBlock(string block);

        /// <summary>
        /// Add a new Food Truck.
        /// </summary>
        /// <param name="foodTruck">The <see cref="FoodTruckModel"/>.</param>
        /// <returns>True if the Food Truck was added else false.</returns>
        bool AddFoodTruck(FoodTruckModel foodTruck);
    }
}
