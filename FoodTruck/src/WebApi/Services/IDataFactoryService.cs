// -----------------------------------------------------------------------
// <copyright file="IDataFactoryService.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using FoodTruck.WebApi.Objects;

namespace FoodTruck.WebApi.Services
{
    /// <summary>
    /// The Data Factory Contract.
    /// </summary>
    public interface IDataFactoryService
    {
        /// <summary>
        /// Get the FoodTruckDataCollection.
        /// </summary>
        /// <returns>The <see cref="FoodTruckDataCollection"/>.</returns>
        FoodTruckDataCollection GetFoodTruckDataCollection();
    }
}
