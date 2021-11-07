// -----------------------------------------------------------------------
// <copyright file="FoodTruckDataCollection.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FoodTruck.WebApi.Constants;
using FoodTruck.WebApi.Models;

namespace FoodTruck.WebApi.Objects
{
    /// <summary>
    /// A thread safe data structure with indexes.
    /// </summary>
    public class FoodTruckDataCollection
    {
        /// <summary>
        /// Gets the values.
        /// </summary>
        public IEnumerable<FoodTruckModel> Values => FoodTrucks.Values;

        /// <summary>
        /// Gets the FoodTrucks dictionary.
        /// </summary>
        private ConcurrentDictionary<long, FoodTruckModel> FoodTrucks { get; } = new ConcurrentDictionary<long, FoodTruckModel>();

        /// <summary>
        /// Gets the Block index.
        /// </summary>
        private ConcurrentDictionary<string, HashSet<long>> BlockIndex { get; } = new ConcurrentDictionary<string, HashSet<long>>();

        /// <summary>
        /// Try Add a Food Truck into the FoodTruckDataCollection.
        /// </summary>
        /// <param name="foodTruckModel">The <see cref="FoodTruckModel"/>.</param>
        /// <returns>True if the FoodTruckModel was inserted else false.</returns>
        /// <exception cref="ArgumentNullException">FoodTruckModel is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">LocationId is out of range.</exception>
        public bool TryAdd(FoodTruckModel foodTruckModel)
        {
            _ = foodTruckModel ?? throw new ArgumentNullException(nameof(foodTruckModel));
            ValidateLocationIdRange(foodTruckModel.LocationId);

            if (FoodTrucks.TryAdd(foodTruckModel.LocationId, foodTruckModel))
            {
                var blockId = string.IsNullOrEmpty(foodTruckModel.Block) ? "0000" : foodTruckModel.Block;
                BlockIndex.AddOrUpdate(blockId, AddId, UpdateIds, foodTruckModel.LocationId);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Try get a Food Truck by locationId.
        /// </summary>
        /// <param name="locationId">The Food Truck unique identifier.</param>
        /// <param name="foodTruck">The Food Truck for the given locationId.</param>
        /// <returns>True if the value exist els false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">LocationId is out of range.</exception>
        public bool TryGetValue(long locationId, out FoodTruckModel foodTruck)
        {
            ValidateLocationIdRange(locationId);

            return FoodTrucks.TryGetValue(locationId, out foodTruck);
        }

        /// <summary>
        /// Try Get Food Trucks by Block.
        /// </summary>
        /// <param name="block">The block identifier string.</param>
        /// <param name="foodTrucks">The <see cref="FoodTruckModel"/> collection.</param>
        /// <returns>True if Food Trucks exist for the block else false.</returns>
        /// <exception cref="ArgumentNullException">block is null or empty.</exception>
        public bool TryGetValuesByBlock(string block, out IEnumerable<FoodTruckModel> foodTrucks)
        {
            _ = string.IsNullOrEmpty(block) ? throw new ArgumentNullException(nameof(block)) : block;

            if (BlockIndex.TryGetValue(block, out var locationIds))
            {
                var trucks = new List<FoodTruckModel>();
                foreach (var locationId in locationIds)
                {
                    if (FoodTrucks.TryGetValue(locationId, out var truck))
                    {
                        trucks.Add(truck);
                    }
                }

                foodTrucks = trucks;
                return true;
            }
            else
            {
                foodTrucks = null;
                return false;
            }
        }

        /// <summary>
        /// Add a collection Food Trucks into the FoodTruckDataCollection.
        /// </summary>
        /// <param name="foodTrucks">The collection of Food Trucks to add.</param>
        /// <returns>True if the Food Trucks are added else false.</returns>
        /// <exception cref="ArgumentNullException">FoodTruckModel is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">LocationId is out of range.</exception>
        public bool TryAddRange(IEnumerable<FoodTruckModel> foodTrucks)
        {
            _ = foodTrucks ?? throw new ArgumentNullException(nameof(foodTrucks));

            foreach (var foodtruck in foodTrucks)
            {
                if (!TryAdd(foodtruck))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validate locationId value.
        /// </summary>
        /// <param name="locationId">The Food Truck unique identifier.</param>
        private static void ValidateLocationIdRange(long locationId)
        {
            if (locationId < ValidationConstants.MinLocationId || locationId > ValidationConstants.MaxLocationId)
            {
                throw new ArgumentOutOfRangeException(nameof(locationId), $"The valid range is from {ValidationConstants.MinLocationId} to {ValidationConstants.MaxLocationId}");
            }
        }

        /// <summary>
        /// Adds a new HashSet with the id added.
        /// </summary>
        /// <param name="key">The key to the parent collection.</param>
        /// <param name="newValue">The new value to add.</param>
        /// <returns>A new HahSet with the new id added.</returns>
        private HashSet<long> AddId(string key, long newValue)
        {
            _ = string.IsNullOrEmpty(key) ? throw new ArgumentNullException(nameof(key)) : key;

            return new HashSet<long> { newValue };
        }

        /// <summary>
        /// Updates the HashSet by adding the new id.
        /// </summary>
        /// <param name="key">The key to the parent collection.</param>
        /// <param name="values">The HashSet to update.</param>
        /// <param name="newValue">The new value to add.</param>
        /// <returns>The update HashSet.</returns>
        private HashSet<long> UpdateIds(string key, HashSet<long> values, long newValue)
        {
            _ = string.IsNullOrEmpty(key) ? throw new ArgumentNullException(nameof(key)) : key;

            values.Add(newValue);

            return values;
        }
    }
}
