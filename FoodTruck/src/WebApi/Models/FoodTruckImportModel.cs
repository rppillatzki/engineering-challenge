// -----------------------------------------------------------------------
// <copyright file="FoodTruckImportModel.cs" company="Contoso">
//   Copyright (c) Contoso Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Newtonsoft.Json;

namespace FoodTruck.WebApi.Models
{
    /// <summary>
    /// The Food Truck Data Import Model.
    /// </summary>
    public class FoodTruckImportModel : FoodTruckModel
    {
        /// <summary>
        /// Gets or sets the LocationId value.
        /// </summary>
        [JsonRequired]
        [JsonProperty("objectid")]
        public override long LocationId { get; set; }
    }
}
