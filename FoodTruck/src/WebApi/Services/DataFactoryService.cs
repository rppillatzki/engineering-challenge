// -----------------------------------------------------------------------
// <copyright file="DataFactoryService.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using FoodTruck.WebApi.Models;
using FoodTruck.WebApi.Objects;
using FoodTruck.WebApi.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FoodTruck.WebApi.Services
{
    /// <summary>
    /// The Data Factory Service.
    /// </summary>
    public class DataFactoryService : IDataFactoryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataFactoryService"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DataFactoryServiceOptions"/>.</param>
        public DataFactoryService(IOptions<DataFactoryServiceOptions> options)
        {
            FileName = options.Value.FileName;
            var jsonData = ReadJson();
            var foodTrucks = CreateFoodTruckCollection(jsonData);
            FoodTruckDataCollection = CreateFoodTruckDataCollection(foodTrucks);
        }

        /// <summary>
        /// Gets the FileName value.
        /// </summary>
        private string FileName { get; }

        /// <summary>
        /// Gets the FoodTruckDataCollection.
        /// </summary>
        private FoodTruckDataCollection FoodTruckDataCollection { get; }

        /// <summary>
        /// Get the FoodTruckDataCollection.
        /// </summary>
        /// <returns>The <see cref="FoodTruckDataCollection"/>.</returns>
        public FoodTruckDataCollection GetFoodTruckDataCollection()
        {
            return FoodTruckDataCollection;
        }

        /// <summary>
        /// Creates a new FoodTruckDataCollection.
        /// </summary>
        /// <param name="foodTrucks">The collection of Food Trucks.</param>
        /// <returns>A new <see cref="FoodTruckDataCollection"/>.</returns>
        private static FoodTruckDataCollection CreateFoodTruckDataCollection(IEnumerable<FoodTruckModel> foodTrucks)
        {
            var data = new FoodTruckDataCollection();
            data.TryAddRange(foodTrucks);

            return data;
        }

        /// <summary>
        /// Creates a collection of Food Trucks from JSON string.
        /// </summary>
        /// <param name="jsonData">The JSON data to parse.</param>
        /// <returns>A collection of Food Trucks.</returns>
        private static IEnumerable<FoodTruckImportModel> CreateFoodTruckCollection(string jsonData)
            => JsonConvert.DeserializeObject<List<FoodTruckImportModel>>(jsonData);

        /// <summary>
        /// Read JSON Data.
        /// </summary>
        private string ReadJson()
        {
            using var reader = new StreamReader(FileName);
            return reader.ReadToEnd();
        }
    }
}
